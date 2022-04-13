import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;

  constructor(public formBuilder: FormBuilder, private authService: AuthService, private router: Router) {
    this.loginForm = this.formBuilder.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
  }

  onSubmit() {
    sessionStorage.removeItem('TOKEN_KEY');
    this.authService.login(this.loginForm.value.username, this.loginForm.value.password)
      .subscribe(res => {
        sessionStorage.setItem('TOKEN_KEY', res.token);
        this.router.navigate(['/']);
      }, err => {
        alert('Invalid username or password');
      });
  }

}
