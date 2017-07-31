import { Injectable } from '@angular/core';
import { UserManager, User, MetadataService, OidcClient } from "oidc-client";

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
    this.client.createSignoutRequest().then(req => {
      this.mgr.removeUser().then(() => {
        window.location.href = req.url;
      });
    });
  }
}


const config: any = {
  authority: "https://accounts.google.com",
  client_id: "793729558350-58etvsoelqbc8pi5lknlven67esr03vh.apps.googleusercontent.com",
  client_secret: "jjgBcDv28Uqz-VaEueBX4Gwb",
  redirect_uri: "http://localhost:4200/callback.html",
  response_type: "id_token token", // id_token token / id_token / token / code
  scope: "openid profile",
  post_logout_redirect_uri: "http://localhost:4200/index.html",
};
