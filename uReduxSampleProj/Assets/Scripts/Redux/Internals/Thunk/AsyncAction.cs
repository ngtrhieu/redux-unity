using System.Collections;
using System.Threading.Tasks;

namespace uRedux.Thunk {

  ///<summary>
  ///A task-based async action
  ///</summary>
  public delegate Task AsyncAction<TState>(IStore<TState> store);

  ///<summary>
  ///An Unity coroutine-based async action
  ///</summary>
  public delegate IEnumerator CoroutineAction<TState>(IStore<TState> store);
}