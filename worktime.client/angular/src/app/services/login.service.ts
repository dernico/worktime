import { Injectable } from '@angular/core';
import { UserManager, User, MetadataService, OidcClient } from "oidc-client";
import { config } from '../authconfig';

@Injectable()
export class LoginService {
  private mgr: UserManager;
  private client: OidcClient;
  private user: User;

  constructor() {
    this.mgr = new UserManager(config);
    this.client = new OidcClient(config);
  }

  login(): Promise<User> {
    return new Promise((resolve, reject) => {

      if (this.user) {
        resolve(this.user);
        return;
      }


      this.mgr.getUser().then(user => {
        if (user) {
          this.user = user;
          resolve(user);
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
