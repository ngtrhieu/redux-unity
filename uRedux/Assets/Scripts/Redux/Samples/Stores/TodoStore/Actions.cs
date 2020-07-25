
namespace uRedux.Samples.TodoStore {

  public enum EVisibilityFilter {
    SHOW_ALL = 0,
    SHOW_COMPLETED,
    SHOW_ACTIVE,
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

}