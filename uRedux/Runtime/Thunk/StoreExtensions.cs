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
    ///Dispatch a Coroutine-based AsyncAction. Return the same action back.
    ///</summary>
    public static CoroutineAction<TState> Dispatch<TState>(this IStore<TState> store, CoroutineAction<TState> action) {
      UnityThread.ExecuteCoroutine(action(store));
      return action;
    }

  }
}