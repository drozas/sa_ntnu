namespace XQUEST.GameObjectManagement
{
  /// <summary>
  /// Enum representing the various states a game object can be in.
  /// </summary>
  public enum GameObjectState
  {
    Alive, // game object is alive, draw it and update its logic
    Dying, // game object is dying, draw it and update its logic, but don't do collision detection
    Dead // game object is dead, should be removed
  }
}