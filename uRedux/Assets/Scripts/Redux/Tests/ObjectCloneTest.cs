using System.Collections.Generic;
using NUnit.Framework;
using uRedux.Samples.SimpleStore;
using uRedux.Samples.TodoStore;

namespace uRedux.Tests {

  public class CloneTest {
    [Test]
    public void BuiltinPrimitiveCloneTest() {
      // bool
      Assert.AreEqual(false, false.Copy());
      Assert.AreEqual(true, true.Copy());

      // byte
      Assert.AreEqual((byte)0, ((byte)0).Copy());

      // char
      Assert.AreEqual('c', 'c'.Copy());
      Assert.AreEqual('!', '!'.Copy());

      // int
      Assert.AreEqual(5, 5.Copy());
      Assert.AreEqual(-10, -10.Copy());

      // float
      Assert.AreEqual(3.14f, 3.14f.Copy());
      UnityEngine.Assertions.Assert.AreApproximatelyEqual(-8.88f, -8.88f.Copy());
    }

    [Test]
    public void ArrayCloneTest() {
      var original = new int[] { 1, 2, 3 };
      var cloned = original.Copy();

      Assert.AreEqual(original, cloned);
      Assert.AreNotSame(original, cloned);
    }

    [Test]
    public void DictionaryCloneTest() {
      var original = new Dictionary<string, int> { { "first", 1 }, { "second", 2 }, { "third", 3 } };
      var cloned = original.Copy();

      Assert.AreEqual(original, cloned);
      Assert.AreNotSame(original, cloned);
    }

    [Test]
    public void ListCloneTest() {
      var original = new List<string> { "gotta", "go", "learn", "Redux" };
      var cloned = original.Copy();

      Assert.AreEqual(original, cloned);
      Assert.AreNotSame(original, cloned);
    }

    [Test]
    public void StructCloneTest() {
      var original = new AddAction() { amount = 5 };
      var cloned = original.Copy();

      Assert.AreEqual(original, cloned);
      Assert.AreNotSame(original, cloned);
    }

    [Test]
    public void ObjectCloneTest() {
      var original = new TodoState();
      var cloned = original.Copy();

      Assert.AreEqual(UnityEngine.JsonUtility.ToJson(original), UnityEngine.JsonUtility.ToJson(cloned));
      Assert.AreNotSame(original, cloned);
    }
  }
}