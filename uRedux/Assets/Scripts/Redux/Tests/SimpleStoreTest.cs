using NUnit.Framework;

namespace uRedux.Tests {

  public class SimpleStoreTest {
    [Test]
    public void StoreShouldHaveDefaultState() {
      var store = new SimpleStore();
      Assert.AreEqual(0, store.GetState());
    }

    [Test]
    public void StoreShouldHaveInitialState() {
      var store = new SimpleStore(5);
      Assert.AreEqual(5, store.GetState());
    }

    [Test]
    public void StageShouldChangeAfterCountAction() {
      var store = new SimpleStore();

      store.Dispatch(new CountAction());
      Assert.AreEqual(1, store.GetState());

      store.Dispatch(new CountAction());
      Assert.AreEqual(2, store.GetState());
    }

    [Test]
    public void StageShouldChangeAfterAddAction() {
      var store = new SimpleStore();

      store.Dispatch(new CountAction());
      Assert.AreEqual(1, store.GetState());

      store.Dispatch(new AddAction() { amount = 5 });
      Assert.AreEqual(6, store.GetState());
    }

    [Test]
    public void StageShouldNotChangeAfterUnrecognizedAction() {
      var store = new SimpleStore();

      store.Dispatch(new UnrecognizedAction());
      Assert.AreEqual(0, store.GetState());

      store.Dispatch(new UnrecognizedAction());
      Assert.AreEqual(0, store.GetState());
    }

  }
}