using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TilemapDebug : MonoBehaviour
{
    [SerializeField]
    private Tilemap _groundTilemap;
    [SerializeField]
    private Tilemap _objectsTilemap;

    private void Awake()
    {
        BoundsInt bounds = _groundTilemap.cellBounds;
        string map = string.Empty;

        for (int z = bounds.min.z; z < bounds.max.z; ++z)
        {
            Debug.Log(z);
            for (int y = bounds.min.y; y < bounds.max.y; ++y)
            {
                Debug.Log(y);
                for (int x = bounds.max.x; x < bounds.max.x; ++x)
                {
                    Debug.Log(x);
                    if (_groundTilemap.GetTile(new Vector3Int(x, y, z)) != null)
                    {
                        map += ".";
                    }
                    else
                    {
                        map += " ";
                    }
                }
                map += Environment.NewLine;
            }
        }

        Debug.Log(map);

        bounds = _objectsTilemap.cellBounds;
        TileBase tile;
        for (int z = bounds.min.z; z < bounds.max.z; ++z)
        {
            for (int y = bounds.min.y; y < bounds.max.y; ++y)
            {
                for (int x = bounds.max.x; x < bounds.max.x; ++x)
                {
                    tile = _objectsTilemap.GetTile(new Vector3Int(x, y, z));
                    if (tile != null)
                    {
                        Debug.Log(tile.name);
                    }
                }
            }
        }
    }
}
