using System;

namespace uRedux {
  public delegate IAction DispatcherDelgate(IAction action);
  public delegate Func<DispatcherDelgate, DispatcherDelgate> MiddlewareDelegate<TState>(IStore<TState> store);
  public delegate TState ReducerDelegate<TState>(TState previousState, IAction action);
}