using System;
using System.Collections.Generic;
using System.Text;

using XQUEST.GameObjectManagement;
using XQUEST.Helpers;
using XQUEST.SpriteAnimationFramework;
using Microsoft.Xna.Framework;
namespace pang_01

{
  /// <summary>
  /// The ball gameobject inherits BasicGameObject and 
  /// implements the ICollidable interface for checking collisions
  /// against the bricks or the paddle.
  /// </summary>
  public class Shot : BasicGameObject, ICollidable
  {
    public static readonly float DefaultSpeed = 18.0f;

      public static readonly Vector2 DefaultVelocity = new Vector2(0.0f,-1.0f);
    private float speed;
      Character character;


    public Shot(Game game, Sprite sprite, Vector2 position, Character character)
      : base(game, sprite, position)
    {
      speed = DefaultSpeed;
      velocity = DefaultVelocity;
      //position = shotInitialPosition;
      this.character = character;
     
    }

      
    // Implemented method from the ICollidable interface.
    public void OnCollision(IGameObject collisionObject)
    {
      // Find the point of intersection and set the velocity accordingly.
      CollisionIntersectionPoint intersectionPoint =
          CollisionIntersection.GetIntersectionPoint(this, collisionObject);
        
          if (collisionObject is Ball)
          {
              /*if size<=0
                  un-Draw Ball;
              else
                paint 2 balls of size n-1;*/
              ((Pang)game).RemoveObject(this);

          }
      
    }

    public override void Update(GameTime gameTime, InputManager input)
    {
        position += velocity * speed;

      // Bounce off the sides and the top.
          if (position.Y < 0.0f)
            ((Pang)game).RemoveObject(this);

      base.Update(gameTime, input);
    }

    public float Speed
    {
      get { return speed; }
      set { speed = value; }
    }


      public Character Character
      {
          get { return character; }
          set { character = value; }
      }
  }
}

