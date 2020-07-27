using System.Threading.Tasks;

namespace uRedux.Thunk {

  public static class StoreExtensions {

    ///<summary>
    ///Dispatch an Task-based AsyncAction.
    ///</summary>
    public static Task Dispatch<TState>(this IStore<TState> store, AsyncAction<TState> action) {
      return action.Invoke(store.Dispatch, store.GetState);
    }

  }
}