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
    public class Ball : BasicGameObject, ICollidable
    {
        public static readonly float DefaultSpeed = 4.0f;
        public static readonly Vector2 DefaultVelocity = Vector2.One;
        private Vector2 speed;
        private int size;
        const int BONUS_DESTROYED_BALL = 100;
        private int bonus;

        public Ball(Game game, Sprite sprite, Vector2 position, int ballSize)
            : base(game, sprite, position)
        {
            speed.X = DefaultSpeed;
            speed.Y = DefaultSpeed;
            velocity = DefaultVelocity;
            size = ballSize;
            bonus = BONUS_DESTROYED_BALL / ballSize;
        }
        public Ball(Game game, Sprite sprite, int ballSize)
            : base(game, sprite, new Vector2(0))
        {
            speed.X = DefaultSpeed;
            speed.Y = DefaultSpeed;
            int aux = (RandomHelper.RandomInt(0, 2));
            if (Convert.ToBoolean(aux))
            {
                velocity = Vector2.One;
            }
            else
            {
                velocity = new Vector2(-1, 1);
            }
            //velocity = DefaultVelocity;
            size = ballSize;
            bonus = BONUS_DESTROYED_BALL / ballSize;
            position = RandomHelper.RandomVector2(100f, 700f, 50f, 200f);
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        // Implemented method from the ICollidable interface.
        public void OnCollision(IGameObject collisionObject)
        {
            // Find the point of intersection and set the velocity accordingly.
            CollisionIntersectionPoint intersectionPoint =
                CollisionIntersection.GetIntersectionPoint(this, collisionObject);

            if (collisionObject is Character)
            {
                //Death case 
                ((Pang)game).CharacterHit(this);
            }
            if (collisionObject is Shot)
            {
                //Remove ball and maybe create two smaller
                ((Pang)game).Shot(this, (Shot)collisionObject);

            }

        }

        public override void Update(GameTime gameTime, InputManager input)
        {
            if (position.X < 0.0f || position.X > game.Window.ClientBounds.Width - sprite.Width)
            {
                while (position.X > game.Window.ClientBounds.Width - sprite.Width)
                    position.X -= 0.1f * velocity.X;

                velocity.X *= -1.0f;
            }
            if (position.Y > game.Window.ClientBounds.Height - sprite.Height)
            {
                while (position.Y > game.Window.ClientBounds.Height - sprite.Height)
                    position.Y -= 0.1f;
                velocity.Y *= -1.0f;
                speed.Y = 9 + size;
            }

            speed.Y += 0.01f * velocity.Y * gameTime.ElapsedGameTime.Milliseconds;

            position += speed * velocity;

            base.Update(gameTime, input);
        }

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }


        public int Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }
    }
}
