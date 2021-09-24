using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap _ground;
    [SerializeField]
    private Tilemap _passableObjects;
    [SerializeField]
    private Tilemap _blockingObjects;

    [SerializeField]
    private ObjectType _boxType;
    [SerializeField]
    private List<TileData> _tileData;

    private Dictionary<TileBase, TileData> _tileDataLookup;

    private void Awake()
    {
        _tileDataLookup = new Dictionary<TileBase, TileData>();
        foreach (var data in _tileData)
        {
            _tileDataLookup.Add(data.Tile, data);
        }
    }

    public TileData GetTileData(TileBase tile)
    {
        if (_tileDataLookup.ContainsKey(tile))
        {
            return _tileDataLookup[tile];
        }
        return null;
    }

    public bool IsPassable(Vector3 target)
    {
        Vector3Int cell = _ground.WorldToCell(target);
        return _ground.HasTile(cell) && !_blockingObjects.HasTile(cell);
    }

    public bool CanPush(Vector3 target, Vector3 direction)
    {
        Vector3Int cell = _ground.WorldToCell(target);
        if (_blockingObjects.HasTile(cell))
        {
            TileData data = GetTileData(_blockingObjects.GetTile(cell));
            if (data != null && data.Type == _boxType)
            {
                return IsPassable(target + direction);
            }
        }

        return false;
    }

    public void MoveBlockingObject(Vector3 fromPosition, Vector3 direction)
    {
        Vector3Int fromCell = _blockingObjects.WorldToCell(fromPosition);
        Vector3Int toCell = _blockingObjects.WorldToCell(fromPosition + direction);
        TileBase tile = _blockingObjects.GetTile(fromCell);

        _blockingObjects.SetTile(fromCell, null);
        _blockingObjects.SetTile(toCell, tile);
    }
}
