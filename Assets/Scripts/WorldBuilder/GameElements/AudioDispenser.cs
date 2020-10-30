using UnityEngine;

/// <summary>
/// AudioDispenser class
/// </summary>
public class AudioDispenser : Dispenser {
    #region Fields

    string name;
    Sound sound;

    #endregion

    #region Properties

    public string Name {
        get { return name; }
        set { name = value; }
    }

    public Sound Sound {
        get { return sound; }
		set { sound = value; }
	}

    #endregion

    #region Constructor(s)

    public AudioDispenser(string name, Sound sound) {
        this.name = name;
        this.sound = sound;
    }

    #endregion

    #region Method(s)

    public override void Dispense() {
        if(Comp != null) {
            AudioSource audioSource = Comp as AudioSource;
            audioSource.clip = Resources.Load("AudioClips/" + sound.FileName) as AudioClip;
            audioSource.minDistance = 0;
            audioSource.maxDistance = sound.MaxDistance;
            audioSource.volume = sound.Gain;
            audioSource.Play();
            this.LogToCsv();
        }
    }

	public override void LogToCsv() {
		
	}

	#endregion
}
