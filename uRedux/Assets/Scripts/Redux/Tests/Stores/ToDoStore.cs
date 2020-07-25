using System;
using System.Collections.Generic;

namespace uRedux.Tests {

  public enum EVisibilityFilter {
    SHOW_ALL = 0,
    SHOW_COMPLETED,
    SHOW_ACTIVE,
  }

  [Serializable]
  public class TodoItem {
    public string text;
    public bool completed;
  }

  [Serializable]
  public class TodoState {
    public EVisibilityFilter visibilityFilter;
    public List<TodoItem> todos;
  }

  public struct AddTodoAction : IAction {
    public string text;
  }

  public struct ToggleTodoAction : IAction {
    public int index;
  }

  public struct SetVisibilityFilterAction : IAction {
    public EVisibilityFilter filter;
  }

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