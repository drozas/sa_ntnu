using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace task02
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Task02 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D helicopterTexture;
        Vector2 position;
        Vector2 velocity;

        Helicopter helicopter;

        int screenWidth;
        int screenHeight;

        SpriteFont font;
        Vector2 fontPosition = new Vector2(0, 0);
        Vector2 fontPositionScreen = new Vector2(0, 100);

        float rotation;
        float scale;
        
        public Task02()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the texture for the helicopter
            helicopterTexture = Content.Load<Texture2D>("helicopter");


            font = Content.Load<SpriteFont>("spritePosition");
        

            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            //Draw the helicopter in the middle of the screen
            position = new Vector2((screenWidth - helicopterTexture.Width) / 2,
                (screenHeight - helicopterTexture.Height) / 2);


            velocity = Vector2.One * 2;
            rotation = 0;
            scale = 1;

            helicopter = new Helicopter(position, velocity, rotation, scale);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Back))
                this.Exit();

            //Coordinates of the texture are in the upper-left corner

            //Down movement           
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) &&
                helicopter.Position.Y < (screenHeight - helicopterTexture.Height))
                helicopter.MoveDown();


            //Up movement               
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up)
                && (helicopter.Position.Y > 0))
                helicopter.MoveUp();


            //Right movement           
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right) && 
                (helicopter.Position.X < screenWidth - helicopterTexture.Width))
                helicopter.MoveRight();


            //Left movement           
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) 
                && (helicopter.Position.X> 0))
                helicopter.MoveLeft();


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Chocolate);

            // TODO: Add your drawing code here
            Vector2 newOrigin = position;
            newOrigin.X = helicopterTexture.Width / 2;
            newOrigin.Y = helicopterTexture.Height / 2;

            spriteBatch.Begin(); 
            spriteBatch.DrawString(font,"Position: " + helicopter.Position.ToString(), fontPosition, Color.White);
           
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
                helicopter.Resize();
            }
            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.R))
            {
                helicopter.Rotate();           
            }

            spriteBatch.Draw(helicopterTexture, helicopter.Position, null, Color.White, helicopter.Rotation, Vector2.Zero, helicopter.Scale, SpriteEffects.None, 0);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
