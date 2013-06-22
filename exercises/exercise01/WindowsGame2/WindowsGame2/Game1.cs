using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace task03
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        

      
        int screenWidth;
        int screenHeight;
        const int HELICOPTERS_NUMBER = 3;
        const int N_FRAMES = 4;
        const int FRAME_WIDTH = 130;
       
       
        SpriteFont font;
        ArrayList helicopters = new ArrayList();

        Vector2 position, velocity;
        float rotation, scale;
        
        
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
            velocity = Vector2.One * 2;
            rotation = 0;
            scale = 1;
            position = Vector2.Zero;
            //position = Vector2.One;
            // TODO: Add your initialization logic here
            for (int i = 0; i < HELICOPTERS_NUMBER; i++)
            {
                //position = position * 100 * i;
                helicopters.Add(new AnimatedHelicopter(N_FRAMES,FRAME_WIDTH,position,velocity,rotation,scale));

            }

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
            screenHeight = GraphicsDevice.Viewport.Height;
            screenWidth = GraphicsDevice.Viewport.Width;
            //Set random initial positions for all helicopters
            
            foreach (AnimatedHelicopter a in helicopters)
             {
                 a.setTexture(Content.Load<Texture2D>("anisprite"));
                 a.setRandomPosition(screenWidth, screenHeight);

            }
            font = Content.Load<SpriteFont>("comic");
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

            Vector2 vx = new Vector2(-1, 1);
            Vector2 vy = new Vector2(1, -1);
            Random r= new Random();

            foreach (AnimatedHelicopter a in helicopters)
            {
                a.NextFrame(); //duplicated?
                //if (Keyboard.GetState().IsKeyDown(Keys.M))
                    a.RandomMovement();

                //Width bounce
                if ((a.Position.X + a.FrameWidth > screenWidth) || (a.Position.X < 0))
                {//a.Position += a.Position * -1;
                //a.Position += a.Velocity * vy * r.Next(-3, 3);
                    a.Velocity = a.Velocity * vx;
                }
                //a.setRandomPosition(screenWidth, screenHeight);}
                    //a.Position = a.Position + a.Velocity * -1;

                //Height bounce
                if ((a.Position.Y + a.getTexture().Height > screenHeight) || (a.Position.Y < 0))
                {
                    a.Velocity = a.Velocity * vy;
                    //a.Position += a.Position * vy;
                    //a.Position += a.Velocity * vx * r.Next(-1, 4);
                    //a.setRandomPosition(screenWidth, screenHeight);
                }
                    //a.Position = a.Position + a.Velocity * -1;
                //TODO: Understand this condition!!!!!!!!!!!!!!!!
                if (a.Velocity.X > 0)
                    a.Effect = SpriteEffects.FlipHorizontally;
                else
                    a.Effect = SpriteEffects.None;

                //Next frame
                //a.NextFrame();

                //Setting box
                a.setBoudingBox();

            }
            

            foreach (AnimatedHelicopter a in helicopters)
                foreach (AnimatedHelicopter b in helicopters)
                    if (!(a.Equals(b)) && a.getBoundingBox().Intersects(b.getBoundingBox()))
                        a.Bouncing(b);
                   
                        
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


           
            //Choose a frame from the spritesheet (with upper-left coordinates + height and width)
            Rectangle frameArea;
            int i = 0;
            foreach (AnimatedHelicopter h in helicopters)
            {
                frameArea = new Rectangle(h.FrameWidth * h.CurrentFrame, 0, h.FrameWidth, h.getTexture().Height);
                spriteBatch.Draw(h.getTexture(), h.Position, frameArea, Color.White, h.Rotation, Vector2.Zero, h.Scale, h.Effect, 0);
                spriteBatch.DrawString(font, "Position: " + h.Position.ToString(), Vector2.One * i , Color.White);
                i= i+100;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
