using System;
using System.Collections.Generic;
using System.Text;
using XQUEST.GameObjectManagement;
using XQUEST.Helpers;
using XQUEST.SpriteAnimationFramework;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;

namespace pang_01
{
    public class Character : BasicGameObject
    {
        Weapon currentWeapon = new Weapon();
        int currentLives;
        int score;
        String name;


        //This will change picking items
        public bool doubleShoot = false;
        private float speed = 5.0f;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public bool DoubleShoot
        {
            get { return doubleShoot; }
        }


        public int CurrentLives
        {
            get { return currentLives; }
            set { currentLives = value; }
        }


        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public Weapon CurrentWeapon
        {
            get { return currentWeapon; }
        }

        public Character(Game game, Sprite sprite, Vector2 position, int currentLives)
            : base(game, sprite, position)
        {
            //speed = DefaultSpeed;
            this.velocity = Vector2.Zero;
            this.currentLives = currentLives;
            this.score = 4000;
        }


        public override void Update(GameTime gameTime, InputManager input)
        {
            // Handle input
            if (input.IsKeyOrButtonDown(Keys.Left, Buttons.LeftThumbstickLeft))
            {
                velocity.X = -0.7f;
            }
            else if (input.IsKeyOrButtonDown(Keys.Right, Buttons.LeftThumbstickRight))
            {
                velocity.X = 0.7f;
            }
            else
            {
                velocity.X = 0.0f;
            }
            //if shoot button pressed 
            //game.fireweapon(pos);
            if (input.IsKeyPressed(Keys.Space))
                ((Pang)game).FireWeapon(this);

            position += velocity * speed;


            // Clamp position to screen.
            position =
              Vector2.Clamp(position, Vector2.Zero,
                            new Vector2(game.Window.ClientBounds.Width - sprite.Width,
                                        game.Window.ClientBounds.Height - sprite.Height));


            base.Update(gameTime, input);
        }
    }
}
