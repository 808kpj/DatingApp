import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {
  private http = inject(HttpClient);

  baseUrl = environment.apiUrl;

  currentUser = signal<User | null>(null);


  login(model:any)
  {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map( user => {
        if(user){
          //saving the users information to the local storage, only for learning not safe for production in the slightest.
          this.setCurrentUser(user);
        }
      })
    )
  }

  register(model:any)
  {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map( user => {
        if(user){
          //saving the users information to the local storage, only for learning not safe for production in the slightest.
          this.setCurrentUser(user);
        }
        return user;
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUser.set(user);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }


}
