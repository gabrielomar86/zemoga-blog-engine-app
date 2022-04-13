# Blog  Engine App
**Development time:** 38 hours
**CI/CD:**
- Azure Static Web Apps CI/CD
- Azure Web App

**Public  access**
	https://icy-flower-039c20a0f.1.azurestaticapps.net/
	
**Dependencies versions**
* dotnet core 5.0
* Node v14.19.1
* Angular 13.+
* Sqlite

# Installing software prerequisites

To run this application, first you need to install the following:
* Git 2.25.1
	https://www.makeuseof.com/install-configure-git-on-linux/
* .Net sdk 5.x
	https://docs.microsoft.com/en-us/dotnet/core/install/
* Node.js
	https://nodejs.org/en/download/package-manager/
* Angular 13
	https://angular.io/guide/setup-local
	

Once you install all of necessary dependencies, you're ready to run this application.

# Cloning application
To clone the application repository run the following command:
```bash
git clone https://github.com/gabrielomar86/zemoga-blog-engine-app.git
```
now, you can see `zemoga-blog-engine-app` directory

# Running backend
To run backend you need to landing in the following directory:
```bash
cd zemoga-blog-engine-app/
dotnet restore
cd src/api
dotnet run
```
the application is running up on **5000 http** and **5001 https** ports,  you can see the **api** documentation in this url: http://localhost:5000/swagger

# Running frontend
To run frontend you need to landing in the following directory:
```bash
cd zemoga-blog-engine-app/src/blog-engine-frontend
npm install
npm run start
```
Now you can access to frontend app, in the following url: http://localhost:4200/

The application has setted up to run just following the last two sections.

To logging in, you can use the following users:
  **username:** gtarapues **password:** writer*123
  **username:** pescobar **password:** writer*456
  **username:** orodriguez **password:** editor*123

The options you have in the application are the followings:
## Users Not Authenticated
- **Login**
	to login the system
- **Published Post List** (access for everyone auth and no auth users)
	post list that have been authorized by a **Editor** role user
	- **Show and Send Comments**

## User Authenticated
- **Home**
- **Pending Posts List** (access just for **Editor** Role)
	post list that have been wrote by an **Writer** role user
	- **Approve**
	- **Reject**
	- **Delete**

- **Published Post List** (access for everyone auth and no auth users)
	post list that have been authorized by a **Editor** role user
	- **Show and Send Comments**

- **My Rejected Posts List** (access just for **Writer** Role)
	post list that have been rejected by a **Editor** role user
	- **Update rejected post**

- **Logout**

## Create New Migracion

* In case that you need changing database schema or add new seed data, you need to do the following:
  * landing on src directory.
  * run the following command:

```addMigration
dotnet ef migrations add [NewMigrationName] -p infrastructure -s api -o data/Migrations
```

the new migration will create to src/infrastructure/data/Migrations

When run the application, this migrations will run automatically