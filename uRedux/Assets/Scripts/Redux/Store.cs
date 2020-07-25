
using System;


namespace uRedux {
  public class Store<TState> : IStore<TState> {
    public event Action StageChanged;

    private readonly ReducerDelegate<TState> reducer;
    private readonly DispatcherDelgate dispatcher;
    private TState state;

    public Store(ReducerDelegate<TState> reducer, TState initialState = default(TState), params MiddlewareDelegate<TState>[] middlewares) {
      this.reducer = reducer;
      this.dispatcher = ApplyMiddlewares(middlewares);
      this.state = initialState;
    }

    public IAction Dispatch(IAction action) {
      return this.dispatcher(action);
    }

    public TState GetState() {
      return state;
    }

    private DispatcherDelgate ApplyMiddlewares(params MiddlewareDelegate<TState>[] middlewares) {
      DispatcherDelgate dispatcher = InnerDispatcher;
      foreach (var middleware in middlewares) {
        dispatcher = middleware(this)(dispatcher);
      }
      return dispatcher;
    }

    private readonly object syncLock = new object();

    private IAction InnerDispatcher(IAction action) {
      lock (syncLock) {
        state = reducer(state, action);
      }
      StageChanged?.Invoke();
      return action;
    }
  }
}