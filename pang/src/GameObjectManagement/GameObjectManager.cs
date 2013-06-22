using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XQUEST.Helpers;

namespace XQUEST.GameObjectManagement
{
  /// <summary>
  /// The GameObjectManager manages a list of IGameObjects. It provides automatic 
  /// logic updating and game objects in addition to collision detection. The class 
  /// implements the IEnumerable interface to allow enumeration of the game objects, 
  /// something that can be useful if the user want to perform some operation on all 
  /// game objects. The manager provides a Draw method which simply enumerates the 
  /// game objects and call their draw method. Note that GameObjectManager is not a 
  /// DrawableGameComponent and thus the Draw method will not be called automatically 
  /// by the framework. This is intentional as the user may want to draw the game objects 
  /// their own way, for example using a game-specific camera projection which needs to 
  /// translate the game object’s positions.
  /// </summary>
  public class GameObjectManager : GameComponent, IEnumerable<IGameObject>
  {
    private List<IGameObject> gameObjects;

    private InputManager input;

    /// <summary>
    /// Creates a new GameObjectManager.
    /// </summary>
    /// <param name="game">The Game instance.</param>
    public GameObjectManager(Game game)
      : base(game)
    {
      gameObjects = new List<IGameObject>();

      input = (InputManager) game.Services.GetService(typeof (InputManager));

      // Add the manager as a service
      game.Services.AddService(typeof (GameObjectManager), this);
    }

    /// <summary>
    /// Adds a GameObject to the manager.
    /// </summary>
    /// <param name="gameObject">The GameObject to add.</param>
    public void Add(IGameObject gameObject)
    {
      gameObjects.Add(gameObject);
    }

    /// <summary>
    /// Removes a GameObject from the manager.
    /// </summary>
    /// <param name="gameObject">The GameObject to remove.</param>
    public void Remove(IGameObject gameObject)
    {
      gameObjects.Remove(gameObject);
    }

    /// <summary>
    /// Removes a range of GameObjects from the manager.
    /// </summary>
    /// <param name="index">The zero-based starting index of the range of elements to
    /// remove.</param>
    /// <param name="count">The number of elements to remove.</param>
    public void RemoveRange(int index, int count)
    {
      gameObjects.RemoveRange(index, count);
    }

    public void RemoveAll(Predicate<IGameObject> match)
    {
      gameObjects.RemoveAll(match);
    }

    public int Count
    {
      get { return gameObjects.Count; }
    }

    /// <summary>
    /// Removes all GameObjects from the manager.
    /// </summary>
    public void Clear()
    {
      gameObjects.Clear();
    }

    /// <summary>
    /// Updates the logic of the game objects currently under control of the manager,
    /// and checks for collisions between them.
    /// </summary>
    /// <param name="gameTime">The GameTime instance form the application 
    /// framework.</param>
    public override void Update(GameTime gameTime)
    {
      for (int i = 0; i < gameObjects.Count; i++)
      {
        IGameObject go1 = gameObjects[i];
        go1.Update(gameTime, input);

        if (go1.State == GameObjectState.Dead)
        {
          Remove(go1);
          i--;
          continue;
        }

        if (go1.State == GameObjectState.Dying) continue;

        for (int j = i + 1; j < gameObjects.Count; j++)
        {
          IGameObject go2 = gameObjects[j];

          if (go2.State == GameObjectState.Dead)
          {
            Remove(go2);
            j--;
            continue;
          }

          if (go2.State == GameObjectState.Dying) continue;

          IGameObject collider = null, collidee = null;
          if (go1 is ICollidable && !(go2 is ICollidable))
          {
            collider = go1;
            collidee = go2;
          }
          else if (go2 is ICollidable && !(go1 is ICollidable))
          {
            collider = go2;
            collidee = go1;
          }
          else if (go1 is ICollidable && go2 is ICollidable)
          {
            collider = go1;
            collidee = go2;
          }
          else continue;

          if (collider.BoundingRectangle.Intersects(collidee.BoundingRectangle))
          {
            ((ICollidable) collider).OnCollision(collidee);
            if (collidee is ICollidable)
              ((ICollidable) collidee).OnCollision(collider);
          }
        }
      }
    }

    /// <summary>
    /// Draws the GameObjects controlled by the manager.
    /// </summary>
    /// <param name="spriteBatch">A SpriteBatch instance used to draw the 
    /// GameObjects.</param>
    public void Draw(SpriteBatch spriteBatch)
    {
      foreach (IGameObject gameObject in gameObjects)
        gameObject.Draw(spriteBatch);
    }

    /// <summary>
    /// Indexer into the game object collection.
    /// </summary>
    /// <param name="index">Index of the game object to retrieve</param>
    /// <returns>The game object at the specified index.</returns>
    public IGameObject this[int index]
    {
      get { return gameObjects[index]; }
      set { gameObjects[index] = value; }
    }

    // Implementation of IEnumerable<GameObject>
    public IEnumerator<IGameObject> GetEnumerator()
    {
      return gameObjects.GetEnumerator();
    }

    // Implementation of IEnumerable
    IEnumerator IEnumerable.GetEnumerator()
    {
      return gameObjects.GetEnumerator();
    }
  }
}