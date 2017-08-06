import { Injectable } from '@angular/core';
import { Headers, Http, RequestOptionsArgs } from "@angular/http";
import { LoginService } from "./login.service";
import 'rxjs/add/operator/toPromise';

@Injectable()
export class WebclientService {

    constructor(private http: Http, private loginService: LoginService) { }
    
    get<T>(url: string, options?: RequestOptionsArgs): Promise<T> {
        return new Promise((resolve, reject) => {

            this.loginService.login().then(user => {
                
                if (!options) {
                    options = {}
                    options.headers = new Headers();
                }
                
                options.headers.append('Accept', 'application/json');
                //const bearer = `Bearer ${user.access_token}`;
                const bearer = `Bearer ${user.id_token}`;
                options.headers.append("Authorization", bearer);

                console.log(`get request: ${url}`);
                return this.http
                    .get(url, options)
                    .toPromise()
                    .then((response) => {
                        var json = response.json();
                        resolve(json);
                    })
                    .catch(this.handleError);
            });
        });
    }

    post<T>(url: string, body: any, options?: RequestOptionsArgs): Promise<T> {
        return new Promise((resolve, reject) => {

            this.loginService.login().then(user => {

                if (!options) {
                    options = {}
                    options.headers = new Headers();
                }

                options.headers.append('Accept', 'application/json');
                const bearer = `Bearer ${user.id_token}`;
                options.headers.append("Authorization", bearer);
                
                return this.http
                    .post(url, body, options)
                    .toPromise()
                    .then(resolve)
                    .catch(reject);
            });
        });
    }

    delete<T>(url: string, options?: RequestOptionsArgs): Promise<T> {
        return new Promise((resolve, reject) => {

            this.loginService.login().then(user => {

                if (!options) {
                    options = {}
                    options.headers = new Headers();
                }

                options.headers.append('Accept', 'application/json');
                const bearer = `Bearer ${user.id_token}`;
                options.headers.append("Authorization", bearer);
                
                return this.http
                    .delete(url, options)
                    .toPromise()
                    .then(resolve)
                    .catch(reject);
            });
        });
    }

    private handleError(error: any): Promise<any> {
        //console.error('An error occurred', error); // for demo purposes only
        //return this.loginService.logout();
        return Promise.reject(error.message || error);
    }

}
