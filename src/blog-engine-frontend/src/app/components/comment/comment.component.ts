import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommentService } from 'src/app/services/comment.service';
import { Comment } from 'src/app/core/comment.interface';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {

  commentForm: FormGroup;
    displayedColumns: string[] = ['id', 'detail', 'userId'];
  public comments!: Comment[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: {postId: string},
    public formBuilder: FormBuilder,
    private commentService: CommentService,) {
    this.commentForm = this.formBuilder.group({
      detail: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.loadPendingComments(this.data.postId);
  }

  onSubmit() {
    this.commentService.createComment(this.data.postId!, this.commentForm.value.detail)
      .subscribe(post => {
        this.commentForm.reset();
        this.loadPendingComments(this.data.postId!);
        alert('comment sent');
      }, error => {
        alert('Error creating comment' + error.message);
      });
  }

  loadPendingComments(postId: string) {
    this.commentService.getAllCommentsByPostId(postId)
    .subscribe(comments => {
      this.comments = comments;
    }, error => {
      alert('Error: ' + error.message);
    });
  }

}
