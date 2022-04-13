import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PostComponent } from './components/post/post.component';
import { AuthService } from './services/auth.service';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'blog-engine-frontend';
  baseurl = environment.apiBaseUrl;
  loadingStatus$!: Observable<boolean>;

  constructor(private loadingService: LoadingService, private authService: AuthService, private router: Router, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadingStatus$ = this.loadingService.listenLoading();
  }

  logout() {
    sessionStorage.removeItem('TOKEN_KEY');
    this.router.navigate(['/']);
  }

  isLogged(): boolean {
    return this.authService.isLogged();
  }

  openNewPostDialog(): void {
    const dialogRef = this.dialog.open(PostComponent, {
      width: '350px',
      data: undefined,
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

}
