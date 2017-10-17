import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { LoginService } from "./login.service";
import { User } from "../models/user";
 
@Injectable()
export class HttpInterceptorService implements HttpInterceptor {
  constructor(private loginService: LoginService) {}
 
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return this
      .loginService
      .login()
      // with flatmap i transform the login response into the HttpResponse 
      .flatMap(user => {
        const authHeader = this.getAuthHeader(user);
        // Clone the request to add the new header.
        const authReq = req.clone({headers: req.headers.set('Authorization', authHeader)});
        // Pass on the cloned request instead of the original request.
        return next.handle(authReq);
      });

  }

  private getAuthHeader(user: User) : string{
      //return `Bearer ${user.access_token}`;
      return `Bearer ${user.id_token}`;
  }

}