using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = new PlayerMovement();
    }

    private void OnEnable()
    {
        _playerMovement.Player.Movement.Enable();
    }

    private void OnDisable()
    {
        _playerMovement.Player.Movement.Disable();
    }

    private void Update() {
        Debug.LogFormat("Movement values {0}", _playerMovement.Player.Movement.ReadValue<Vector2>());
    }
}
