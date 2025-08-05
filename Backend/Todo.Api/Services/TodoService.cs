
using System;
using System.Collections.Generic;
using System.Linq;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService
    {
        private readonly List<TodoItem> _todos = new();
        private int _nextId = 1;

        public IEnumerable<TodoItem> GetAll() => _todos;

        public void Add(TodoItem item) {
            item.Id = _nextId++;
            _todos.Add(item);
            }

        public void Delete(int id) => _todos.RemoveAll(t => t.Id == id);
    }
}
