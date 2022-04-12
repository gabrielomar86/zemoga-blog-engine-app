import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'blog-engine-frontend';

  loadingStatus$!: Observable<boolean>;

  constructor(private loadingService: LoadingService) { }

  ngOnInit(): void {
    this.loadingStatus$ = this.loadingService.listenLoading();
  }

}
