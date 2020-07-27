using System.Threading.Tasks;

namespace uRedux.Thunk {
  
  ///<summary>
  ///An async action
  ///</summary>
  public delegate Task AsyncAction<TState>(DispatcherDelgate dispatch, GetStateDelegate<TState> geState);
}