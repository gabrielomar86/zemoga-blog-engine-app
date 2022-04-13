import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { PostListPendingComponent } from './components/post-list-pending/post-list-pending.component';
import { PostComponent } from './components/post/post.component';
import { CommentListComponent } from './components/comment-list/comment-list.component';
import { CommentComponent } from './components/comment/comment.component';
import { AngularMaterialModule } from './angular-material.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { NgxLoadingModule } from 'ngx-loading';
import { loadingInterceptorProviders } from './interceptors/loading.interceptor';
import { authInterceptorProviders } from './interceptors/auth.interceptor';
import { LoginComponent } from './components/login/login.component';
import { PostListApprovedComponent } from './components/post-list-approved/post-list-approved.component';
import { PostListRejectedComponent } from './components/post-list-rejected/post-list-rejected.component';

@NgModule({
  declarations: [
    AppComponent,
    PostListPendingComponent,
    PostListApprovedComponent,
    PostListRejectedComponent,
    PostComponent,
    CommentListComponent,
    CommentComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    NgxLoadingModule.forRoot({}),
  ],
  providers: [
    loadingInterceptorProviders,
    authInterceptorProviders
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
