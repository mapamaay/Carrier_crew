using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerController1 _playerController1;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        var playersController = FindObjectsOfType<PlayerController1>();
        var index = _playerInput.playerIndex;
        _playerController1 = playersController.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    public void OnMove(CallbackContext context)
    {
        if (_playerController1 != null)
        {
            _playerController1.SetInputVector(context.ReadValue<Vector2>());
        }
    }

    public void OnInteract(CallbackContext context)
    {
        if (_playerController1 != null)
        {
            _playerController1.OnInteract(context.phase);
        }
    }
}
