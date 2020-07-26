using System.Threading.Tasks;

namespace uRedux {
  
  ///<summary>
  ///An async action
  ///</summary>
  public delegate Task AsyncAction<TState>(DispatcherDelgate dispatch, GetStateDelegate<TState> geState);
}