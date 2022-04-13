import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PostPending } from 'src/app/core/post-pending.interface';
import { PostService } from 'src/app/services/post.service';
import { CommentComponent } from '../comment/comment.component';
import { PostComponent } from '../post/post.component';

@Component({
  selector: 'app-post-list-approved',
  templateUrl: './post-list-approved.component.html',
  styleUrls: ['./post-list-approved.component.css']
})
export class PostListApprovedComponent implements OnInit {

  displayedColumns: string[] = ['id', 'title', 'author', 'submitDate', 'comments'];
  public posts!: PostPending[];

  constructor(private postService: PostService, private router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadPendingPost();
  }

  loadPendingPost() {
    this.postService.getAllApprovedPosts()
    .subscribe(posts => {
      this.posts = posts;
    }, error => {
      if(error.status === 401) {
        alert('You are not authorized to view this page');
      } else {
        alert('Error: ' + error.message);
      }
      this.router.navigate(['/']);
    });
  }

  openNewPostDialog(postId: string): void {
    const dialogRef = this.dialog.open(CommentComponent, {
      width: '550px',
      data: { postId },
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadPendingPost();
    });
  }

}
