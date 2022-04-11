import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PostPending } from '../core/post-pending.interface';
import { Observable } from 'rxjs';
import { PostNew } from '../core/post-new.interface';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private httpClient: HttpClient) { }

  public createPost(post: PostNew): Observable<PostPending> {
    console.warn('====>', post);
    return this.httpClient.post<PostPending>('http://localhost:5000/posts', post);
  }

  public getPostById(id: string): Observable<PostPending> {
    return this.httpClient.get<PostPending>(`http://localhost:5000/posts/${id}`);
  }

  public getAllPosts(): Observable<PostPending> {
    return this.httpClient.get<PostPending>('http://localhost:5000/posts');
  }

  public getAllPendingPosts(): Observable<PostPending[]> {
    return this.httpClient.get<PostPending[]>('http://localhost:5000/posts/pendings');
  }

  public getAllApprovedPosts(): Observable<PostPending[]> {
    return this.httpClient.get<PostPending[]>('http://localhost:5000/posts/approved');
  }

  public getAllRejectedPosts(): Observable<PostPending[]> {
    return this.httpClient.get<PostPending[]>('http://localhost:5000/posts/rejected');
  }
}
