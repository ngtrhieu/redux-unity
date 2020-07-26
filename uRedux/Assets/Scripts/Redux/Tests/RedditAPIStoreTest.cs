using UnityEngine;
using NUnit.Framework;
using System.Threading.Tasks;
using uRedux.Samples.RedditAPI;

namespace uRedux.Tests {

  public class RedditAPIStoreStest {
    [Test]
    public void StoreShouldHaveDefaultState() {
      var store = new RedditStore();
      Assert.AreEqual(null, store.GetState());
    }

    [Test]
    public void StoreShouldAddNewSubredditOnRequest() {
      var store = new RedditStore();
      Assert.AreEqual(null, store.GetState());

      store.Dispatch(new Actions.RequestPosts() { subreddit = "frontend" });

      Assert.True(store.GetState().postsBySubreddit.ContainsKey("frontend"));
      Assert.True(store.GetState().postsBySubreddit["frontend"].isFetching);
    }

    [Test]
    public void FetchPostsIfNeeded_ShouldFetchPostAsync() {
      Task.Run(async () => {
        var store = new RedditStore();
        await store.Dispatch(Actions.FetchPostsIfNeeded("frontend"));

        var state = store.GetState();
        Assert.NotNull(state.postsBySubreddit);

        Assert.True(state.postsBySubreddit.ContainsKey("frontend"));
        Assert.False(state.postsBySubreddit["frontend"].isFetching);
        Assert.NotNull(state.postsBySubreddit["frontend"].items);
      }).GetAwaiter().GetResult();
    }
  }
}