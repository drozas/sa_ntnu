namespace XQUEST.GameObjectManagement
{
  /// <summary>
  /// Interface for handling of collisions. Have you game object implement 
  /// this interface if you want it to collide with other game objects.
  /// </summary>
  public interface ICollidable
  {
    /// <summary>
    /// Callback method for collisions. This method will be called
    /// when the GameObject implementing this interface collides with other 
    /// game objects in the game world.
    /// </summary>
    /// <param name="collisionObject">The GameObject this GameObject collided 
    /// with.</param>
    void OnCollision(IGameObject collisionObject);
  }
}