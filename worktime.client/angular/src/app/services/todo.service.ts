import { Injectable, Inject } from "@angular/core";
import { WorkEntry } from "../models/workentry";
import { WebclientService } from "./webclient.service";
import { Headers, Http } from "@angular/http";

import 'rxjs/add/operator/toPromise';

const _baseUrl = "http://localhost:3000/";


@Injectable()
export class TodoService {

    constructor(private webclient : WebclientService) {}

    loadTodos(): Promise<WorkEntry[]> {

        return this.webclient.get(_baseUrl + "api/workentry");
    }

    addTodo(todo: WorkEntry): Promise<any> {
        
        return this.webclient.post<WorkEntry[]>(_baseUrl + "api/workentry", todo);
    }

    deleteTodo(id: number) {
        return this.webclient
            .delete(_baseUrl + "api/workentry/" + id);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

}
