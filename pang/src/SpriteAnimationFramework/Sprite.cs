using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XQUEST.SpriteAnimationFramework
{
  /// <summary>
  /// The Sprite class encapsulates an XNA Texture2D object and in
  /// addition provides sprite operations such as rotation and scaling.
  /// </summary>
  public class Sprite
  {
    protected Texture2D spriteTexture;
    protected Rectangle sourceRect;
    protected float rotation;
    protected Vector2 origin;
    protected Vector2 scale;
    protected SpriteEffects effects;
    protected float layerDepth;
    protected Color tint;

    /// <summary>
    /// Creates a new sprite from the given texture.
    /// The entire texture is used as one single sprite.
    /// </summary>
    /// <param name="spriteTexture">Texture which hold the sprite data.</param>
    public Sprite(Texture2D spriteTexture)
      : this(spriteTexture,
             new Rectangle(0, 0, spriteTexture.Width, spriteTexture.Height))
    {
    }

    /// <summary>
    /// Creates a new sprite from the given texture and source rectangle.
    /// The rectangle tells which part of the texture will comprise the sprite.
    /// </summary>
    /// <param name="spriteTexture">Texture which hold the sprite data.</param>
    /// <param name="sourceRect">Area of texture to use for this sprite.</param>
    public Sprite(Texture2D spriteTexture, Rectangle sourceRect)
    {
      this.spriteTexture = spriteTexture;
      this.sourceRect = sourceRect;

      effects = SpriteEffects.None;
      rotation = 0.0f;
      origin = Vector2.Zero;
      scale = new Vector2(1.0f);
      layerDepth = 0.0f;
      tint = Color.White;
    }

    /// <summary>
    /// Draws the sprite.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
    /// <param name="position">The screen position you want to draw the sprite to.</param>
    public void Draw(SpriteBatch spriteBatch, Vector2 position)
    {
      spriteBatch.Draw(spriteTexture, position, sourceRect, tint, rotation,
                       origin, scale, effects, layerDepth);
    }

    /// <summary>
    /// Gets or sets the scale of the sprite.
    /// </summary>
    public Vector2 Scale
    {
      get { return scale; }
      set { scale = value; }
    }

    /// <summary>
    /// Gets or sets the origin of the sprite. The origin is the point on the sprite 
    /// that will be mapped to a screen position once drawn. The default value is 
    /// top left corner of the sprite.
    /// </summary>
    public Vector2 Origin
    {
      get { return origin; }
      set { origin = value; }
    }

    /// <summary>
    /// Gets or sets the rotation value of the sprite in radians.
    /// </summary>
    public float Rotation
    {
      get { return rotation; }
      set { rotation = value; }
    }

    /// <summary>
    /// Gets or sets the source rectangle. The source rectangle specifies which portion
    /// of the original XNA Texture2D object constitutes this sprite.
    /// </summary>
    public Rectangle SourceRect
    {
      get { return sourceRect; }
      set { sourceRect = value; }
    }

    /// <summary>
    /// Sets the original XNA Texture2D object.
    /// </summary>
    public Texture2D SpriteTexture
    {
      get { return spriteTexture; }
      set { spriteTexture = value; }
    }

    /// <summary>
    /// Gets the width of the sprite.
    /// </summary>
    public int Width
    {
      get { return sourceRect.Width; }
    }

    /// <summary>
    /// Gets the height of the sprite.
    /// </summary>
    public int Height
    {
      get { return sourceRect.Height; }
    }

    /// <summary>
    /// Gets or sets the sprite effects.
    /// </summary>
    public SpriteEffects Effects
    {
      get { return effects; }
      set { effects = value; }
    }

    /// <summary>
    /// Gets or sets the layer depth (0.0 - 1.0).
    /// </summary>
    public float LayerDepth
    {
      get { return layerDepth; }
      set { layerDepth = value; }
    }

    /// <summary>
    /// Gets or set the color of the sprite.
    /// </summary>
    public Color Tint
    {
      get { return tint; }
      set { tint = value; }
    }

    /// <summary>
    /// Gets the center point of the sprite.
    /// </summary>
    public Vector2 Center
    {
      get { return new Vector2(sourceRect.Width/2, sourceRect.Height/2); }
    }
  }
}