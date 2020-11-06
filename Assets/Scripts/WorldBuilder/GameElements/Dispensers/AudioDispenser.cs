using UnityEngine;

/// <summary>
/// AudioDispenser class
/// </summary>
public class AudioDispenser : Dispenser {
    #region Fields

    Sound sound;

    #endregion

    #region Properties

    public Sound Sound {
        get { return sound; }
		set { sound = value; }
	}

    #endregion

    #region Constructor(s)

    public AudioDispenser(string dispenserName, Sound sound) : base (dispenserName) {
        this.sound = sound;
    }

    #endregion

    #region Method(s)

    public override void Dispense() {
        AudioSource audioSource = new AudioSource();
        audioSource.name = sound.Name;

        string audioFileNameNoExt = sound.FileName.Split('.')[0];

        audioSource.clip = Resources.Load<AudioClip>("Audio/" + audioFileNameNoExt);
        audioSource.volume = sound.Gain;
        audioSource.maxDistance = sound.MaxDistance;

        audioSource.Play();
	}

	//public override void LogToCsv() {
		
	//}

	#endregion
}
