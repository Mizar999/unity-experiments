using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Scriptable Objects/Tile Data")]
public class TileData : ScriptableObject
{
    public TileBase Tile;
    public ObjectType Type;
    public ObjectClass Class;
}
