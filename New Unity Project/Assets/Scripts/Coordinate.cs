using System;

public enum Coordinate {X, Y, Z}

public static class CoordinateExt {
  public static R Eliminate<R>(this Coordinate a, Func<R> ifX, Func<R> ifY, Func<R> ifZ) {
    switch(a) {
    case Coordinate.X:
      return ifX();
    case Coordinate.Y:
      return ifY();
    case Coordinate.Z:
      return ifZ();
    default:
      throw new Exception("impossible");
    }
  }
}
