using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 movementInput;

    public float VerticalMovementInput => movementInput.y;
    public float HorizontalMovementInput => movementInput.x;

    private PlayerInput _playerInput;
    
    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();

            _playerInput.PlayerMovement.Movement.performed += callbackContext => movementInput = callbackContext.ReadValue<Vector2>();
        }
        
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
    
    public void HandleAllInput()
    {
        
    }
}