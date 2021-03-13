using System;

namespace JakeUtils {
  public interface INotifier<T> {
    event Action<T> Signalled;
    void Signal(T t);
  }
}
