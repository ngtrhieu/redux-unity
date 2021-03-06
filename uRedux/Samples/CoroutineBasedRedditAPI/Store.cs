
namespace uRedux.Samples.CoroutineBasedRedditAPI {

  public class RedditStore : Store<RedditState> {
    public RedditStore(RedditState initialState = default(RedditState), params MiddlewareDelegate<RedditState>[] middlewares)
      : base(RedditReducer.Reduce, initialState, middlewares) { }
  }
}