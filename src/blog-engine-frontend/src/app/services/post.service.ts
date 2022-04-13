import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PostPending } from '../core/post-pending.interface';
import { Observable } from 'rxjs';
import { PostNew } from '../core/post-new.interface';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  API_URL = `${environment.apiBaseUrl}/posts`;

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

  public approvePost(postId: string): Observable<ArrayBuffer> {
    return this.httpClient.patch(`${this.API_URL}/${postId}/approve`, null, { responseType: 'arraybuffer' });
  }

  public rejectPost(postId: string): Observable<ArrayBuffer> {
    return this.httpClient.patch(`${this.API_URL}/${postId}/reject`, null, { responseType: 'arraybuffer' });
  }
}
