using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace XQUEST.Helpers
{
  /// <summary>
  /// Stores Texture2D objects in a dictionary. The purpose is to make sure 
  /// textures are loaded only once to prevent memory waste. The class is static,
  /// so can be used anywhere in your code to access textures.
  /// Use the Add method to add textures to the store, and then later
  /// when you want to access it, use the get method with the same name
  /// as you specified when you added it. 
  /// 
  /// 
  /// Example:
  /// TextureStore.Add("cat", content.Load<Texture2D>(@"textures\cat"));
  /// ...then later when you need to draw it:
  /// spriteBatch.Draw(TextureStore.Get("cat"), ...);
  /// 
  /// </summary>
  public static class TextureStore
  {
    // Dictionary of Texture2D objects, keyed by a string describing the object.
    private static Dictionary<string, Texture2D> textures =
      new Dictionary<string, Texture2D>();

    /// <summary>
    /// Gets the Texture2D object with the specified name for the TextureStore
    /// </summary>
    /// <param name="name">Name of the texture to get.</param>
    /// <returns></returns>
    public static Texture2D Get(string name)
    {
      return textures[name];
    }

    /// <summary>
    /// Adds a texture to the TextureStore
    /// </summary>
    /// <param name="textureName">Name of the texture.</param>
    /// <param name="spriteTexture">The Texture2D object to store.</param>
    public static void Add(string textureName, Texture2D spriteTexture)
    {
      textures[textureName] = spriteTexture;
    }

    /// <summary>
    /// Clears the TextureStore
    /// </summary>
    public static void Clear()
    {
      textures.Clear();
    }

    /// <summary>
    /// Gets the number of textures currently in this TextureStore.
    /// </summary>
    public static int Count
    {
      get { return textures.Count; }
    }
  }
}