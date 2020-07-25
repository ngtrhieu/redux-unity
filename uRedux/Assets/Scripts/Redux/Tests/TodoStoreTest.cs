using NUnit.Framework;
using uRedux.Samples.TodoStore;

namespace uRedux.Tests {

  public class TodoStoreTest {
    [Test]
    public void StoreShouldHaveDefaultState() {
      var store = new TodoStore();
      Assert.AreEqual(null, store.GetState());
    }

    [Test]
    public void AddFirstTodoItem() {
      var store = new TodoStore();

      store.Dispatch(new AddTodoAction() { text = "Learn Redux" });

      Assert.AreEqual(EVisibilityFilter.SHOW_ALL, store.GetState().visibilityFilter);
      Assert.AreEqual(1, store.GetState().todos.Count);
      Assert.AreEqual("Learn Redux", store.GetState().todos[0].text);
      Assert.AreEqual(false, store.GetState().todos[0].completed);
    }

    [Test]
    public void AddMoreTodoItems() {
      var store = new TodoStore();

      store.Dispatch(new AddTodoAction() { text = "Learn Redux" });
      store.Dispatch(new AddTodoAction() { text = "Start first project" });
      store.Dispatch(new AddTodoAction() { text = "Ship products" });

      Assert.AreEqual(EVisibilityFilter.SHOW_ALL, store.GetState().visibilityFilter);
      Assert.AreEqual(3, store.GetState().todos.Count);
      Assert.AreEqual("Ship products", store.GetState().todos[2].text);
      Assert.AreEqual(false, store.GetState().todos[2].completed);
    }

    [Test]
    public void ToggleVisibility() {
      var store = new TodoStore();

      store.Dispatch(new SetVisibilityFilterAction() { filter = EVisibilityFilter.SHOW_COMPLETED });
      Assert.AreEqual(EVisibilityFilter.SHOW_COMPLETED, store.GetState().visibilityFilter);
    }

    [Test]
    public void ToggleItemCompletion() {
      var store = new TodoStore();

      store.Dispatch(new AddTodoAction() { text = "Learn Redux" });
      store.Dispatch(new AddTodoAction() { text = "Start first project" });
      store.Dispatch(new AddTodoAction() { text = "Ship products" });
      store.Dispatch(new ToggleTodoAction() { index = 2 });

      Assert.AreEqual(EVisibilityFilter.SHOW_ALL, store.GetState().visibilityFilter);
      Assert.AreEqual(3, store.GetState().todos.Count);
      Assert.AreEqual("Ship products", store.GetState().todos[2].text);
      Assert.AreEqual(true, store.GetState().todos[2].completed);
    }

    [Test]
    public void ToggleItemCompletionWithInvalidIndex() {
      var store = new TodoStore();

      store.Dispatch(new AddTodoAction() { text = "Learn Redux" });
      store.Dispatch(new AddTodoAction() { text = "Start first project" });
      store.Dispatch(new AddTodoAction() { text = "Ship products" });

      store.Dispatch(new ToggleTodoAction() { index = 3 }); // No effects
      store.Dispatch(new ToggleTodoAction() { index = -1 }); // No effects

      Assert.AreEqual(EVisibilityFilter.SHOW_ALL, store.GetState().visibilityFilter);
      Assert.AreEqual(3, store.GetState().todos.Count);
      Assert.AreEqual("Ship products", store.GetState().todos[2].text);
      Assert.AreEqual(false, store.GetState().todos[2].completed);
    }
  }
}