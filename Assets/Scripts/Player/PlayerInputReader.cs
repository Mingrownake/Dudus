using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private Player _player;

    public void OnGetMovementInput(InputAction.CallbackContext action)
    {
        Vector2 movementDirection = action.ReadValue<Vector2>();
        _player.SetDirection(movementDirection);
    }

    public void OnAttack(InputAction.CallbackContext action)
    {
        if (action.canceled)
        {
            
        }
    }
}
