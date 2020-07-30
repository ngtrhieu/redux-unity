using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using uRedux.Thunk;
using uRedux.Samples.CoroutineBasedRedditAPI;

namespace uRedux.Tests {
  public class CoroutineBasedRedditAPI {
    [Test]
    public void StoreShouldHaveDefaultState() {
      var store = new RedditStore();
      Assert.AreEqual(null, store.GetState());
    }

    [UnityTest]
    public IEnumerator FetchPostsIfNeeded_ShouldFetchPostAsync() {
      UnityThread.CreateUnityThreadInstance();

      var store = new RedditStore();
      store.Dispatch(Actions.FetchPostsIfNeeded("frontend"));

      yield return new WaitForSeconds(1f);

      var state = store.GetState();
      Assert.NotNull(state.postsBySubreddit);

      Assert.True(state.postsBySubreddit.ContainsKey("frontend"));
      Assert.False(state.postsBySubreddit["frontend"].isFetching);
      Assert.NotNull(state.postsBySubreddit["frontend"].items);
    }
  }
}