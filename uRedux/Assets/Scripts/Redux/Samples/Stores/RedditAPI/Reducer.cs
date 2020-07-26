using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace uRedux.Samples.RedditAPI {

  ///<summary>
  ///Implementation of the reducers described at  
  ///https://redux.js.org/advanced/example-reddit-api#reducersjs
  ///</summary>
  public static class RedditReducer {

    public static RedditState Reduce(RedditState state, IAction action) {
      if (state == null) {
        state = new RedditState();
      }

      // combineReducer
      state.selectedSubreddit = SelectedSubredditReducer(state.selectedSubreddit, action);
      state.postsBySubreddit = PostsBySubredditReducer(state.postsBySubreddit, action);

      return state;
    }

    public static string SelectedSubredditReducer(string state, IAction action) {
      if (action is Actions.SelectSubreddit) {
        return ((Actions.SelectSubreddit)action).subreddit;
      }
      return state;
    }

    // To be reused as a cache list.
    // Avoid creating a new list every time the PostsBySubredditReducer is called.
    private static List<string> _subRedditPosts = new List<string>();

    public static Dictionary<string, SubredditPost> PostsBySubredditReducer(Dictionary<string, SubredditPost> state, IAction action) {

      // Create a new subreddit if not yet exist
      if (action is Actions.InvalidateSubreddit) {
        var invalidAction = (Actions.InvalidateSubreddit)action;
        if (!state.ContainsKey(invalidAction.subreddit))
          state.Add(invalidAction.subreddit, new SubredditPost());

      } else if (action is Actions.RequestPosts) {
        var requestAction = (Actions.RequestPosts)action;
        if (!state.ContainsKey(requestAction.subreddit))
          state.Add(requestAction.subreddit, new SubredditPost());

      } else if (action is Actions.ReceivePosts) {
        var receiveAction = (Actions.ReceivePosts)action;
        if (!state.ContainsKey(receiveAction.subreddit))
          state.Add(receiveAction.subreddit, new SubredditPost());

      } else if (action is Actions.SelectSubreddit) {
        var selectAction = (Actions.SelectSubreddit)action;
        if (!state.ContainsKey(selectAction.subreddit))
          state.Add(selectAction.subreddit, new SubredditPost());
      }

      // Get a copy of all keys in the state
      _subRedditPosts.Clear();
      _subRedditPosts.AddRange(state.Keys);

      // call PostReducer for each subreddit item
      foreach (var subreddit in _subRedditPosts) {
        state[subreddit] = PostReducer(state[subreddit], action);
      }

      return state;
    }

    private static SubredditPost PostReducer(SubredditPost state, IAction action) {
      if (state == null) {
        state = new SubredditPost();
      }

      if (action is Actions.InvalidateSubreddit) {
        state.didInvalidate = true;

      } else if (action is Actions.RequestPosts) {
        state.isFetching = true;
        state.didInvalidate = false;

      } else if (action is Actions.ReceivePosts) {
        state.isFetching = false;
        state.didInvalidate = false;
        state.lastUpdated = System.DateTime.Now;
        state.items = JObject.Parse(((Actions.ReceivePosts)action).json);
      }

      return state;
    }
  }
}