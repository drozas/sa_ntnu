using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XQUEST.Helpers
{
  /// <summary>
  /// Handles all input needs from the keyboard, mouse (Windows only), and
  /// up to four Xbox 360 controllers.
  /// </summary>
  public class InputManager : GameComponent
  {
    private const int MaxInputs = 4;
    // Array to hold the current state of each of the four game pads.
    private readonly GamePadState[] currentGamePadStates;

    // Array to hold the state of each of the four game pads from the previous 
    // update call.
    private readonly GamePadState[] lastGamePadStates;

    // Keyboard current state and state from the previous update call.
    private readonly KeyboardState[] currentKeyboardStates;
    private readonly KeyboardState[] lastKeyboardStates;

#if !XBOX
    // Mouse current state and state from the previous update call.
    private MouseState currentMouseState = Mouse.GetState();
    private MouseState lastMouseState;
#endif

    public GamePadState[] CurrentGamePadStates
    {
      get { return currentGamePadStates; }
    }

    public KeyboardState[] CurrentKeyboardStates
    {
      get { return currentKeyboardStates; }
    }

    public GamePadState[] LastGamePadStates
    {
      get { return lastGamePadStates; }
    }

    public KeyboardState[] LastKeyboardStates
    {
      get { return lastKeyboardStates; }
    }

#if !XBOX
    public MouseState CurrentMouseState
    {
      get { return currentMouseState; }
    }

    public MouseState LastMouseState
    {
      get { return lastMouseState; }
    }
#endif

    /// <summary>
    /// Creates a new InputManager. Adds itself as a service.
    /// </summary>
    /// <param name="game">The Game to run under.</param>
    public InputManager(Game game) : base(game)
    {
      currentGamePadStates = new GamePadState[MaxInputs];
      lastGamePadStates = new GamePadState[MaxInputs];

      currentKeyboardStates = new KeyboardState[MaxInputs];
      lastKeyboardStates = new KeyboardState[MaxInputs];

      // Register the InputManager with the Game Services. Users of the 
      // InputManager should look it up from the Game Services when needed, 
      // and not create it through this constructor.
      game.Services.AddService(typeof (InputManager), this);
    }

    public override void Update(GameTime gameTime)
    {
      // Save game pad state from the previous update call.
      for (int i = 0; i < MaxInputs; i++)
      {
        lastGamePadStates[i] = currentGamePadStates[i];
        lastKeyboardStates[i] = currentKeyboardStates[i];

        currentGamePadStates[i] = GamePad.GetState((PlayerIndex) i);
        currentKeyboardStates[i] = Keyboard.GetState((PlayerIndex) i);
      }

#if !XBOX
      // Save last and get the new mouse state.
      lastMouseState = currentMouseState;
      currentMouseState = Mouse.GetState();
#endif
    }

    /// <summary>
    /// Checks for any key press, by any player.
    /// </summary>
    /// <returns>A boolean value indicating whether any key is down, by any player.</returns>
    public bool IsAnyKeyDown()
    {
      for (int i = 0; i < MaxInputs; i++)
      {
        if (IsAnyKeyDown((PlayerIndex) i))
          return true;
      }
      return false;
    }

    public bool IsAnyKeyDown(PlayerIndex playerIndex)
    {
      return currentKeyboardStates[(int) playerIndex].GetPressedKeys().Length != 0;
    }

    /// <summary>
    /// Checks if a specific key is down, by any player.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>A boolean value indicating whether the key was down, by any player.</returns>
    public bool IsKeyDown(Keys key)
    {
      for (int i = 0; i < MaxInputs; i++)
      {
        if (IsKeyDown(key, (PlayerIndex) i))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Checks if a specific key is down.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="playerIndex">The player index.</param>
    /// <returns>A boolean value indicating whether the key was down.</returns>
    public bool IsKeyDown(Keys key, PlayerIndex playerIndex)
    {
      return currentKeyboardStates[(int) playerIndex].IsKeyDown(key);
    }

    /// <summary>
    /// Returns whether a key has just been pressed, by any player. This is useful
    /// if you do not want the key to repeat (like in menu selections).
    /// </summary>
    /// <param name="key">Enumerated value that specifies the key to query.</param>
    /// <returns>true if the key was just pressed, false otherwise.</returns>
    public bool IsKeyPressed(Keys key)
    {
      for (int i = 0; i < MaxInputs; i++)
      {
        if (IsKeyPressed(key, (PlayerIndex) i))
          return true;
      }
      return false;
    }

    public bool IsKeyPressed(Keys key, PlayerIndex playerIndex)
    {
      return
        currentKeyboardStates[(int) playerIndex].IsKeyDown(key) &&
        lastKeyboardStates[(int) playerIndex].IsKeyUp(key);
    }

    /// <summary>
    /// Returns whether the specified game pad is connected.
    /// </summary>
    /// <param name="playerIndex">Enumerator value that specifies which game 
    /// pad to query.</param>
    /// <returns>true if the game pad is connected, false otherwise.</returns>
    public bool GamePadIsConnected(PlayerIndex playerIndex)
    {
      return currentGamePadStates[(int) playerIndex].IsConnected;
    }

    public bool IsAnyButtonDown()
    {
      for (int i = 0; i < MaxInputs; i++)
      {
        if (IsAnyButtonDown((PlayerIndex) i))
          return true;
      }
      return false;
    }

    public bool IsAnyButtonDown(PlayerIndex playerIndex)
    {
      int i = (int) playerIndex;
      return currentGamePadStates[i].IsButtonDown(Buttons.A) ||
             currentGamePadStates[i].IsButtonDown(Buttons.B) ||
             currentGamePadStates[i].IsButtonDown(Buttons.X) ||
             currentGamePadStates[i].IsButtonDown(Buttons.Y) ||
             currentGamePadStates[i].IsButtonDown(Buttons.Start) ||
             currentGamePadStates[i].IsButtonDown(Buttons.Back) ||
             currentGamePadStates[i].IsButtonDown(Buttons.DPadDown) ||
             currentGamePadStates[i].IsButtonDown(Buttons.DPadLeft) ||
             currentGamePadStates[i].IsButtonDown(Buttons.DPadRight) ||
             currentGamePadStates[i].IsButtonDown(Buttons.DPadUp) ||
             currentGamePadStates[i].IsButtonDown(Buttons.LeftShoulder) ||
             currentGamePadStates[i].IsButtonDown(Buttons.RightShoulder) ||
             currentGamePadStates[i].IsButtonDown(Buttons.LeftStick) ||
             currentGamePadStates[i].IsButtonDown(Buttons.RightStick) ||
             currentGamePadStates[i].IsButtonDown(Buttons.LeftTrigger) ||
             currentGamePadStates[i].IsButtonDown(Buttons.RightTrigger);
    }

    public bool IsButtonDown(Buttons button)
    {
      for (int i = 0; i < MaxInputs; i++)
      {
        if (IsButtonDown(button, (PlayerIndex) i))
          return true;
      }
      return false;
    }

    public bool IsButtonDown(Buttons button, PlayerIndex playerIndex)
    {
      return currentGamePadStates[(int) playerIndex].IsButtonDown(button);
    }

    public bool IsButtonPressed(Buttons button)
    {
      for (int i = 0; i < MaxInputs; i++)
      {
        if (IsButtonPressed(button, (PlayerIndex) i))
          return true;
      }
      return false;
    }

    public bool IsButtonPressed(Buttons button, PlayerIndex playerIndex)
    {
      return currentGamePadStates[(int) playerIndex].IsButtonDown(button)
             && lastGamePadStates[(int) playerIndex].IsButtonUp(button);
    }

    public bool IsKeyOrButtonDown(Keys key, Buttons button, PlayerIndex playerIndex)
    {
      return IsKeyDown(key, playerIndex) || IsButtonDown(button, playerIndex);
    }

    public bool IsKeyOrButtonDown(Keys key, Buttons button)
    {
      for (int i = 0; i < MaxInputs; i++)
      {
        if (IsKeyOrButtonDown(key, button, (PlayerIndex) i))
          return true;
      }
      return false;
    }

    public bool IsKeyOrButtonPressed(Keys key, Buttons button, PlayerIndex playerIndex)
    {
      return IsKeyPressed(key, playerIndex) || IsButtonPressed(button, playerIndex);
    }

    public bool IsKeyOrButtonPressed(Keys key, Buttons button)
    {
      for (int i = 0; i < MaxInputs; i++)
      {
        if (IsKeyOrButtonPressed(key, button, (PlayerIndex) i))
          return true;
      }
      return false;
    }


    // These methods gets Position values for the thumb sticks and triggers

    /// <summary>
    /// Gets the Position of the game pad left thumbstick.
    /// </summary>
    /// <param name="playerIndex">Which of the 4 gamepads to query.</param>
    /// <returns>The Position of the game pad left thumbstick.</returns>
    public Vector2 GetLeftStickPosition(PlayerIndex playerIndex)
    {
      return currentGamePadStates[(int) playerIndex].ThumbSticks.Left;
    }

    /// <summary>
    /// Gets the Position of the game pad left thumbstick (Player One).
    /// </summary>
    /// <returns>The Position of the game pad left thumbstick (Player One).</returns>
    public Vector2 GetLeftStickPosition()
    {
      return currentGamePadStates[(int)PlayerIndex.One].ThumbSticks.Left;
    }

    /// <summary>
    /// Gets the Position of the game pad right thumbstick.
    /// </summary>
    /// <param name="playerIndex">Which of the 4 gamepads to query.</param>
    /// <returns>The Position of the game pad right thumbstick</returns>
    public Vector2 GetRightStickPosition(PlayerIndex playerIndex)
    {
      return currentGamePadStates[(int) playerIndex].ThumbSticks.Right;
    }

    /// <summary>
    /// Gets the Position of the game pad right thumbstick (Player One).
    /// </summary>
    /// <returns>The Position of the game pad right thumbstick (Player One).</returns>
    public Vector2 GetRightStickPosition()
    {
      return currentGamePadStates[(int) PlayerIndex.One].ThumbSticks.Right;
    }

    /// <summary>
    /// Gets the Position of the game pad left trigger.
    /// </summary>
    /// <param name="playerIndex">Which of the 4 gamepads to query.</param>
    /// <returns>The Position of the game pad left trigger.</returns>
    public float GetLeftTriggerPosition(PlayerIndex playerIndex)
    {
      return currentGamePadStates[(int) playerIndex].Triggers.Left;
    }

    /// <summary>
    /// Gets the Position of the game pad left trigger (Player One).
    /// </summary>
    /// <returns>The Position of the game pad left trigger (Player One).</returns>
    public float GetLeftTriggerPosition()
    {
      return currentGamePadStates[(int) PlayerIndex.One].Triggers.Left;
    }

    /// <summary>
    /// Gets the Position of the game pad right trigger.
    /// </summary>
    /// <param name="playerIndex">Which of the 4 gamepads to query.</param>
    /// <returns>The Position of the game pad right trigger.</returns>
    public float GetRightTriggerPosition(PlayerIndex playerIndex)
    {
      return currentGamePadStates[(int) playerIndex].Triggers.Right;
    }

    /// <summary>
    /// Gets the Position of the game pad right trigger (Player One).
    /// </summary>
    /// <returns>The Position of the game pad right trigger (Player One).</returns>
    public float GetRightTriggerPosition()
    {
      return currentGamePadStates[(int) PlayerIndex.One].Triggers.Right;
    }

#if !XBOX
    /// <summary>
    /// Gets the current state of the mouse (Windows only).
    /// </summary>
    public MouseState MouseState
    {
      get { return currentMouseState; }
    }

    /// <summary>
    /// Gets a boolean value indicating whether the left mouse button is 
    /// being held down.
    /// </summary>
    public bool IsLeftMouseButtonDown
    {
      get { return currentMouseState.LeftButton == ButtonState.Pressed; }
    }

    /// <summary>
    /// Gets a boolean value indicating whether the right mouse button is 
    /// being held down.
    /// </summary>
    public bool IsRightMouseButtonDown
    {
      get { return currentMouseState.RightButton == ButtonState.Pressed; }
    }

    /// <summary>
    /// Gets a boolean value indicating whether the middle mouse button is 
    /// being held down.
    /// </summary>
    public bool IsMiddleMouseButtonDown
    {
      get { return currentMouseState.MiddleButton == ButtonState.Pressed; }
    }

    /// <summary>
    /// Gets a boolean value indicating whether mouse XButton1 is being held down.
    /// </summary>
    public bool IsMouseXButton1Down
    {
      get { return currentMouseState.XButton1 == ButtonState.Pressed; }
    }

    /// <summary>
    /// Gets a boolean value indicating whether mouse XButton2 is being held down.
    /// </summary>
    public bool IsMouseXButton2Down
    {
      get { return currentMouseState.XButton2 == ButtonState.Pressed; }
    }

    /// <summary>
    /// Gets a boolean value indicating whether the left mouse button was just 
    /// pressed.
    /// </summary>
    public bool IsLeftMouseButtonPressed
    {
      get
      {
        return currentMouseState.LeftButton == ButtonState.Pressed &&
               lastMouseState.LeftButton == ButtonState.Released;
      }
    }

    /// <summary>
    /// Gets a boolean value indicating whether the right mouse button was just 
    /// pressed.
    /// </summary>
    public bool IsRightMouseButtonPressed
    {
      get
      {
        return currentMouseState.RightButton == ButtonState.Pressed &&
               lastMouseState.RightButton == ButtonState.Released;
      }
    }

    /// <summary>
    /// Gets a boolean value indicating whether the middle mouse button was just 
    /// pressed.
    /// </summary>
    public bool IsMiddleMouseButtonPressed
    {
      get
      {
        return currentMouseState.MiddleButton == ButtonState.Pressed &&
               lastMouseState.MiddleButton == ButtonState.Released;
      }
    }

    /// <summary>
    /// Gets a boolean value indicating whether mouse XButton1 was just pressed.
    /// </summary>
    public bool IsMouseXButton1ButtonPressed
    {
      get
      {
        return currentMouseState.XButton1 == ButtonState.Pressed &&
               lastMouseState.XButton1 == ButtonState.Released;
      }
    }

    /// <summary>
    /// Gets a boolean value indicating whether mouse XButton2 was just pressed.
    /// </summary>
    public bool IsMouseXButton2ButtonPressed
    {
      get
      {
        return currentMouseState.XButton2 == ButtonState.Pressed &&
               lastMouseState.XButton2 == ButtonState.Released;
      }
    }

    /// <summary>
    /// Gets the Position of the mouse cursor.
    /// </summary>
    public Vector2 MousePosition
    {
      get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
    }

    public Vector2 MouseDelta
    {
      get { return MousePosition - new Vector2(lastMouseState.X, lastMouseState.Y); }
    }

    /// <summary>
    /// Gets the horizontal Position of the mouse.
    /// </summary>
    public int MouseXPosition
    {
      get { return currentMouseState.X; }
    }

    /// <summary>
    /// Gets the vertical Position of the mouse.
    /// </summary>
    public int MouseYPosition
    {
      get { return currentMouseState.Y; }
    }

    /// <summary>
    /// Gets the cumulated mouse scroll wheel value since the game started.
    /// </summary>
    public int ScrollWheelValue
    {
      get { return currentMouseState.ScrollWheelValue; }
    }

    /// <summary>
    /// Gets the change in mouse scroll wheel value since last frame.
    /// </summary>
    public int ScrollWheelDelta
    {
      get
      {
        return currentMouseState.ScrollWheelValue -
               lastMouseState.ScrollWheelValue;
      }
    }

#endif
  }
}