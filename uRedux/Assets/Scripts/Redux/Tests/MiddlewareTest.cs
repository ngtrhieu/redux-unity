using UnityEngine;
using NUnit.Framework;

namespace uRedux.Tests {

  public class MiddlewareTest {

    [Test]
    public void MiddlewareShouldBeCalledOnDispatch() {
      var count = 0;

      // This middleware just simply increase count variable
      // whenever Dispatch is count
      MiddlewareDelegate<int> countingMiddleware = _ => {
        return dispatcher => action => {
          ++count;
          dispatcher.Invoke(action);
          return action;
        };
      };

      var store = new SimpleStore(0, countingMiddleware);

      store.Dispatch(new CountAction());
      Assert.AreEqual(1, count);

      store.Dispatch(new AddAction() { amount = 5 });
      Assert.AreEqual(2, count);

      store.Dispatch(new CountAction());
      Assert.AreEqual(3, count);

    }

    [Test]
    public void StoreShouldUseMiddlewareModifiedAction() {

      // This is an evil middleware that basically replace all actions
      // to CountAction instead.
      MiddlewareDelegate<int> replaceAllActionToCountMiddleware = _ => {
        return dispatcher => action => {
          var newAction = new CountAction();
          dispatcher.Invoke(newAction);
          return newAction;
        };
      };

      var store = new SimpleStore(0, replaceAllActionToCountMiddleware);
      Assert.AreEqual(0, store.GetState());

      store.Dispatch(new CountAction());
      Assert.AreEqual(1, store.GetState());

      store.Dispatch(new AddAction() { amount = 5 });
      Assert.AreEqual(2, store.GetState());

      // Even on actions that normally not recognized
      store.Dispatch(new UnrecognizedAction());
      Assert.AreEqual(3, store.GetState());
    }

    [Test]
    public void MiddlewareEffectsShouldCompound() {
      MiddlewareDelegate<int> addOneMiddleware = _ => {
        return dispatcher => action => {
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
      };

      MiddlewareDelegate<int> doubleMiddleware = _ => {
        return dispatcher => action => {
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
      };

      MiddlewareDelegate<int> logMiddleware = _ => {
        return dispatcher => action => {
          Debug.Log(JsonUtility.ToJson(action));
          dispatcher.Invoke(action);
          return action;
        };
      };

      var store = new SimpleStore(0, addOneMiddleware, doubleMiddleware, logMiddleware);
      Assert.AreEqual(0, store.GetState());

      store.Dispatch(new CountAction());
      Assert.AreEqual(1, store.GetState());

      // AddAction +5 would be modified as +1 then x2, therefore ultimately become AddAction +12
      store.Dispatch(new AddAction() { amount = 5 });
      Assert.AreEqual(13, store.GetState());

      // These middlewares only target AddActions
      store.Dispatch(new UnrecognizedAction());
      Assert.AreEqual(13, store.GetState());
    }

  }
}