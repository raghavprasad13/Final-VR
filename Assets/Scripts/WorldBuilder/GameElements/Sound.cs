using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound {
	string name;
	string fileName;
	float gain;
	float maxDistance;
	float height;

    public string FileName {
        get { return fileName; }
        set { fileName = value; }
    }

    public float Gain {
        get { return gain; }
        set { gain = value; }
    }

    public float MaxDistance {
        get { return maxDistance; }
        set { maxDistance = value; }
    }

    public float Height {
        get { return height; }
        set { height = value; }
    }

    public Sound(string name, string fileName, float gain, float maxDistance, float height) {
        this.name = name;
        this.fileName = fileName;
        this.gain = gain;
        this.maxDistance = maxDistance;
        this.height = height;
	}
}
