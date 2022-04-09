# Instrucciones

## Instalar Plantilla **nDev-webapi**

* Para instalar la plantilla **nDev-webapi** se debe realizar lo siguiente:

  * Clonar el proyecto que contiene la plantilla con:

  ```clonarProyecto
    git clone http://machangara.ddns.net:3397/gabriel.tarapues/ndeveloper-dotnet-templates.git
  ```

  * Nos ubicamos en la carpeta raiz del proyecto clonado y ejecutamos el siguiente comando:

  ```instalarPlantilla
     dotnet new -i .
  ```

  * Si todo se ejecuto correctamente, nos desplegara el listado de plantillas que se encuentran instaladas entre las cuales deberia listarse la recien instalada con la descripcion:
    * **Template Name:** nDeveloper Webapi Template
    * **Short Name:** nDev-webapi
    * **Language:** [C#]
    * **Tags:** webapi

## Crear Nuevo Proyecto con plantilla **nDev-webapi**

* Para crear un proyecto nuevo con la plantilla **nDev-webapi** se debe ejecutar el siguiente comando

  ```crearProyecto
    dotnet new ndev-webapi --proyectName [NombreProyectoNuevo] --allow-scripts yes
  ```

  ***--allow-scripts*** ==> Permite ejecucion de scripts sin confirmacion.

## Primeros Pasos

Una vez creado el proyecto nuevo con la plantilla **nDev-webapi**, se debe seguir los siguientes pasos:

* ### **Crear Proyecto git**
  
  * En la pantalla principal de nuestra cuenta gitlab, seleccionamos New Project
  * Ingresamos nombre del nuevo proyecto
  * Ingresamos una descripcion para el proyecto.
  * Seccionamos nivel de visibilidad del proyecto.
  * **NO** se debe seleccionar la creacion del README
  * Y por ultimo hacemos click sobre Create project.

* ### **Variables Para CI/CD**

  Se deben crear tambien las siguientes variables en en la seccion de CI/CD en el proyecto creado, ingresando al siguiente menu: **Settings/CI CD/** seccion **Variables**:

  * REGISTRY_USERNAME: Nombre de usuario del docker registry utilizado
  * REGISTRY_PASSWORD: Password del docker registry utilizado
  * REGISTRY_SERVER: url del servicio de docker registry. Ej: docker.io/gabrielomar86

  Estas variables son necesarias para poder realizar un docker push de la imagen creada del backend.

* ### **Registrar runner en gitlab**

  * Para instalar un Gitlab-Runner se deben seguir las instrucciones publicadas en la siguiente url:
  <https://docs.gitlab.com/runner/install/>
  * Una vez creado el proyecto en gitlab, nos dirigimos al menu dentro del proyecto ***Settings/CI CD*** luego hacer click sobre *Expand* en la seccion *Runner*, aqui se mostrara los datos necesarios para configurar nuestro runner como la URL y su token.
  * En el servidor donde el Runner se ejecuta, se debe realizar lo siguiente:

    [WINDOWS]

    ```registrarRunner
      .\gitlab-runner-windows-amd64.exe stop;
      .\gitlab-runner-windows-amd64.exe register
      En la primera opcion se debe ingresar la URL que tenemos en la seccion Runner, antes mencionada.
      Luego se debe ingresar el token que se encuentra en la misma seccion mencionada con anterioridad.
      Luego se debe ingresar una descripcion para el Runner.
      Cuando nos pida ingresar el TAG, este debe coincidir con el tag que se encuentra en el archivo .gitlab-ci.yml, para esta plantilla se deberia ingresar el siguiente nombre nDevRunner. 
      Si se decide poner otro tag al runner, se deberia actualizar dicho nombre en la seccion tag del archivo .gitlab-ci.yml.
      Luego se debe ingresar el tipo de Runner, para esta plantilla el tipo es SHELL.
    ```

    ```notaShell

      NOTA:

      Se utiliza shell para poder desplegar la aplicacion en el servidor donde se encuentra el runner, que en este caso tambien tiene instalado docker y docker-compose.

      https://docs.gitlab.com/runner/executors/shell.html

    ```

* ### Ejemplo runner en archivo .toml

    ```ejemploToml

    luego de seguir los pasos anteriores, el registro en el archivo .toml deberia tener la siguiente estructura:

    [[runners]]
      name = "GTARAPUES-DESKTOP"
      url = "[URL CI/CD PROYECTO GITLAB]"
      token = "[TOKEN ENCRIPTADO CI/CD PROYECTO GITLAB]"
      executor = "shell"
      shell = "pwsh"
      [runners.custom_build_dir]
      [runners.cache]
        [runners.cache.s3]
        [runners.cache.gcs]
        [runners.cache.azure]

    ```
  
  * Una vez realizado los pasos descritos anteriormente, en el menu dentro del proyecto ***Settings/CI CD*** en la seccion *Runner*, debe aparecer el runner que acabamos de registrar.
  * Luego nos resta solo ejecutar el runner en el servidor:

  ```ejecutarRunner
    .\gitlab-runner-windows-amd64.exe start
  ```

* ### **Registrar url git**

  * Dentro de nuestra cuenta gitlab, seleccionamos el proyecto creado con anterioridad.
  * Al hacer click sobre el boton Clone, nos despliega la URL del proyecto tanto en SSH como en HTTP, copiamos la que se vaya a utilizar.
  * Asignar url de proyecto creado en gitlab al repositorio local: con el siguiente comando:

  ```gitRemoteAdd
    git remote add origin [urlCopiada]
  ```

  * Una vez asignado la url al proyecto local, lo que resta es crear nuestro primer commit y realizar el primer push hacia gitlab.
  
  ```PrimerCommit
    git add .
    git commit -m "Initial commit"
    git push -u origin master  
  ```

  * Al realizar el push una vez ya subido los cambios a gitlab, este ejecutara el pipeline el mismo que podemos ver en el menu dentro del proyecto ***CI CD/Pipeline***

# K8S
* Para desplegar el backend en un cluster local de k8s se debe seguir los siguientes paso:

  * [Windows]
  Configurar k8s, se puede seguir los pasos del siguiente link: <https://docs.docker.com/desktop/kubernetes/>
  * Crear nuevo proyecto con base en la plantilla nDev-webapi. la plantilla creara una carpeta llamada k8s en donde se tiene los siguientes archivos:
    
    * deployment.yaml --> Este archivo se encarga de crear los pods y desplegarla dentro del cluster.
    * service.yaml --> Este archivo se encarga de crear un Balanceador de carga para poder acceder al backend externamente.

  * Dentro del archivo **k8s/deployment.yaml** reemplazar **[IMAGEN-BACKEND-DOCKER-REGISTRY]** por el nombre de la imagen del backend generada y publicado en el docker registry de preferencia.
  Para registrar una imagen en un docker registry nos podemos basar en los comandos que se encuentran  en el archivo **.gitlab-ci.yml** en las lineas 28, 29 y  30, estos comandos se los ejecuta desde la carpeta raiz del proyecto si se lo quiere hacer manualmente, o se puede descomentar todo el codigo comprendido desde la linea 22 a 36, con esto al momento de que se ejecute el pipeline se generara y se publicara en un docker registry.
  * Luego nos ubicamos en la raiz del proyecto y ejecutamos los siguientes comandos:

    ```DesplegarK8S
    kubectl apply -f k8s/deployment.yaml 
    kubectl apply -f k8s/service.yaml 
    
    Este ultimo comando se lo debe ejecutar cuando exista un deployment en el cluster de k8s, este comando reinicia el deployment para que se refleje los nuevos cambios.
      kubectl rollout restart deployment/ejemploplantilla
    ```
  Una vez ejecutado estos comandos, ejecutamos el siguiente comando:
    ```
     kubectl get all
    ```
  y podemos visualizar lo siguiente:
  
  Informacion de **POD**
  * **NAME:** pod/[nombreproyecto]-78dd76b59b-6jr8d
  * **READY:** 1/1
  * **STATUS:** Running 
  * **RESTARTS:** 0
  * **AGE:** 55s

  Informacion de **Deployment**
  * **NAME:** deployment.apps/[nombreproyecto]
  * **READY:** 1/1
  * **UP-TO-DATE:** 1 
  * **AVAILABLE:** 1
  * **AGE:** 60s

  Informacion de **Servicio**
  * **NAME:** service/[nombreproyecto]
  * **TYPE:** LoadBalancer
  * **CLUSTER-IP:** 10.100.116.12
  * **EXTERNAL-IP:** localhost
  * **PORT(S):** 2005:31589/TCP
  * **AGE:** 58s

  La informacion de servicio es donde podemos encontrar el acceso al pod externamente, en donde la informacion que nos interesa es **EXTERNAL-IP** y **PORT** en este caso el puerto de acceso externo es **2005**, y la url final para acceder remotamente seria: **http://localhost:2005/[nombreproyecto]**

## Crear Nueva Migracion

* Para crear una nueva migracion, se debe realizar los siguientes pasos:
  * Posicionarse dentro de la carpeta src.
  * Ejecutar el siguiente comando:

    ```addMigration
    dotnet ef migrations add [NombreNuevaMigracion] -p infrastructure -s api -o data/Migrations
    ```

Esto creara una nueva migracion dentro del proyecto infrastructure en la carpeta data/Migrations

Al ejecutar la aplicacion, se ejecutara las migraciones automaticamente, incluyendo sus datos semillas, para el ejemplo se ha configurado una base de datos SQLITE, la misma que por default se creara en el proyecto API con el nombre ndeveloper-ejemplo.db.

## Creacion de imagen del backend

* Posicionarse en la carpeta raiz del proyecto.
* Ejecutar el siguiente comando
  
  ```crearContenedor
    docker build --no-cache=true -t blogengineapp:latest ./src -f ./ci-cd/docker/Dockerfile --progress=plain
  ```

* Para ejecutar la imagen se debe ejecutar el siguiente comando  
  
  ```ejecutarImagen
    docker run -d -it --rm --name blogengineapp -p 80:8080 [id-imagen] 
  ```

  Este comando ejecuta la imagen sobre el puerto 8080.

## Referencias

  <https://devblogs.microsoft.com/dotnet/how-to-create-your-own-templates-for-dotnet-new/>

  <https://github.com/dotnet/templating/wiki/>

  <http://json.schemastore.org/template>

  <https://queil.net/2018/07/dotnet-templating-cheat-sheet/>

  <https://rehansaeed.com/dotnet-new-feature-selection/>

  <https://weblog.west-wind.com/posts/2020/Oct/05/Creating-a-dotnet-new-Project-Template>

## Interesantes

  <https://auth0.com/blog/create-dotnet-project-template/>

### Ejemplos

  <https://github.com/dotnet/dotnet-template-samples>

## Markdown

  <https://github.com/DavidAnson/markdownlint/blob/v0.24.0/doc/Rules.md>

## Kubernetes

  <https://kubernetes.io/blog/2020/05/21/wsl-docker-kubernetes-on-the-windows-desktop/>  
