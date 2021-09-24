using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private MapManager _mapManager;

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

        if (_mapManager.CanPush(target, direction))
        {
            _mapManager.MoveBlockingObject(target, direction);
        }
        else if (!_mapManager.IsPassable(target))
        {
            return;
        }

        transform.position += direction;
    }
}
