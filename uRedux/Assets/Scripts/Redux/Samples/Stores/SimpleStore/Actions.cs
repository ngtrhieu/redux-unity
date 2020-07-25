
namespace uRedux.Samples.SimpleStore {

  ///<summary>
  ///Increase the state by 1.
  ///</summary>
  public struct CountAction : IAction { }

  ///<summary>
  ///Increase the state by an arbitrary amount.
  ///</summary>
  public struct AddAction : IAction {
    public int amount;
  }

  ///<summary>
  ///Stub to an action that is not recognized by the reducer.
  ///</summary>
  public struct UnrecognizedAction : IAction { }

}