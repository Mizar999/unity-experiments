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
        Vector3Int gridPosition = _ground.WorldToCell(target);
        return !_objects.HasTile(gridPosition) && _ground.HasTile(gridPosition);
    }
}
