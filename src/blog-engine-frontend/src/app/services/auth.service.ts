import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthResponse } from '../core/auth-response.interface';
import { Login } from '../core/login.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  API_URL = `${environment.apiBaseUrl}/auth`;

  constructor(private httpClient: HttpClient) { }

  public login(username: string, password: string): Observable<AuthResponse> {
    const login: Login = { userName: username, password: password };
    return this.httpClient.post<AuthResponse>(this.API_URL, login);
  }

  public isLogged(): boolean {
    return sessionStorage.getItem('TOKEN_KEY') !== null;
  }

}
