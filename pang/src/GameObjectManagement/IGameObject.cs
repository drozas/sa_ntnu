using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XQUEST.Helpers;

namespace XQUEST.GameObjectManagement
{
  /// <summary>
  /// The IGameObject interface provides the most basic functionality for a game object. 
  /// The three vectors position, velocity, and acceleration are used to simulate simple physics. 
  /// Normally, the game object will follow these basic rules know from physics:
  /// 
  /// velocity += acceleration;
  /// position += velocity;
  /// 
  /// First, the acceleration vector is added to the velocity vector. Then the velocity vector 
  /// is added to the position vector. The position vector will now be updated to represent the 
  /// physical position of the game object on the screen if these two statements are run every 
  /// frame in the game loop.
  /// The BoundingRectangle is a struct that is part of the XNA framework. It provides a logical 
  /// representation of the box that encapsulates the game object. In a 2D game, we can consider 
  /// this as a rectangle instead. The bounding box (or rectangle) is used to check for collisions 
  /// between game objects. If their bounding boxes intersect, we know that there is a collision. 
  /// The BoundingRectangle struct handily provides methods to check for intersections along with a few 
  /// other convenient methods. Using a bounding may however not be optimal for every game object. 
  /// Irregular-shaped game objects may better represent their bounding volume using a bounding 
  /// sphere, which translates to a circle in 2D. Another option is to use per-pixel collision 
  /// detection, which means we check for collisions on the pixel level of the game objects. To 
  /// achieve this, the pixels that represent the shape of the game object sprite must be separated 
  /// from those that are not part of the shape, normally setting those pixels to be transparent or 
  /// use some agreed-upon color value. Per-pixel collision detection is quite a complicated subject 
  /// and although it is the most precise method of checking collisions, it cannot match the performance 
  /// of simpler algorithms like bounding boxes or bounding spheres. We chose to go with the bounding box,
  /// as it is very simple to implement and generally yields better per-formance. Note however that 
  /// this field is ignored if the game object does not implement ICollidable. The user can choose to 
  /// implement their own collision detection algorithms but still be able to use the GameObjectManager 
  /// to handle the game objects.
  /// The State property is an enumerated value indicating which state the game object is in. 
  /// The GameObjectState enum has three possible values; alive, dying, and dead. Alive means 
  /// that the game object is visible, should have its logic updated and be drawn. Dying indicates 
  /// that the game object is about to die. This is useful if we want to display some kind of death 
  /// animation or explosion before we remove the game object from the scene. Setting the state to 
  /// Dead will immediately remove the game object from the GameObjectManager and it will no longer be visible.
  /// </summary>
  public interface IGameObject
  {
    /// <summary>
    /// Gets or sets the position vector of the Game Object.
    /// </summary>
    Vector2 Position
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the velocity vector of the Game Object.
    /// </summary>
    Vector2 Velocity
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the acceleration vector of the Game Object.
    /// </summary>
    Vector2 Acceleration
    {
      get;
      set;
    }

    /// <summary>
    /// Gets the bounding box of the Game Object.
    /// </summary>
    Rectangle BoundingRectangle
    {
      get;
    }

    /// <summary>
    /// Gets or sets the state of the game object.
    /// </summary>
    GameObjectState State
    {
      get;
      set;
    }

    /// <summary>
    /// Updates the Game Object's logic. Should be called every frame.
    /// </summary>
    /// <param name="gameTime">The GameTime instance from the Application Framework.</param> 
    void Update(GameTime gameTime, InputManager input);

    /// <summary>
    /// Draws the Game Object.
    /// </summary>
    /// <param name="spriteBatch">The SpriteBatch to use in drawing this Game Object.</param>
    void Draw(SpriteBatch spriteBatch);
  }
}