import { Injectable } from '@angular/core';
import { ReplaySubject, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Address, User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  loadCurrentUser(token: string | null) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set(
      'Authorization',
      'Bearer z;!v]~R`>u&Wtjbw=q*GV~8e*+<;s2]NFCUSP=%3+q&g{ux3NYM5H5G,x~j*h{WK'
    );

    return this.http.get<User>(this.baseUrl + 'account', { headers }).pipe(
      map((user) => {
        if (user) {
          localStorage.setItem('z;!v]~R`>u&Wtjbw=q*GV~8e*+<;s2]NFCUSP=%3+q&g{ux3NYM5H5G,x~j*h{WK', user.token);
          this.currentUserSource.next(user);
          return user;
        } else {
          return null;
        }
      })
    );
  }

  login(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', values).pipe(
      map((user) => {
        localStorage.setItem('z;!v]~R`>u&Wtjbw=q*GV~8e*+<;s2]NFCUSP=%3+q&g{ux3NYM5H5G,x~j*h{WK', user.token);
        this.currentUserSource.next(user);
      })
    );
  }

  register(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', values).pipe(
      map((user) => {
        localStorage.setItem('z;!v]~R`>u&Wtjbw=q*GV~8e*+<;s2]NFCUSP=%3+q&g{ux3NYM5H5G,x~j*h{WK', user.token);
        this.currentUserSource.next(user);
      })
    );
  }

  logout() {
    localStorage.removeItem('z;!v]~R`>u&Wtjbw=q*GV~8e*+<;s2]NFCUSP=%3+q&g{ux3NYM5H5G,x~j*h{WK');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(
      this.baseUrl + 'account/emailExists?email=' + email
    );
  }

  // getUserAddress(){
  //   return this.http.get<Address>(this.baseUrl+'account/address');
  // }

   updateUserAddress(address:Address){
     return this.http.put<Address>(this.baseUrl+'account/address', address);
   }



}
