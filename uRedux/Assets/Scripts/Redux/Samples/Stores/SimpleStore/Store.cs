
namespace uRedux.Samples.SimpleStore {

  ///<summary>
  ///Just a simple store that contains only an integer.
  ///</summary>
  public class SimpleStore : Store<int> {
    public SimpleStore(int initialState = default(int), params MiddlewareDelegate<int>[] middlewares)
      : base(Reduce, initialState, middlewares) { }

    static int Reduce(int previousState, IAction action) {
      if (action is CountAction) {
        return previousState + 1;
      } else if (action is AddAction) {
        return previousState + ((AddAction)action).amount;
      } else {
        return previousState;
      }
    }
  }
}