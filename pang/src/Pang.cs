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
using XQUEST.GameObjectManagement;
using XQUEST.Helpers;
using XQUEST.SpriteAnimationFramework;



namespace pang_01
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Pang : Game
    {
        // The possible game states.
        private enum GameState
        {
            Menu,
            GameOver,
            Playing,
            GameWon,
            NextStage,
            RestartStage,
            HighScores,
            Credits,
            NewHighscore
        }

        //The possible menu elements
        private enum MenuElement
        {
            StartGame,
            HighScore,
            Credits,
            Exit
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private GameState currentState;
        private MenuElement currentMenuElement;
        private SpriteFont fontMedium;
        private InputManager input;
        private FileManager fileManager = new FileManager();
        private GameObjectManager gameObjectManager;

        KeyboardState[] oldkeystates;
        String playerName;
        Character character;

        // GameObjectManager game component.


        private Sprite[] ballSprites;
        private Sprite shotSprite;
        private Sprite characterSprite;
        Texture2D characterTexture;

        // Screen dimensions.
        private int screenWidth;
        private int screenHeight;
        private Texture2D backgroundTexture;
        private Rectangle backgroundRectangle;

        private List<Ball> balls;



        double pauseTimer = 0.0f;
        private int currentStage;
        private HighScores highscores;

        const int TOTAL_STAGES = 7;
        const int INITIAL_LIVES = 2;
        const int BONUS_NEXT_STAGE = 100;
        const int BONUS_END_GAME = 1000;
        public const int MAXPLAYERNAMESTRING = 3;
        public const int MAXINHIGHSCORELIST = 5;

        // AudioManager game component.
        private AudioManager audio;

        public Pang()
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
            //XQuest initialization
            // Intitialize game components.
            //RestartGameValues();
            input = new InputManager(this);
            gameObjectManager = new GameObjectManager(this);


            // We add them to the Components collection, so that the 
            // game component is automatically handled for us.
            Components.Add(input);
            Components.Add(gameObjectManager);
            this.currentState = GameState.Menu;
            currentStage = 1;
            playerName = "";

            audio = new AudioManager(this, Content.RootDirectory + "/AudioProject.xgs",
                               Content.RootDirectory + "/Wave Bank.xwb",
                               Content.RootDirectory + "/Sound Bank.xsb");

            Components.Add(audio);
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
            KeyboardState[] oldkeystates = input.CurrentKeyboardStates;

            // TODO: use this.Content to load your game content here

            // Get screen dimensions.
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;
            highscores = new HighScores(fileManager);

            //Load Consolas font for use in the menu
            fontMedium = Content.Load<SpriteFont>("Consolas");

            // Load Arial font, and use it for the TextOut class.
            SpriteFont font = Content.Load<SpriteFont>("Arial");
            TextOut.Initialize(font);

            characterTexture = Content.Load<Texture2D>("character");
            Texture2D ballTexture1 = Content.Load<Texture2D>("ball1");
            Texture2D ballTexture2 = Content.Load<Texture2D>("ball2");
            Texture2D ballTexture3 = Content.Load<Texture2D>("ball3");
            Texture2D shotTexture = Content.Load<Texture2D>("shot");

            backgroundTexture = Content.Load<Texture2D>("stage01");
            backgroundRectangle = new Rectangle
                 (0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            characterSprite = new Sprite(characterTexture);
            shotSprite = new Sprite(shotTexture);

            ballSprites = new Sprite[4];
            ballSprites[1] = new Sprite(ballTexture1);
            ballSprites[2] = new Sprite(ballTexture2);
            ballSprites[3] = new Sprite(ballTexture3);


            Vector2 characterInitialPos = new Vector2(screenWidth / 2 - (characterTexture.Width / 2), screenHeight);
            character = new Character(this, characterSprite, characterInitialPos, INITIAL_LIVES);




            // Add all textures to the TextureStore.
            TextureStore.Add("character", characterTexture);
            TextureStore.Add("ball1", ballTexture1);
            TextureStore.Add("ball2", ballTexture2);
            TextureStore.Add("ball3", ballTexture3);
            TextureStore.Add("shot", shotTexture);

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
            if (input.IsKeyOrButtonDown(Keys.Escape, Buttons.Back))
                Exit();

            //TODO: Implement actions which perform the logic of the game
            switch (currentState)
            {
                case GameState.Menu:
                    if (input.IsKeyPressed(Keys.Up) && currentMenuElement != MenuElement.StartGame)
                        currentMenuElement--;
                    else if (input.IsKeyPressed(Keys.Down) && currentMenuElement != MenuElement.Exit)
                        currentMenuElement++;
                    else if (input.IsKeyPressed(Keys.Enter))
                    {
                        switch (currentMenuElement)
                        {
                            case MenuElement.StartGame:
                                RestartGameValues();
                                LoadStage();
                                currentState = GameState.Playing;
                                break;
                            case MenuElement.HighScore:
                                currentState = GameState.HighScores;
                                break;
                            case MenuElement.Credits:
                                currentState = GameState.Credits;
                                break;
                            case MenuElement.Exit:
                                Exit();
                                break;
                        }
                    }
                    break;
                case GameState.Playing:
                    break;
                case GameState.RestartStage:
                    pauseTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    gameObjectManager.Clear();
                    if (pauseTimer > 2.0f)
                    {
                        //After 3 sec showing the text, go to menu
                        LoadStage();
                        this.currentState = GameState.Playing;
                        pauseTimer = 0.0f;
                    }
                    break;
                case GameState.NextStage:
                    pauseTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    gameObjectManager.Clear();
                    if (pauseTimer > 3.0f)
                    {
                        //After 3 sec showing the text, go to menu
                        currentStage++;
                        if (currentStage > TOTAL_STAGES)
                        {
                            character.Score += BONUS_END_GAME * character.CurrentLives;
                            //End of the game
                            this.currentState = GameState.GameWon;
                        }
                        else
                        {
                            character.Score += BONUS_NEXT_STAGE * character.CurrentLives;
                            //Load next stage
                            LoadStage();
                            this.currentState = GameState.Playing;
                        }
                        pauseTimer = 0.0f;
                    }
                    break;
                case GameState.GameWon:
                case GameState.GameOver:
                    pauseTimer += gameTime.ElapsedGameTime.TotalSeconds;

                    gameObjectManager.Clear();
                    if (pauseTimer > 2.0f)
                    {
                        if (character.Score > highscores.getLowest())
                            this.currentState = GameState.NewHighscore;
                        else
                            this.currentState = GameState.Menu;

                        pauseTimer = 0.0f;
                    }
                    //call high scores
                    break;
                case GameState.Credits:
                case GameState.HighScores:
                    if (input.IsKeyPressed(Keys.Back))
                        this.currentState = GameState.Menu;
                    break;
                case GameState.NewHighscore:
                    break;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.SaveState);
            gameObjectManager.Draw(spriteBatch);

            switch (currentState)
            {
                case GameState.Playing:
                    TextOut.DrawTextCentered(spriteBatch, "Lives: " + character.CurrentLives + " - Score:" + character.Score, new Vector2(screenWidth / 2, 20), Color.Black);
                    spriteBatch.Draw(backgroundTexture, backgroundRectangle, backgroundRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                    break;
                case GameState.Menu:
                    DrawMenu();
                    break;
                case GameState.GameWon:
                    TextOut.DrawTextCentered(spriteBatch, "Gratulerer, you have finished the game! ", new Vector2(screenWidth / 2, screenHeight / 2), Color.Orange);
                    break;
                case GameState.RestartStage:
                    if (character.CurrentLives == 1)
                        TextOut.DrawTextCentered(spriteBatch, "Try again! (You have " + character.CurrentLives + " life left)", new Vector2(screenWidth / 2, screenHeight / 2), Color.Orange);
                    else
                        TextOut.DrawTextCentered(spriteBatch, "Try again! (You have " + character.CurrentLives + " lives left)", new Vector2(screenWidth / 2, screenHeight / 2), Color.Orange);
                    break;
                case GameState.GameOver:
                    TextOut.DrawTextCentered(spriteBatch, "Game over", new Vector2(screenWidth / 2, screenHeight / 2), Color.Orange);
                    break;
                case GameState.NextStage:
                    TextOut.DrawTextCentered(spriteBatch, "Gratulerer! You have accomplished level " + currentStage + "!", new Vector2(screenWidth / 2, screenHeight / 2), Color.Orange);
                    break;
                case GameState.Credits:
                    TextOut.DrawTextCentered(spriteBatch, "Game un-proudly made by...\n(Press back to return to the menu)", new Vector2(screenWidth / 2, screenHeight / 2), Color.Orange);
                    break;
                case GameState.HighScores:
                    TextOut.DrawTextCentered(spriteBatch, highscores.ToString(), new Vector2(screenWidth / 2, screenHeight / 2), Color.Orange);
                    break;
                case GameState.NewHighscore:
                    newHighScore();
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void Shot(Ball b, Shot s)
        {
            if (b.Size != 1)
            {
                Vector2 posAux = b.Position;
                posAux.X += (b.Sprite.Width / 2);

                Ball newball1 = new Ball(this, ballSprites[b.Size - 1], posAux, b.Size - 1);
                Ball newball2 = new Ball(this, ballSprites[b.Size - 1], posAux, b.Size - 1);

                Vector2 velAux = b.Velocity;
                velAux.X *= -1;

                newball1.Velocity = velAux;
                newball2.Velocity = b.Velocity;

                balls.Add(newball1);
                balls.Add(newball2);

                gameObjectManager.Add(newball1);
                gameObjectManager.Add(newball2);

            }
            this.balls.Remove(b);
            gameObjectManager.Remove(b);
            audio.PlayCue("hit_2");

            s.Character.Score += b.Bonus;

            if (balls.Count <= 0)
                currentState = GameState.NextStage;
        }
        public void CharacterHit(Ball b)
        {
            if (currentState == GameState.Playing)
            {
                character.CurrentLives--;

                if (character.CurrentLives >= 1)
                    currentState = GameState.RestartStage;
                else
                    currentState = GameState.GameOver;

            }
        }

        public void FireWeapon(Character character)
        {
            if (character.DoubleShoot)
            {
                //Shot shot1 = new Shot(this, shotSprite, character.Position, character.CurrentWeapon);
                Shot shot1 = new Shot(this, shotSprite, character.Position, character);

                gameObjectManager.Add(shot1);

                //Shot shot2 = new Shot(this, shotSprite, new Vector2(character.Position.X + character.Sprite.Width, character.Position.Y),
                //                    character.CurrentWeapon);

                Shot shot2 = new Shot(this, shotSprite, new Vector2(character.Position.X + character.Sprite.Width, character.Position.Y), character);

                gameObjectManager.Add(shot2);
            }
            else
            {

                //Shot shot1 = new Shot(this, shotSprite, new Vector2(character.Position.X + (character.Sprite.Width/2), character.Position.Y), character.CurrentWeapon);

                Shot shot1 = new Shot(this, shotSprite, new Vector2(character.Position.X + (character.Sprite.Width / 2), character.Position.Y), character);

                gameObjectManager.Add(shot1);
            }
            audio.PlayCue("laser2");
        }

        public void RemoveObject(BasicGameObject o)
        {
            gameObjectManager.Remove(o);
        }

        public void DisplayMenu()
        {
            TextOut.DrawTextCentered(spriteBatch, "A) Start Game \n B) High scores \n C)Credits", new Vector2(screenWidth / 2, screenHeight / 2), Color.Black);
        }

        private void DrawMenu()
        {
            //graphics.GraphicsDevice.Clear(Color.Black);

            Color startGameColor = Color.White;
            Color highScoreColor = Color.White;
            Color creditsColor = Color.White;
            Color exitColor = Color.White;

            if (currentMenuElement == MenuElement.StartGame)
                startGameColor = Color.Orange;
            else if (currentMenuElement == MenuElement.HighScore)
                highScoreColor = Color.Orange;
            else if (currentMenuElement == MenuElement.Credits)
                creditsColor = Color.Orange;
            else if (currentMenuElement == MenuElement.Exit)
                exitColor = Color.Orange;

            Vector2 easyMeasure = fontMedium.MeasureString("START GAME");
            Vector2 mediumMeasure = fontMedium.MeasureString("HIGH SCORE");
            Vector2 hardMeasure = fontMedium.MeasureString("CREDITS");
            Vector2 expertMeasure = fontMedium.MeasureString("EXIT");

            Vector2 startGamePosition = new Vector2((GraphicsDevice.Viewport.Width * 0.5f) - (easyMeasure.X * 0.5f), (GraphicsDevice.Viewport.Height * 0.1f) + (easyMeasure.Y * 2.0f));
            Vector2 highScorePosition = new Vector2((GraphicsDevice.Viewport.Width * 0.5f) - (mediumMeasure.X * 0.5f), startGamePosition.Y + easyMeasure.Y);
            Vector2 creditsPosition = new Vector2((GraphicsDevice.Viewport.Width * 0.5f) - (hardMeasure.X * 0.5f), highScorePosition.Y + mediumMeasure.Y);
            Vector2 exitPosition = new Vector2((GraphicsDevice.Viewport.Width * 0.5f) - (expertMeasure.X * 0.5f), creditsPosition.Y + hardMeasure.Y);

            spriteBatch.DrawString(fontMedium, "START GAME", startGamePosition, startGameColor);
            spriteBatch.DrawString(fontMedium, "HIGH SCORE", highScorePosition, highScoreColor);
            spriteBatch.DrawString(fontMedium, "CREDITS", creditsPosition, creditsColor);
            spriteBatch.DrawString(fontMedium, "EXIT", exitPosition, exitColor);

        }

        private void LoadStage()
        {
            //Stage stage = new Stage(currentStage, fileManager);
            Stage stage = fileManager.loadStage(currentStage);
            if (character.Score >= 2000) character.doubleShoot = true;
            balls = new List<Ball>();

            // Set up ball game object.

            Vector2 ballInitialPos = new Vector2(screenWidth / 2, screenHeight / 10);


            for (int i = 0; i < stage.BallsNoSize1; i++)
                balls.Add(new Ball(this, ballSprites[1], 1));
            //todo: add 2 and 3

            for (int i = 0; i < stage.BallsNoSize2; i++)
                balls.Add(new Ball(this, ballSprites[2], 2));


            for (int i = 0; i < stage.BallsNoSize3; i++)
                balls.Add(new Ball(this, ballSprites[3], 3));


            foreach (Ball b in balls)
                gameObjectManager.Add(b);
            gameObjectManager.Add(character);

        }

        private void RestartGameValues()
        {

            this.currentStage = 1;
            character.CurrentLives = INITIAL_LIVES;
            character.Position = new Vector2(screenWidth / 2 - (characterTexture.Width / 2), screenHeight);
            character.Score = 0;
            playerName = "";
        }
        public void newHighScore()
        {


            KeyboardState[] keyStates = input.CurrentKeyboardStates;
            KeyboardState ks = keyStates[0];
            //if(keyStates!=oldkeystates){
            //foreach (KeyboardState ks in keyStates)
            //{
            Keys[] keyMap = ks.GetPressedKeys();


            foreach (Keys k in keyMap)
            {

                if (input.IsKeyPressed(k))
                {


                    if (playerName.Length <= MAXPLAYERNAMESTRING)
                    {
                        if (char.IsLetter(k.ToString(), 0) && (k.ToString().Length == 1))
                            playerName += k.ToString();
                    }
                }

            }


            //}

            TextOut.DrawTextCentered(spriteBatch, "New Highscore\nEnter your name: " + playerName, new Vector2(screenWidth / 2, screenHeight / 2), Color.Orange);

            if (playerName.Length >= MAXPLAYERNAMESTRING)
            {
                highscores = new HighScores(fileManager);
                highscores.newHighscore(new Score(playerName, character.Score));

                fileManager.writeScore(highscores);

                //HIGHSCORE updated, display new higscore
                currentState = GameState.HighScores;
            }
        }
    }
}
