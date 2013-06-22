using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace XQUEST.SpriteAnimationFramework
{
  /// <summary>
  /// Holds information about an Animation, such as what frames it consists, 
  /// if it's looped and the framesPerSecond
  /// </summary>
  public class Animation
  {
    private List<Rectangle> frames;
    private float animationSpeed;
    private int[] animationSequence;

    private bool isStarted;
    private bool isLooped;

    private int currentFrame;

    /// <summary>
    /// Creates a new Animation.
    /// </summary>
    /// <param name="frames">A list of rectangles representing frames in a texture 
    /// that the animation consists of.</param>
    /// <param name="animationSpeed">The delay between each frame during animation, 
    /// in seconds.</param>
    /// <param name="isLooped">If the animation should be loop, or not</param>
    public Animation(List<Rectangle> frames, float animationSpeed, bool isLooped)
      : this(frames, animationSpeed, isLooped, GetDefaultSequence(frames.Count))
    {
    }

    /// <summary>
    /// Creates a new Animation from a sprite sheet.
    /// </summary>
    /// <param name="textureWidth">Width of the sprite sheet texture</param>
    /// <param name="textureHeight">Height of the sprite sheet texture</param>
    /// <param name="frameWidth">Width of the frames in the sprite sheet</param>
    /// <param name="frameHeight">Height of the frames in the sprite sheet</param>
    /// <param name="columnSpacing">Spacing in pixels between each column of 
    /// frames</param>
    /// <param name="rowSpacing">Spacing in pixels between each row of frames</param>
    /// <param name="animationSpeed">The delay between each frame during animation, 
    /// in seconds.</param>
    /// <param name="isLooped">If the animation should be loop, or not</param>
    public Animation(int textureWidth, int textureHeight, int frameWidth, int frameHeight,
                     int columnSpacing, int rowSpacing, float animationSpeed, bool isLooped)
      : this(ExtractFrames(textureWidth, textureHeight, frameWidth, frameHeight,
                           columnSpacing, rowSpacing), animationSpeed, isLooped)
    {
    }

    /// <summary>
    /// Creates a new Animation.
    /// </summary>
    /// <param name="frames">A list of rectangles representing frames in a texture that 
    /// the animation consists of.</param>
    /// <param name="isLooped">If the animation should be loop, or not</param>
    /// <param name="animationSpeed">The delay between each frame during animation, 
    /// in seconds.</param>
    /// <param name="animationSequence">A specific sequence the rectangles should be
    /// drawn in. Maybe they should be drawn like 1 2 3 2 1 ?</param>
    public Animation(List<Rectangle> frames, float animationSpeed, bool isLooped,
                     int[] animationSequence)
    {
      this.frames = frames;
      this.isLooped = isLooped;
      this.animationSpeed = animationSpeed;
      this.animationSequence = animationSequence;
      isStarted = true;
    }

    private static List<Rectangle> ExtractFrames(int textureWidth, int textureHeight,
                                                 int frameWidth, int frameHeight, int columnSpacing,
                                                 int rowSpacing)
    {
      List<Rectangle> frames = new List<Rectangle>();
      for (int x = 0; x < textureWidth; x += frameWidth + columnSpacing)
      {
        for (int y = 0; y < textureHeight; y += frameHeight + rowSpacing)
        {
          frames.Add(new Rectangle(x, y, frameWidth, frameHeight));
        }
      }
      return frames;
    }

    private static int[] GetDefaultSequence(int numFrames)
    {
      int[] sequence = new int[numFrames];
      for (int i = 0; i < numFrames; i++)
      {
        sequence[i] = i;
      }
      return sequence;
    }

    /// <summary>
    /// Gets or sets the animation sequence.
    /// </summary>
    public int[] AnimationSequence
    {
      get { return animationSequence; }
      set { animationSequence = value; }
    }

    /// <summary>
    /// Gets or sets the animation speed (pause between frames).
    /// </summary>
    public float AnimationSpeed
    {
      get { return animationSpeed; }
      set { animationSpeed = value; }
    }

    /// <summary>
    /// Gets or sets a boolean value indicating whether the animation is started.
    /// </summary>
    public bool IsStarted
    {
      get { return isStarted; }
      set { isStarted = value; }
    }

    /// <summary>
    /// Gets or sets a boolean value indicating whether the animation should loop.
    /// </summary>
    public bool IsLooped
    {
      get { return isLooped; }
      set { isLooped = value; }
    }

    /// <summary>
    /// Gets or sets the list of source rectangles for the individual frames in the 
    /// animation.
    /// </summary>
    public List<Rectangle> Frames
    {
      get { return frames; }
      set { frames = value; }
    }

    /// <summary>
    /// Gets or sets the current frame.
    /// </summary>
    public int CurrentFrame
    {
      get { return currentFrame; }
      set { currentFrame = value; }
    }
  }
}