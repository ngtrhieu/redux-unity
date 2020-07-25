using System;

namespace uRedux {
  public interface IStore<TState> {

    IAction Dispatch(IAction action);

    TState GetState();

    event Action StageChanged;
  }
}