using System;

namespace UnityEngine.Networking {

  public static class UnityWebRequestExtensions {
    public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOperation) {
      return new UnityWebRequestAwaiter(asyncOperation);
    }
  }

  // https://gist.github.com/krzys-h/9062552e33dd7bd7fe4a6c12db109a1a
  public class UnityWebRequestAwaiter : System.Runtime.CompilerServices.INotifyCompletion {
    private UnityWebRequestAsyncOperation asyncOperation;
    private Action continuation;

    public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOperation) {
      this.asyncOperation = asyncOperation;
      asyncOperation.completed += _ => this.continuation.Invoke();
    }

    public bool IsCompleted { get => asyncOperation.isDone; }

    public void GetResult() { }

    public void OnCompleted(Action continuation) {
      this.continuation = continuation;
    }
  }


}