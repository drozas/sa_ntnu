using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XQUEST.SpriteAnimationFramework
{
  /// <summary>
  /// Represents a sequence of images rendered after eachother
  /// to simulate animation.
  /// </summary>
  public class AnimatedSprite : Sprite
  {
    // Dictionary of Animation objects, keyed by a string describing the object.
    private Dictionary<string, Animation> animations =
      new Dictionary<string, Animation>();

    private float elapsedTime;
    private string currentAnimationSet;

    /// <summary>
    /// Creates a new AnimatedSprite
    /// </summary>
    /// <param name="spriteTexture"></param>
    public AnimatedSprite(Texture2D spriteTexture)
      : base(spriteTexture)
    {
    }

    /// <summary>
    /// Creates a new AnimatedSprite with a default animation
    /// </summary>
    /// <param name="spriteTexture">A Texture2D object pointing to a sequence of 
    /// frames.</param>
    public AnimatedSprite(Texture2D spriteTexture, Animation animation)
      : base(spriteTexture)
    {
      animations.Add("default", animation);
      currentAnimationSet = "default";
      sourceRect =
        animation.Frames[animation.AnimationSequence[animation.CurrentFrame]];
    }

    /// <summary>
    /// Adds an animation to the animated sprite.
    /// </summary>
    /// <param name="key">Animation identifier.</param>
    /// <param name="animation">The Animation object to add.</param>
    public void AddAnimation(string key, Animation animation)
    {
      animations.Add(key, animation);
      if (currentAnimationSet == null)
      {
        currentAnimationSet = key;
        sourceRect =
          animation.Frames[animation.AnimationSequence[animation.CurrentFrame]];
      }
    }

    /// <summary>
    /// Sets the current animation to display.
    /// </summary>
    /// <param name="animationKey">The string identifier for the animation to set.</param>
    public void SetAnimation(string animationKey)
    {
      // This animation is already set
      if (currentAnimationSet.Equals(animationKey))
        return;

      currentAnimationSet = animationKey;
      Animation currentAnimation = animations[currentAnimationSet];
      if (!currentAnimation.IsLooped)
      {
        currentAnimation.IsStarted = true;
        currentAnimation.CurrentFrame = currentAnimation.AnimationSequence[0];
        elapsedTime = 0.0f;
      }
    }

    /// <summary>
    /// Gets the current animation.
    /// </summary>
    public Animation CurrentAnimation
    {
      get { return animations[currentAnimationSet]; }
    }

    /// <summary>
    /// Updates the animation, moving between frames each time
    /// animationSpeed seconds have elapsed. This method must be called
    /// in the update method of your game, or in an encapsulating class
    /// if you want the sprite to animate.
    /// </summary>
    /// <param name="gameTime">GameTime object from the XNA framework's Update</param>
    public void UpdateAnimation(GameTime gameTime)
    {
      Animation animation = animations[currentAnimationSet];
      if (animation.IsStarted)
      {
        elapsedTime += (float) gameTime.ElapsedGameTime.TotalSeconds;
        if (elapsedTime >= animation.AnimationSpeed)
        {
          animation.CurrentFrame++;
          if (animation.CurrentFrame >= animation.AnimationSequence.Length)
          {
            animation.IsStarted = animation.IsLooped;
            animation.CurrentFrame = 0;
          }
          elapsedTime = 0.0f;
        }
        sourceRect =
          animation.Frames[animation.AnimationSequence[animation.CurrentFrame]];
      }
    }
  }
}