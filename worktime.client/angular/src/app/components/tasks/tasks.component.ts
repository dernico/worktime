import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo.service';
import { LoginService } from '../../services/login.service';
import { WorkEntry } from '../../models/workentry';

@Component({
    templateUrl: './tasks.component.html',
    styleUrls: ['./tasks.component.css'],
    providers: [TodoService]
})
export class TasksComponent implements OnInit {

    public todos: WorkEntry[];
    public todoTitle: string;
    public todoDescription: string;
    public todoStartDate: string;
    public todoStartTime: string;
    public todoEndDate: string;
    public todoEndTime: string;

    constructor(private todoService: TodoService) { 
        this.todoStartDate = new Date(Date.now()).toISOString();
        this.todoEndDate = new Date(Date.now()).toISOString();
    }

    newTodo() {
        const todo = new WorkEntry();
        todo.title = this.todoTitle;
        todo.description = this.todoDescription;

        var startdate = new Date(this.todoStartDate + " " + this.todoStartTime);
        todo.startTime = startdate;

        var enddate = new Date(this.todoEndDate + " " + this.todoEndTime);
        todo.endTime = enddate;

        this.todoService
            .addTodo(todo)
            .subscribe(() => {
                this.todoTitle = "";
                this.todoDescription = "";
                this.todoStartDate = null;
                this.todoEndDate = null;
                this.init();
            });
    }

    deleteTodo(id: number){
        this.todoService
            .deleteTodo(id)
            .subscribe(() => {
                this.init();
            });
    }

    init() {
        this.todoService
            .loadTodos()
            .subscribe(todos => {
                this.todos = todos;
            });
    }


    ngOnInit() {
        this.init();
    }

}
