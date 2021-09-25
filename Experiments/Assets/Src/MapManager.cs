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
    private IntegerVariable _matchingBoxes;

    private Dictionary<TileBase, TileData> _tileDataLookup;

    private void Awake()
    {
        Initialize();
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

        TileData data = GetTileData(tile);
        if (data != null && data.Type == _boxType)
        {
            if (IsOnMatchingTarget(fromCell, data.Class))
            {
                --_matchingBoxes.Value;
            }
            if (IsOnMatchingTarget(toCell, data.Class))
            {
                ++_matchingBoxes.Value;
            }
            Debug.LogFormat("Boxes on target: {0}", _matchingBoxes.Value);
        }
    }

    private void Initialize()
    {
        _targets.Value = 0;
        _matchingBoxes.Value = 0;

        _tileDataLookup = new Dictionary<TileBase, TileData>();
        foreach (var data in _tileData)
        {
            _tileDataLookup.Add(data.Tile, data);
        }

        TileData tileData;
        Vector3Int cell;
        foreach (var pos in _passableObjects.cellBounds.allPositionsWithin)
        {
            cell = new Vector3Int(pos.x, pos.y, pos.z);
            if (_passableObjects.HasTile(cell))
            {
                tileData = GetTileData(_passableObjects.GetTile(cell));
                if (tileData != null && tileData.Type == _targetType)
                {
                    ++_targets.Value;
                }
            }
        }
    }

    private bool IsOnMatchingTarget(Vector3Int cell, ObjectClass objectClass)
    {
        if (_passableObjects.HasTile(cell))
        {
            TileData data = GetTileData(_passableObjects.GetTile(cell));
            if (data != null && data.Type == _targetType)
            {
                return data.Class == objectClass;
            }
        }
        return false;
    }
}
