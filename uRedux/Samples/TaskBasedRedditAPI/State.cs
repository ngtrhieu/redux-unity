using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace uRedux.Samples.TaskBasedRedditAPI {

  public class RedditState {
    public Dictionary<string, SubredditPost> postsBySubreddit = new Dictionary<string, SubredditPost>();
    public string selectedSubreddit;
  }

  public class SubredditPost {
    public bool isFetching = false;
    public bool didInvalidate = false;
    public DateTime lastUpdated = DateTime.Now;
    public JObject items = new JObject();
  }

}