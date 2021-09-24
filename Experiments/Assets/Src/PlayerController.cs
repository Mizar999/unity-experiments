using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tilemap _ground;
    [SerializeField]
    private Tilemap _objects;
    [SerializeField]
    private ObjectType _boxType;
    [SerializeField]
    private ObjectType _targetType;
    [SerializeField]
    private TileDataManager _tileDataManager;
    [SerializeField]
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = new PlayerMovement();
    }

    private void OnEnable()
    {
        _playerMovement.Enable();
    }

    private void OnDisable()
    {
        _playerMovement.Disable();
    }

    private void Start()
    {
        _playerMovement.Player.Movement.performed += context => Move(context);
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector3 direction = (Vector3)context.ReadValue<Vector2>();
        Vector3 target = transform.position + direction;

        if (CanMove(target))
        {
            transform.position = target;
        }
    }

    private bool CanMove(Vector3 target)
    {
        Vector3Int cellPosition = _ground.WorldToCell(target);

        if (_objects.HasTile(cellPosition))
        {
            // TODO handle box collision & movement
            TileData data = _tileDataManager.GetTileData(cellPosition);
            if (data == null || data.Type == _boxType)
            {
                return false;
            }
        }

        return _ground.HasTile(cellPosition);
    }
}
