using System.Collections.Generic;
using System.Linq;

namespace JakeUtils {
  public static class IEnumerableExt {
    public static IEnumerable<T>
    NotNull<T>(this IEnumerable<T> source) where T : class =>
      source.Where(x => null != x);

    public static IEnumerable<T>
    Cons<T>(this IEnumerable<T> source, T e) {
      yield return e;
      foreach(var x in source) {
        yield return x;
      }
    }
  }
}
