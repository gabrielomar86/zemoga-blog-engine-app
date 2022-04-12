import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PostPending } from '../core/post-pending.interface';
import { Observable } from 'rxjs';
import { PostNew } from '../core/post-new.interface';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  API_URL = 'http://localhost:5000/posts';

  constructor(private httpClient: HttpClient) { }

  public createPost(post: PostNew): Observable<PostPending> {
    return this.httpClient.post<PostPending>(this.API_URL, post);
  }

  public getPostById(id: string): Observable<PostPending> {
    return this.httpClient.get<PostPending>(`${this.API_URL}/${id}`);
  }

  public getAllPosts(): Observable<PostPending> {
    return this.httpClient.get<PostPending>(this.API_URL);
  }

  public getAllPendingPosts(): Observable<PostPending[]> {
    return this.httpClient.get<PostPending[]>(`${this.API_URL}/pendings`);
  }

  public getAllApprovedPosts(): Observable<PostPending[]> {
    return this.httpClient.get<PostPending[]>(`${this.API_URL}/approved`);
  }

  public getAllRejectedPosts(): Observable<PostPending[]> {
    return this.httpClient.get<PostPending[]>(`${this.API_URL}/rejected`);
  }
}
