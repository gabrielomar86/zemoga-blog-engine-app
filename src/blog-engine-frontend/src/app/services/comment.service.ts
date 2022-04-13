import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CommentNew } from '../core/comment-new.interface';
import { Comment } from '../core/comment.interface';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  API_URL = `${environment.apiBaseUrl}/comments`;

  constructor(private httpClient: HttpClient) { }

  public createComment(postId: string, detail: string): Observable<Comment> {
    const comment: CommentNew = { detail: detail };
    console.log('comment: ' + JSON.stringify(comment));
    return this.httpClient.post<Comment>(`${this.API_URL}/${postId}`, comment);
  }

  public getAllCommentsByPostId(postId: string): Observable<Comment[]> {
    return this.httpClient.get<Comment[]>(`${this.API_URL}/${postId}`);
  }

}
