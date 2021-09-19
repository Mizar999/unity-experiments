using System;
using UnityEngine;

[Serializable]
public class Level
{
    [TextArea(16, 16)]
    public string Floor;
    public LevelObject[] Objects;
}
