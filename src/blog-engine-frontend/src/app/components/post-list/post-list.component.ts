import { Component, OnInit } from '@angular/core';
import { PostPending } from 'src/app/core/post-pending.interface';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.css']
})
export class PostListComponent implements OnInit {

  displayedColumns: string[] = ['id', 'title', 'author', 'submitDate'];
  public posts!: PostPending[];

  constructor(private postService: PostService) { }

  ngOnInit(): void {
    this.postService.getAllPendingPosts()
      .subscribe(posts => {
        this.posts = posts;
      });
  }

}
