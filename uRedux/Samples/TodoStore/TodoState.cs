using System;
using System.Collections.Generic;

namespace uRedux.Samples.TodoStore {

  [Serializable]
  public class TodoItem {
    public string text;
    public bool completed;
  }

  [Serializable]
  public class TodoState {
    public EVisibilityFilter visibilityFilter;
    public List<TodoItem> todos = new List<TodoItem>();
  }

}