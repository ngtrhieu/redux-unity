
using System;


namespace uRedux {
  public class Store<TState> : IStore<TState> {
    private readonly object syncLock = new object();
    private ReducerDelegate<TState> reducer;
    private readonly DispatcherDelgate dispatcher;
    private TState state;

    public Store(ReducerDelegate<TState> reducer, TState initialState = default(TState), params MiddlewareDelegate<TState>[] middlewares) {
      this.reducer = reducer;
      this.dispatcher = ApplyMiddlewares(middlewares);
      this.state = initialState;
    }

    /// <summary>
    /// Replaces the reducer currently used by the store to calculate the state.
    /// </summary>
    /// <remarks>
    /// This might be useful for implementing hot reloading, dynamically built reducers, and/or alternating the behaviours of reducers.
    /// </remarks>
    public void ReplaceReducer(ReducerDelegate<TState> reducer) {
      this.reducer = reducer;
    }

    /// <summary>
    /// Dispatches an action. This is the only way to trigger a state change.
    /// </summary>
    /// <remarks>
    /// The reducer will be triggered with this action. The returned value will be considered the **next** state.
    /// Subscribers to `StagedChanged` event will be notified.
    /// </remarks>
    /// <returns>Depends on the existing middlewares. With no middlewares, the same action passed in will be returned.</returns>
    public IAction Dispatch(IAction action) {
      return this.dispatcher(action);
    }

    /// <summary>
    /// Read the state tree managed by the store.
    /// </summary>
    /// <returns>The current state tree of your application.</returns>
    public TState GetState() {
      return state;
    }

    private DispatcherDelgate ApplyMiddlewares(params MiddlewareDelegate<TState>[] middlewares) {
      DispatcherDelgate dispatcher = InitialDispatcher;
      foreach (var middleware in middlewares) {
        dispatcher = middleware(this)(dispatcher);
      }
      return dispatcher;
    }

    private IAction InitialDispatcher(IAction action) {
      lock (syncLock) {
        state = reducer(state, action);
      }
      StageChanged?.Invoke();
      return action;
    }

    public event Action StageChanged;
  }
}