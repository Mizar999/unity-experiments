using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tilemap _ground;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided");
    }

    private void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Moving");
        Vector3 direction = (Vector3)context.ReadValue<Vector2>();
        if (CanMove(direction))
        {
            transform.position += direction;
        }
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3Int gridPosition = _ground.WorldToCell(transform.position + direction);
        return _ground.HasTile(gridPosition);
    }
}
