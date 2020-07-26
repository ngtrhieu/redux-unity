using UnityEngine;

namespace uRedux.Samples.SimpleStore {

  public static class SimpleStoreMiddlewares {

    ///<summary>
    ///An evil middleware that replace all actions to CountAction.
    ///</summary>
    public static MiddlewareDelegate<int> replaceAllActionToCountMiddleware = _ => {
      return dispatcher => action => {
        var newAction = new CountAction();
        dispatcher.Invoke(newAction);
        return newAction;
      };
    };

    ///<summary>
    ///A middleware that modify AddAction amount by 1.
    ///</summary>
    public static MiddlewareDelegate<int> addOneMiddleware = store => dispatcher => action => {
      if (action is AddAction) {
        var addAction = (AddAction)action;
        var newAction = new AddAction() { amount = addAction.amount + 1 };
        dispatcher.Invoke(newAction);
        return newAction as IAction;
      } else {
        dispatcher.Invoke(action);
        return action as IAction;
      }
    };

    ///<summary>
    ///A middleware that double the AddAction amount.
    ///</summary>
    public static MiddlewareDelegate<int> doubleMiddleware = store => dispatcher => action => {
      if (action is AddAction) {
        var addAction = (AddAction)action;
        var newAction = new AddAction() { amount = addAction.amount * 2 };
        dispatcher.Invoke(newAction);
        return newAction as IAction;
      } else {
        dispatcher.Invoke(action);
        return action as IAction;
      }
    };

    ///<summary>
    ///A middleware that log the action out to console.
    ///</summary>
    public static MiddlewareDelegate<int> logMiddleware = store => dispatcher => action => {
      Debug.Log(JsonUtility.ToJson(action));
      dispatcher.Invoke(action);
      return action;
    };

  }

}