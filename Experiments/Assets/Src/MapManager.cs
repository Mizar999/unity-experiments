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
    private ObjectType _targetType;
    [SerializeField]
    private List<TileData> _tileData;

    [SerializeField]
    private IntegerVariable _targets;
    [SerializeField]
    private IntegerVariable _correctBoxes;

    private Dictionary<TileBase, TileData> _tileDataLookup;

    private void Awake()
    {
        _targets.Value = 0;
        _correctBoxes.Value = 0;

        _tileDataLookup = new Dictionary<TileBase, TileData>();
        foreach (var data in _tileData)
        {
            _tileDataLookup.Add(data.Tile, data);
        }

        CountAllTargets();
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

    private void CountAllTargets()
    {
        TileData data;
        Vector3Int cell;
        foreach (var pos in _passableObjects.cellBounds.allPositionsWithin)
        {
            cell = new Vector3Int(pos.x, pos.y, pos.z);
            data = GetTileData(_passableObjects.GetTile(cell));
            if (data != null && data.Type == _targetType)
            {
                ++_targets.Value;
            }
        }
    }
}
