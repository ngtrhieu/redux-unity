using System;

namespace uRedux {
  public interface IStore<TState> {
    /// <summary>
    /// Dispatch an action to the store.
    /// </summary>
    /// <param name="action">
    /// The action to dispatch.
    /// </param>
    /// <returns>
    /// Depends on existing store middlewares. With no middlewares, returns the same action that was passed into.
    /// </returns>
    IAction Dispatch(IAction action);

    /// <summary>
    /// Get the current state.
    /// </summary>
    /// <return>
    /// The current state tree.
    /// <returns>
    TState GetState();

    event Action StageChanged;
  }
}