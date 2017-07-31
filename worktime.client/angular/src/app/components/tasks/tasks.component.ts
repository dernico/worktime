import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { LoginService } from '../../services/login.service';
import { WebclientService } from '../../services/webclient.service';
import { Todo } from '../../models/todo';

@Component({
    templateUrl: './tasks.component.html',
    styleUrls: ['./tasks.component.css'],
    providers: [TodoService, LoginService, WebclientService]
})
export class TasksComponent implements OnInit {

    private todos: Todo[];
    private todoTitle: string;
    private todoDescription: string;

    constructor(private todoService: TodoService) { }

    newTodo(title, description) {
        const todo = new Todo();
        todo.title = this.todoTitle;
        todo.description = this.todoDescription;

        this.todoService
            .addTodo(todo)
            .then(() => {
                this.todoTitle = "";
                this.todoDescription = "";
                this.init();
            });
    }

    deleteTodo(id: number){
        this.todoService
            .deleteTodo(id)
            .then(() => {
                this.init();
            });
    }

    init() {
        this.todoService
            .loadTodos()
            .then(todos => {
                this.todos = todos;
            });
    }


    ngOnInit() {
        this.init();
    }

}
