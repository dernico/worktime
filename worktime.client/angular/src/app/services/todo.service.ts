import { Injectable, Inject } from "@angular/core";
import { WorkEntry } from "../models/workentry";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

const _baseUrl = "http://localhost:3000/";


@Injectable()
export class TodoService {

    constructor(private webclient : HttpClient) {}

    loadTodos(): Observable<WorkEntry[]> {

        return this.webclient.get<WorkEntry[]>(_baseUrl + "api/workentry");
    }

    addTodo(todo: WorkEntry): Observable<ArrayBuffer> {
        return this.webclient.post<ArrayBuffer>(_baseUrl + "api/workentry", todo);
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
