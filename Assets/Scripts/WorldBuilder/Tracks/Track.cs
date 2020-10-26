/// <summary>
/// Abstract base class for all the different track types
/// </summary>
public abstract class Track {
	string type;
	string filePath;

	public string Type {
		get { return type; }
		set { type = value;  }
	}

	public string FilePath {
		get { return filePath; }
		set { filePath = value; }
	}
}
