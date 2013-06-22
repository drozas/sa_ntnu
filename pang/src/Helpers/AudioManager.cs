using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace XQUEST.Helpers
{
  /// <summary>
  /// The AudioManager handles all your needs for playing and manipulating 
  /// sound and music.
  /// </summary>
  public class AudioManager : GameComponent
  {
    private AudioEngine engine;
    private WaveBank waveBank;
    private SoundBank soundBank;

    /// <summary>
    /// Creates a new AudioManager.
    /// </summary>
    /// <param name="game">The Game to run under.</param>
    /// <param name="settingsFileName">Path to a global settings file.</param>
    /// <param name="nonStreamingWaveBankFilename">Path to the wave bank 
    /// file to load.</param>
    /// <param name="soundBankFilename">Path to the sound bank file to load.</param>
    public AudioManager(Game game, string settingsFileName,
                        string nonStreamingWaveBankFilename, string soundBankFilename) : base(game)
    {
      engine = new AudioEngine(settingsFileName);
      waveBank = new WaveBank(engine, nonStreamingWaveBankFilename);
      soundBank = new SoundBank(engine, soundBankFilename);

      // Remove previous audio manager, if any
      if (game.Services.GetService(typeof (AudioManager)) != null)
        game.Services.RemoveService(typeof (AudioManager));

      game.Services.AddService(typeof (AudioManager), this);
    }

    public AudioEngine AudioEngine
    {
      get { return engine; }
    }

    public WaveBank WaveBank
    {
      get { return waveBank; }
    }

    public SoundBank SoundBank
    {
      get { return soundBank; }
    }

    public override void Update(GameTime gameTime)
    {
      engine.Update();
    }

    /// <summary>
    /// Plays a cue.
    /// </summary>
    /// <param name="cueName">Name of the cue as specified in the XACT tool.</param>
    public void PlayCue(string cueName)
    {
      soundBank.PlayCue(cueName);
    }
  }
}