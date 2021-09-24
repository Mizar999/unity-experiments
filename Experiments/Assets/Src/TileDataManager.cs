using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDataManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap _map;
    [SerializeField]
    private List<TileData> _availableData;

    private Dictionary<TileBase, TileData> _tileDataLookup;

    private void Awake()
    {
        _tileDataLookup = new Dictionary<TileBase, TileData>();
        foreach (var data in _availableData)
        {
            _tileDataLookup.Add(data.Tile, data);
        }
    }

    public TileData GetTileData(Vector3Int cellPosition)
    {
        TileBase tile = _map.GetTile(cellPosition);

        if (_tileDataLookup.ContainsKey(tile))
        {
            return _tileDataLookup[tile];
        }

        return null;
    }
}
