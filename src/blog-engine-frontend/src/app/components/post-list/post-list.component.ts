import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PostPending } from 'src/app/core/post-pending.interface';
import { PostService } from 'src/app/services/post.service';
import { PostComponent } from '../post/post.component';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {

  displayedColumns: string[] = ['id', 'title', 'author', 'submitDate'];
  public posts!: PostPending[];

  constructor(private postService: PostService, private router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadPendingPost();
  }

  openNewPostDialog(): void {
    const dialogRef = this.dialog.open(PostComponent, {
      width: '350px',
      data: {},
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadPendingPost();
    });
  }

  loadPendingPost() {
    this.postService.getAllPendingPosts()
    .subscribe(posts => {
      this.posts = posts;
    }, error => {
      if(error.status === 401) {
        alert('You are not authorized to view this page');
      } else {
        alert('Error: ' + error.message);
      }
      // this.router.navigate(['/']);
    });
  }

}
