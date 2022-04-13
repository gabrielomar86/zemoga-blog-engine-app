import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommentComponent } from './components/comment/comment.component';
import { LoginComponent } from './components/login/login.component';
import { PostListApprovedComponent } from './components/post-list-approved/post-list-approved.component';
import { PostListPendingComponent } from './components/post-list-pending/post-list-pending.component';
import { PostListRejectedComponent } from './components/post-list-rejected/post-list-rejected.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent },
  {path: 'pending', component: PostListPendingComponent },
  {path: 'approved', component: PostListApprovedComponent },
  {path: 'rejected', component: PostListRejectedComponent },
  {path: '**', redirectTo: 'home', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
