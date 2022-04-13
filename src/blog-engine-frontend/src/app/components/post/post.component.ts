import { Component, Inject, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PostUpdate } from 'src/app/core/post-update.interface';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  postForm: FormGroup;
  isUpdating = false;

  constructor(
    public formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: PostUpdate,
    private postService: PostService) {

    this.postForm = this.formBuilder.group({
      title: ['', [Validators.required]],
      content: ['', [Validators.required]],
    });

    if (data) {
      this.isUpdating = true;
      this.postForm.patchValue(data);
    }
  }

  ngOnInit(): void {

  }

  onSubmit() {
    if (this.isUpdating) {
      this.postService.updatePost(this.data.id, this.postForm.value)
      .subscribe(post => {
        this.postForm.reset();
        alert('post updated');
      }, error => {
        alert('Error updating post' + error.message);
      });
  } else {
      this.postService.createPost(this.postForm.value)
        .subscribe(post => {
          this.postForm.reset();
          alert('post created');
        }, error => {
          alert('Error creating post' + error.message);
        });
    }
  }

}
