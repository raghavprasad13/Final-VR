/// <summary>
/// Pillar class
/// </summary>
public class Pillar {
    #region Fields

    float height;
    string material;

    #endregion

    #region Properties

    public float Height {
        get { return height; }
    }

    public string Material {
        get { return material; }
    }

    #endregion

    #region Constructor(s)

    public Pillar(float height, string material) {
        this.height = height;
        this.material = material;
    }

    #endregion
}
