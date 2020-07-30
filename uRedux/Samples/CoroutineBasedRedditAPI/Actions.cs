using System.Collections;
using uRedux.Thunk;

namespace uRedux.Samples.CoroutineBasedRedditAPI {

  ///<summary>
  ///Implementation of actions.js
  ///https://redux.js.org/advanced/example-reddit-api#actionsjs
  ///</summary>
  public static class Actions {

    public struct RequestPosts : IAction {
      public string subreddit;
    }

    public struct ReceivePosts : IAction {
      public string subreddit;
      public string json;
    }

    public struct SelectSubreddit : IAction {
      public string subreddit;
    }

    public struct InvalidateSubreddit : IAction {
      public string subreddit;
    }

    private static IEnumerator FetchPosts(DispatcherDelgate dispatch, string subreddit) {
      dispatch(new RequestPosts() { subreddit = subreddit });

      var request = UnityEngine.Networking.UnityWebRequest.Get($"https://www.reddit.com/r/{subreddit}.json");
      yield return request.SendWebRequest();

      var json = request.downloadHandler.text;

      dispatch(new ReceivePosts() {
        subreddit = subreddit,
        json = json
      });
    }

    private static bool ShouldFetchPosts(RedditState state, string subreddit) {
      if (state == null)
        return true;

      if (!state.postsBySubreddit.TryGetValue(subreddit, out var post))
        return true;

      if (post.isFetching)
        return false;

      return post.didInvalidate;
    }

    private static IEnumerator FetchPostsIfNeededCoroutine(IStore<RedditState> store, string subreddit) {
      if (string.IsNullOrEmpty(subreddit))
        yield break;

      if (ShouldFetchPosts(store.GetState(), subreddit)) {
        yield return FetchPosts(store.Dispatch, subreddit);
      };
    }

    public static CoroutineAction<RedditState> FetchPostsIfNeeded (string subreddit) {
      return store => FetchPostsIfNeededCoroutine(store, subreddit);
    }
  }
}