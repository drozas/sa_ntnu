using System;
using Microsoft.Xna.Framework;

namespace XQUEST.Helpers
{
  /// <summary>
  /// Provides methods for getting random numbers in several
  /// formats.
  /// </summary>
  public static class RandomHelper
  {
    private static readonly Random random = new Random();

    /// <summary>
    /// Gets a random integer in the specified range.
    /// </summary>
    /// <param name="min">Minimum value of the random number (inclusive).</param>
    /// <param name="max">Maximum value of the random number (exslusive).</param>
    /// <returns>A random integer in the specified range.</returns>
    public static int RandomInt(int min, int max)
    {
      return random.Next(min, max);
    }

    /// <summary>
    /// Gets a random floating point value in the specified range.
    /// </summary>
    /// <param name="min">Minimum value of the random number (inclusive).</param>
    /// <param name="max">Maximum value of the random number (exslusive).</param>
    /// <returns>A random floating point value in the specified range.</returns>
    public static float RandomFloat(float min, float max)
    {
      return min + (max-min)*(float) random.NextDouble();
    }

    /// <summary>
    /// Gets a random Vector2 with components' values in the specified range.
    /// </summary>
    /// <param name="min">Minimum value for the components of the Vector2 (inclusive).</param>
    /// <param name="max">Maximum value for the components of the Vector2 (exslusive).</param>
    /// <returns>A random Vector2 with components' values in the specified range.</returns>
    public static Vector2 RandomVector2(float min, float max)
    {
      return new Vector2(RandomFloat(min, max), RandomFloat(min, max));
    }

    /// <summary>
    /// Gets a random Vector2 with components' values in the specified range.
    /// </summary>
    /// <param name="minX">Minimum value for the X component (inclusive).</param>
    /// <param name="maxX">Maximum value for the X component (exslusive).</param>
    /// <param name="minY">Minimum value for the Y component (inclusive).</param>
    /// <param name="maxY">Maximum value for the Y component (exslusive).</param>
    /// <returns>A random Vector2 with components' values in the specified range.</returns>
    public static Vector2 RandomVector2(float minX, float maxX, float minY, float maxY)
    {
      return new Vector2(RandomFloat(minX, maxX), RandomFloat(minY, maxY));
    }

    /// <summary>
    /// Gets a random Vector2 with components' values in the specified range.
    /// </summary>
    /// <param name="min">Minimum value of the random Vector2 (inclusive).</param>
    /// <param name="max">Maximum value of the random Vector2 (exslusive).</param>
    /// <returns>A random Vector2 with components' values in the specified range.</returns>
    public static Vector2 RandomVector2(Vector2 min, Vector2 max)
    {
      return new Vector2(RandomFloat(min.X, max.X), RandomFloat(min.Y, max.Y));
    }

    /// <summary>
    /// Gets a random Vector3 with components' values in the specified range.
    /// </summary>
    /// <param name="min">Minimum value for the components of the Vector3 (inclusive).</param>
    /// <param name="max">Maximum value for the components of the Vector3 (exslusive).</param>
    /// <returns>A random Vector3 with components' values in the specified range.</returns>
    public static Vector3 RandomVector3(float min, float max)
    {
      return new Vector3(RandomFloat(min, max), RandomFloat(min, max), RandomFloat(min, max));
    }

    /// <summary>
    /// Gets a random Vector3 with components' values in the specified range.
    /// </summary>
    /// <param name="minX">Minimum value for the X component (inclusive).</param>
    /// <param name="maxX">Maximum value for the X component (exslusive).</param>
    /// <param name="minY">Minimum value for the Y component (inclusive).</param>
    /// <param name="maxY">Maximum value for the Y component (exslusive).</param>
    /// <param name="minZ">Minimum value for the Z component (inclusive).</param>
    /// <param name="maxZ">Maximum value for the Z component (exslusive).</param>
    /// <returns>A random Vector3 with components' values in the specified range.</returns>
    public static Vector3 RandomVector3(float minX, float maxX, float minY, float maxY, float minZ,
                                        float maxZ)
    {
      return new Vector3(RandomFloat(minX, maxX), RandomFloat(minY, maxY), RandomFloat(minZ, maxZ));
    }

    /// <summary>
    /// Gets a random Vector3 with components' values in the specified range.
    /// </summary>
    /// <param name="min">Minimum value of the random Vector3 (inclusive).</param>
    /// <param name="max">Maximum value of the random Vector3 (exslusive).</param>
    /// <returns>A random Vector2 with components' values in the specified range.</returns>
    public static Vector3 RandomVector3(Vector3 min, Vector3 max)
    {
      return new Vector3(RandomFloat(min.X, max.X), RandomFloat(min.Y, max.Y), RandomFloat(min.Z, max.Z));
    }
  }
}