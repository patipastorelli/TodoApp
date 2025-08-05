
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Todo } from '../models/todo.model';
import { TodoService } from '../services/todo.service';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-todo-list',
  standalone: false,
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {
  todos: Todo[] = [];
  newTodo: string = '';

  constructor(private todoService: TodoService) { }

  ngOnInit() {
    this.loadTodos();
  }

  loadTodos() {
    this.todoService.getTodos().subscribe((data: Todo[]) => {
      this.todos = data;
      // this.cdr.detectChanges();
    });
  }

  addTodo() {
    if (!this.newTodo.trim()) return;
    this.todoService.addTodo(this.newTodo).subscribe((todo: any) => {
      this.todos.push(todo);
      this.newTodo = '';
      // this.cdr.detectChanges();
    });
  }

  deleteTodo(id: number) {
    this.todoService.deleteTodo(id).subscribe(() => {
      this.todos = this.todos.filter(t => t.id !== id);
      // this.cdr.detectChanges();
    });
  }
}
