using UnityEngine;

/// <summary>
/// AudioDispenser class
/// </summary>
public class AudioDispenser : Dispenser {
    #region Fields

    string soundName;
    string soundFileName;
    float soundGain;
    float soundRadius;
    float soundSourceHeight;

    #endregion

    #region Properties

    public string SoundName {
        get { return soundName; }
        set { soundName = value; }
    }

    public string SoundFileName {
        get { return soundFileName; }
        set { soundFileName = value; }
    }

    public float SoundGain {
        get { return soundGain; }
        set { soundGain = value; }
    }

    public float SoundRadius {
        get { return soundRadius; }
        set { soundRadius = value; }
    }

    public float SoundSourceHeight {
        get { return soundSourceHeight; }
        set { soundSourceHeight = value; }
    }

    #endregion

    #region Constructor(s)

    public AudioDispenser(string soundName = null, string soundFileName = null,
                            float soundGain = 0.0f, float soundRadius = 0.0f, float soundSourceHeight = 0.0f) {
        this.Name = null;
        this.soundName = soundName;
        this.soundFileName = soundFileName.Split('.')[0];
        this.soundGain = soundGain;
        this.soundRadius = soundRadius;
        this.soundSourceHeight = soundSourceHeight;
    }

    #endregion

    #region Method(s)

    public override void Dispense() {
        if(Comp != null) {
            AudioSource audioSource = Comp as AudioSource;
            audioSource.clip = Resources.Load("AudioClips/"+soundFileName) as AudioClip;
            audioSource.minDistance = 0;
            audioSource.maxDistance = soundRadius;
            audioSource.volume = soundGain;
            audioSource.Play();
            this.LogToCsv();
        }
    }

	public override void LogToCsv() {
		
	}

	#endregion
}
