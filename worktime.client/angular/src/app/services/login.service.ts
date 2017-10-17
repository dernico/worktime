import { Injectable } from '@angular/core';
import { UserManager, User as OidcUser, MetadataService, OidcClient } from "oidc-client";
import { Observable } from 'rxjs/Rx';
import { User } from "../models/user";
import { config } from '../authconfig';

@Injectable()
export class LoginService {
  private mgr: UserManager;
  private client: OidcClient;

  constructor() {
    this.mgr = new UserManager(config);
    this.client = new OidcClient(config);

    this.mgr.events.addAccessTokenExpiring(function () {
      console.log("token expiring");
    });

    this.mgr.events.addAccessTokenExpired(function () {
        console.log("token expired");
    });
  }

  private getUser(oidcUser:OidcUser) : User{
    var user = new User();
    user.id_token = oidcUser.id_token;
    return user;
  }

  private silentRenew(){
      this.mgr.signinSilent().then(user => {
        console.log("silent renew successfull");
      } , err => {
        console.log("silent renew failed.");
      });
  }

  login(): Observable<User> {
    return new Observable((observer) => {

      this.mgr.getUser().then(oidcUser => {
        if (oidcUser && !oidcUser.expired) {
          observer.next(this.getUser(oidcUser));
          observer.complete();
        }
        else {
            this.client.createSigninRequest().then(function (req) {
              window.location.href = req.url;
              observer.complete(); // hmm don't think i will reach this point but anyway ..
            }).catch(err => {
              observer.error(err);
            });
        }
      });
    });
  }

  loginOld(): Promise<User> {
    return new Promise((resolve, reject) => {

      this.mgr.getUser().then(oidcUser => {
        if (oidcUser && !oidcUser.expired) {
          resolve(this.getUser(oidcUser));
        }
        else {
            this.client.createSigninRequest().then(function (req) {
              window.location.href = req.url;
            }).catch(err => {
              reject(err);
            });
        }
      });
    });
  }

  logout() {
    this.mgr.removeUser().then(() => {
      //this.userLoadededEvent.emit(null);
      console.log('user removed');
      window.location.href = "http://localhost:4200/index.html";
    }).catch(function (err) {
      console.log(err);
    });
  }
}
