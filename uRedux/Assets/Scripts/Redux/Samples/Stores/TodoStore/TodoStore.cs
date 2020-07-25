using System;
using System.Collections.Generic;

namespace uRedux.Samples.TodoStore {

  public class TodoStore : Store<TodoState> {
    public TodoStore(TodoState initialState = default(TodoState), params MiddlewareDelegate<TodoState>[] middlewares)
      : base(Reduce, initialState, middlewares) { }

    static TodoState Reduce(TodoState state, IAction action) {
      if (state == null) {
        state = new TodoState();
      }

      if (action is AddTodoAction) {
        var addAction = (AddTodoAction)action;
        if (state.todos == null)
          state.todos = new List<TodoItem>();
        state.todos.Add(new TodoItem() { text = addAction.text });

      } else if (action is ToggleTodoAction) {
        var toggleAction = (ToggleTodoAction)action;
        if (state.todos != null && toggleAction.index < state.todos.Count && toggleAction.index >= 0) {
          state.todos[toggleAction.index].completed = !state.todos[toggleAction.index].completed;
        }

      } else if (action is SetVisibilityFilterAction) {
        var setFilterAction = (SetVisibilityFilterAction)action;
        state.visibilityFilter = setFilterAction.filter;
      }

      return state;
    }
  }
}