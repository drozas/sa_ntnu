using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XQUEST.Helpers
{
  /// <summary>
  /// Helper class for drawing 2D text.
  /// </summary>
  public static class TextOut
  {
    private static SpriteFont font;

    /// <summary>
    /// Gets or sets the SpriteFont to use for drawing text.
    /// </summary>
    public static SpriteFont Font
    {
      get { return font; }
      set { font = value; }
    }

    /// <summary>
    /// Initializes the TextOut class with a SpriteFont object.
    /// </summary>
    /// <param name="spriteFont">SpriteFont object for drawing text with.</param>
    public static void Initialize(SpriteFont spriteFont)
    {
      font = spriteFont;
    }

    /// <summary>
    /// Draws simple text, specifying the sprite batch, text, position, and text color.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="textColor">The color of the text.</param>
    public static void DrawText(SpriteBatch spriteBatch, string text, Vector2 position,
                                Color textColor)
    {
      DrawText(spriteBatch, text, position, textColor, 0.0f, Vector2.Zero, Vector2.One,
               SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draws simple text, specifying the sprite batch, text, position, text color, 
    /// and scale.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="textColor">The color of the text.</param>
    /// <param name="scale">The scale of the text</param>
    public static void DrawText(SpriteBatch spriteBatch, string text, Vector2 position,
                                Color textColor, Vector2 scale)
    {
      DrawText(spriteBatch, text, position, textColor, 0.0f, Vector2.Zero, scale,
               SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draws simple text, specifying the sprite batch, text, position, text color,
    /// rotation, origin, scale, sprite effects, and layer depth.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="textColor">The color of the text.</param>
    /// <param name="rotation">The angle of the text, in radians.</param>
    /// <param name="origin">The origin of the string. Specify (0, 0) for the upper-left 
    /// corner.</param>
    /// <param name="scale">The scale of the text.</param>
    /// <param name="effects">The sprite effects of the text.</param>
    /// <param name="layerDepth">The layer depth of the text.</param>
    public static void DrawText(SpriteBatch spriteBatch, string text, Vector2 position,
                                Color textColor, float rotation, Vector2 origin, Vector2 scale,
                                SpriteEffects effects, float layerDepth)
    {
      if (font != null)
      {
        spriteBatch.DrawString(font, text, position, textColor, rotation, origin,
                               scale, effects, layerDepth);
      }
    }

    /// <summary>
    /// Draws simple text, centered, specifying the sprite batch, text, position, 
    /// and text color.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="textColor">The color of the text.</param>
    public static void DrawTextCentered(SpriteBatch spriteBatch, string text,
                                        Vector2 position, Color textColor)
    {
      DrawTextCentered(spriteBatch, text, position, textColor, 0.0f, Vector2.One,
                       SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draws simple text, centered, specifying the sprite batch, text, position,
    /// text color, and scale.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="textColor">The color of the text.</param>
    /// <param name="scale">The scale of the text.</param>
    public static void DrawTextCentered(SpriteBatch spriteBatch, string text,
                                        Vector2 position, Color textColor, Vector2 scale)
    {
      DrawTextCentered(spriteBatch, text, position, textColor, 0.0f, scale,
                       SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draws simple text, centered, specifying the sprite batch, text, position, 
    /// text color, rotation, origin, scale, sprite effects, and layer depth.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="textColor">The color of the text.</param>
    /// <param name="rotation">The angle of the text, in radians.</param>
    /// <param name="scale">The scale of the text.</param>
    /// <param name="effects">The sprite effects of the text.</param>
    /// <param name="layerDepth">The layer depth of the text.</param>
    public static void DrawTextCentered(SpriteBatch spriteBatch, string text,
                                        Vector2 position, Color textColor, float rotation,
                                        Vector2 scale,
                                        SpriteEffects effects, float layerDepth)
    {
      if (font != null)
      {
        Vector2 textSize = font.MeasureString(text);
        Vector2 origin = textSize/2;
        spriteBatch.DrawString(font, text, position, textColor, rotation,
                               origin, scale, effects, layerDepth);
      }
    }

    /// <summary>
    /// Draws shadowed text, specifying the sprite batch, text, position, 
    /// shadow color, and text color.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="shadowColor">The color of the shadow.</param>
    /// <param name="textColor">The color of the text.</param>
    public static void DrawShadowedText(SpriteBatch spriteBatch, string text,
                                        Vector2 position, Color shadowColor, Color textColor)
    {
      DrawShadowedText(spriteBatch, text, position, shadowColor, textColor, 0.0f,
                       Vector2.Zero, Vector2.One, SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draws shadowed text, specifying the sprite batch, text, position, 
    /// shadow color, text color, and scale.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="shadowColor">The color of the shadow.</param>
    /// <param name="textColor">The color of the text.</param>
    /// <param name="scale">The scale of the text.</param>
    public static void DrawShadowedText(SpriteBatch spriteBatch, string text,
                                        Vector2 position, Color shadowColor, Color textColor,
                                        Vector2 scale)
    {
      DrawShadowedText(spriteBatch, text, position, shadowColor, textColor, 0.0f,
                       Vector2.Zero, scale, SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draws shadowed text, specifying the sprite batch, text, position, 
    /// shadow color, text color, rotation, origin, scale, sprite effects, 
    /// and layer depth.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="shadowColor">The color of the shadow</param>
    /// <param name="textColor">The color of the text.</param>
    /// <param name="rotation">The angle of the text, in radians.</param>
    /// <param name="origin">The origin of the string. Specify (0, 0) for the
    /// upper-left corner.</param>
    /// <param name="scale">The scale of the text.</param>
    /// <param name="effects">The sprite effects of the text.</param>
    /// <param name="layerDepth">The layer depth of the text.</param>
    public static void DrawShadowedText(SpriteBatch spriteBatch, string text,
                                        Vector2 position, Color shadowColor, Color textColor,
                                        float rotation,
                                        Vector2 origin, Vector2 scale, SpriteEffects effects,
                                        float layerDepth)
    {
      if (font != null)
      {
        spriteBatch.DrawString(font, text,
                               new Vector2(position.X - 1, position.Y - 1),
                               shadowColor, rotation, origin, scale, effects, layerDepth);

        spriteBatch.DrawString(font, text, position, textColor, rotation,
                               origin, scale, effects, layerDepth);
      }
    }

    /// <summary>
    /// Draws shadowed text, centered, specifying the sprite batch, text, position, 
    /// shadow color, and text color.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="shadowColor">The color of the shadow.</param>
    /// <param name="textColor">The color of the text.</param>
    public static void DrawShadowedTextCentered(SpriteBatch spriteBatch, string text,
                                                Vector2 position, Color shadowColor, Color textColor)
    {
      DrawShadowedTextCentered(spriteBatch, text, position, shadowColor, textColor,
                               0.0f, Vector2.One, SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draws shadowed text, centered, specifying the sprite batch, text, position, 
    /// shadow color, text color, and scale.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="shadowColor">The color of the shadow.</param>
    /// <param name="textColor">The color of the text.</param>
    /// <param name="scale">The scale of the text.</param>
    public static void DrawShadowedTextCentered(SpriteBatch spriteBatch, string text,
                                                Vector2 position, Color shadowColor, Color textColor,
                                                Vector2 scale)
    {
      DrawShadowedTextCentered(spriteBatch, text, position, shadowColor, textColor,
                               0.0f, scale, SpriteEffects.None, 0.0f);
    }

    /// <summary>
    /// Draws shadowed text, centered, specifying the sprite batch, text, position, 
    /// shadow color, text color, rotation, origin, scale, sprite effects, 
    /// and layer depth.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch object to add to.</param>
    /// <param name="text">The text to draw.</param>
    /// <param name="position">The position of the text in screen coordinates.</param>
    /// <param name="shadowColor">The color of the shadow.</param>
    /// <param name="textColor">The color of the text.</param>
    /// <param name="rotation">The angle of the text, in radians.</param>
    /// <param name="scale">The scale of the text.</param>
    /// <param name="effects">The sprite effects of the text.</param>
    /// <param name="layerDepth">The layer depth of the text.</param>
    public static void DrawShadowedTextCentered(SpriteBatch spriteBatch, string text,
                                                Vector2 position, Color shadowColor, Color textColor,
                                                float rotation, Vector2 scale,
                                                SpriteEffects effects, float layerDepth)
    {
      if (font != null)
      {
        Vector2 textSize = font.MeasureString(text);
        Vector2 origin = textSize/2;

        spriteBatch.DrawString(font, text,
                               new Vector2(position.X - 1, position.Y - 1),
                               shadowColor, rotation, origin, scale, effects, layerDepth);

        spriteBatch.DrawString(font, text, position, textColor, rotation,
                               origin, scale, effects, layerDepth);
      }
    }
  }
}