using System.Threading.Tasks;
using System.Net.Http;

namespace uRedux.Samples.RedditAPI {

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

    private static async Task FetchPosts(DispatcherDelgate dispatch, string subreddit) {
      dispatch(new RequestPosts() { subreddit = subreddit });

      var client = new HttpClient();
      var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"https://www.reddit.com/r/{subreddit}.json"));
      var json = await response.Content.ReadAsStringAsync();

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

    public static uRedux.Thunk.AsyncAction<RedditState> FetchPostsIfNeeded(string subreddit) {
      return async (dispatch, getState) => {
        if (string.IsNullOrEmpty(subreddit))
          return;

        if (ShouldFetchPosts(getState(), subreddit)) {
          await FetchPosts(dispatch, subreddit);
        }
      };
    }
  }
}