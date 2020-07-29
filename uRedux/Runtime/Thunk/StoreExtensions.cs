using UnityEngine;
using System.Threading.Tasks;

namespace uRedux.Thunk {

  public static class StoreExtensions {

    ///<summary>
    ///Dispatch an Task-based AsyncAction.
    ///</summary>
    public static Task Dispatch<TState>(this IStore<TState> store, AsyncAction<TState> action) {
      return action.Invoke(store);
    }

    ///<summary>
    ///Dispatch a coroutine AsyncAction.
    ///</summary>
    public static Coroutine Dispatch<TState>(this IStore<TState> store, CoroutineAction<TState> action) {
      return null; //UnityEngine.UnityThread.ExecuteCoroutine(action);
    }

  }
}