import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {

  postForm: FormGroup;

  constructor(public formBuilder: FormBuilder, private postService: PostService) {
    this.postForm = this.formBuilder.group({
      title: ['', [Validators.required]],
      content: ['', [Validators.required]],
      userId: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {

  }

  onSubmit() {
    this.postService.createPost(this.postForm.value)
      .subscribe(post => {
        console.log('response', post);
      });
  }

}
