using UnityEngine;

namespace JakeUtils {
  public static class RandomExt {
    public static float NextFloat(this System.Random r) =>
      (float)r.NextDouble();

    /**
    * \brief
    * Computes a random value from the standard normal distribution,
    * using sampling from a uniform distribution.
    */
    public static float NextGaussianDouble(this System.Random r)
    {
        float u, v, S;

        do
        {
            u = 2.0f * r.NextFloat() - 1.0f;
            v = 2.0f * r.NextFloat() - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        return u * fac;
    }

    /**
    * \brief
    * Computes a random number from a gaussian distribution with mean
    * (max + min)/2 and standard deviation such that max is three
    * stdevs away from the mean.
    */
    public static float
    NextBoundedGaussian(this System.Random r, float min, float max) {
      var mean = (min + max) /2;
      var stdev = (max - mean) /3;
      return r.NextGaussianDouble() * stdev + mean;
    }
  }
}
