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

namespace task04
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D paddle1;
        Vector2 position1;
        Vector2 velocity1;


        Texture2D paddle2;
        Vector2 position2;
        Vector2 velocity2;

        Texture2D ball;
        Vector2 positionBall;
        Vector2 velocityBall;

        int screenWidth;
        int screenHeight;

        SpriteFont font;
        SpriteFont fontScreen;
        Vector2 fontPosition = new Vector2(0, 0);

        Random rBall = new Random();

        public Game1()
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

            // TODO: use this.Content to load your game content here
            //Load the texture for the helicopter
            paddle1 = Content.Load<Texture2D>("paddle");
            paddle2 = Content.Load<Texture2D>("paddle");
            ball = Content.Load<Texture2D>("ball");
            //We keep the screen width and height
            screenWidth = GraphicsDevice.Viewport.Width;
            screenHeight = GraphicsDevice.Viewport.Height;

            //Draw the helicopter in the middle of the screen
            positionBall = new Vector2((screenWidth - ball.Width) / 2,
                (screenHeight - ball.Height) / 2);

            position1 = new Vector2(0, (screenHeight - paddle1.Height) / 2);
            position2 = new Vector2(screenWidth - paddle2.Width, (screenHeight - paddle2.Height) / 2);

            
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //Movement for player 1
            //Up movement           
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) && (position1.Y ) > 0)
                position1.Y = position1.Y - 2;


            //Down movement           
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) && position1.Y < (screenHeight - (paddle1.Height)))

                position1.Y = position1.Y + 2;




            //Movement for player 2
            //Up movement           
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.A) && (position2.Y) > 0)
                position2.Y = position2.Y - 2;


            //Down movement           
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Z) && position2.Y < (screenHeight - (paddle2.Height)))
                position2.Y = position2.Y + 2;


            //velocityBall = velocityBall * rBall.Next(-10, 10);
            //positionBall = positionBall + velocityBall;
            positionBall.X++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(paddle1, position1, Color.White);

            spriteBatch.Draw(paddle2, position2, Color.White);
            spriteBatch.Draw(ball, positionBall, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
