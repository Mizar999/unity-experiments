using UnityEngine;

[CreateAssetMenu(fileName = "New Level Manager", menuName = "Level/Level Manager")]
public class LevelManager : ScriptableObject
{
    public Level[] Levels;
}
