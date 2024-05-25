using System;

namespace MegaPint
{

/// <summary> This class holds information about the displayed text and the last foldout state in the inspector </summary>
[Serializable]
public class NotePad
{
    public string text = "";
    public bool wasFoldout;
}

}
