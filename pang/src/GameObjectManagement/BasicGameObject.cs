using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XQUEST.Helpers;
using XQUEST.SpriteAnimationFramework;

namespace XQUEST.GameObjectManagement
{
  /// <summary>
  /// The BasicGameObject class is a basic implementation of the IGameObject interface. 
  /// We decided to provide this not only as an example to show how to implement a 
  /// game object, but also to combine it with the sprite framework. The implementation 
  /// contains a Sprite object from which we calculate the bounding rectangle. 
  /// The Draw method simply calls the draw method of the sprite along with the position 
  /// vector. Also, if the sprite is an AnimatedSprite, the Update method makes sure to 
  /// call UpdateAnimation on the sprite. BasicGameObject also contains a reference to the 
  /// game instance in case the game object needs access to any components or services 
  /// registered with the game. A BasicGameObject is automatically added to the 
  /// GameObjectManager upon creation if the GameObjectManager has been registered as a
  /// game component using Components.Add in the game class. 
  /// </summary>
  public abstract class BasicGameObject : IGameObject
  {
    protected Sprite sprite;
    protected Vector2 position;
    protected Vector2 velocity;
    protected Vector2 acceleration;

    protected Rectangle boundingRectangle;

    protected GameObjectState state;

    protected Game game;

    /// <summary>
    /// Creates a new BasicGameObject.
    /// </summary>
    /// <param name="game">The game instance.</param>
    /// <param name="sprite">Sprite representing this game object.</param>
    /// <param name="position">Initial position of the game object.</param>
    public BasicGameObject(Game game, Sprite sprite, Vector2 position)
    {
      this.game = game;
      this.sprite = sprite;
      this.position = position;

      state = GameObjectState.Alive;

      boundingRectangle = new Rectangle();
    }

    public virtual void Update(GameTime gameTime, InputManager input)
    {
      // Update the bounding rectangle
      boundingRectangle.X = (int)(position.X - sprite.Origin.X*sprite.Scale.X);
      boundingRectangle.Y = (int)(position.Y - sprite.Origin.Y*sprite.Scale.Y);
      boundingRectangle.Width = (int) (sprite.Width*sprite.Scale.X);
      boundingRectangle.Height = (int) (sprite.Height*sprite.Scale.Y);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      sprite.Draw(spriteBatch, position);
    }

    /// <summary>
    /// Virtual method for drawing the GameObject. Basically, all it does
    /// is draw the Sprite involved with this GameObject. Override it in the
    /// derived class if you want more control over how it is drawn.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch to use in drawing this
    /// GameObject.</param>
    /// <param name="drawPosition">The position to draw the GameObject. 
    /// Usually this is some transformation of the GameObjects own position.</param>
    public virtual void Draw(SpriteBatch spriteBatch, Vector2 drawPosition)
    {
      sprite.Draw(spriteBatch, drawPosition);
    }

    /// <summary>
    /// Gets or sets the Sprite representing this GameObject.
    /// </summary>
    public Sprite Sprite
    {
      get { return sprite; }
      set { sprite = value; }
    }

    /// <summary>
    /// Gets or sets the position vector.
    /// </summary>
    public Vector2 Position
    {
      get { return position; }
      set { position = value; }
    }

    /// <summary>
    /// Gets or sets the velocity vector.
    /// </summary>
    public Vector2 Velocity
    {
      get { return velocity; }
      set { velocity = value; }
    }

    /// <summary>
    /// Gets or sets the acceleration vector.
    /// </summary>
    public Vector2 Acceleration
    {
      get { return acceleration; }
      set { acceleration = value; }
    }

    /// <summary>
    /// Gets or set the BoundingRectangle.
    /// </summary>
    public Rectangle BoundingRectangle
    {
      get { return boundingRectangle; }
      set { boundingRectangle = value; }
    }

    /// <summary>
    /// Gets or sets the state of the game object.
    /// </summary>
    public GameObjectState State
    {
      get { return state; }
      set { state = value; }
    }

    /// <summary>
    /// Gets the Game instance.
    /// </summary>
    public Game Game
    {
      get { return game; }
    }
  }
}