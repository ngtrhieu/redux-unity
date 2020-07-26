using System;
using System.Threading.Tasks;

namespace uRedux {
  public delegate IAction DispatcherDelgate(IAction action);
  public delegate Func<DispatcherDelgate, DispatcherDelgate> MiddlewareDelegate<TState>(IStore<TState> store);
  public delegate TState ReducerDelegate<TState>(TState previousState, IAction action);
  public delegate TState GetStateDelegate<TState>();
}