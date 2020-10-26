using UnityEngine;

/// <summary>
/// Abstract Dispenser base class which must be derived by different types of Dispenser subclasses
/// </summary>
public abstract class Dispenser {
    string name;
    Component comp;

    public string Name {
        get { return name; }
        set { name = value; }
    }

    public Component Comp {
        get { return comp; }
        set { comp = value; }
    }

    public abstract void Dispense();

    public abstract void LogToCsv();
}
