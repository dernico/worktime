import { Injectable, Inject } from "@angular/core";
import { Todo } from "../models/todo";
import { WebclientService } from "./webclient.service";
import { Headers, Http } from "@angular/http";

import 'rxjs/add/operator/toPromise';

const _baseUrl = "https://localhost:3000/";


@Injectable()
export class TodoService {

    constructor(private webclient : WebclientService) {}

    loadTodos(): Promise<Todo[]> {

        return this.webclient.get(_baseUrl + "api/values");


        //return new Promise((resolve, reject) => {

        //    this.webclient.get("http://localhost:5001/api/values").then(response => {
        //        var json = response.json();
        //        resolve(json as Todo[]);
        //    })
        //    .catch(this.handleError);
        //});
    }

    addTodo(todo: Todo): Promise<any> {
        
        return this.webclient.post<Todo[]>(_baseUrl + "api/values", todo);

        //return new Promise((resolve, reject) => {

        //    this.loginService.login().then(user => {

        //        const headers = new Headers({ 'Accept': 'application/json' });
        //        const bearer = `Bearer ${user.access_token}`;
        //        headers.append("Authorization", bearer);

        //        return this.http
        //            .post('http://localhost:5001/api/values', todo, { headers: headers })
        //            .toPromise()
        //            .then(resolve)
        //            .catch(this.handleError);
        //    });
        //});
    }

    deleteTodo(id: number) {
        return this.webclient
            .delete("http://localhost:5001/api/values/" + id);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

}
