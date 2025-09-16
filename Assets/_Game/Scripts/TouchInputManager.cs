using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction _touchPressAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        //_touchPressAction = _playerInput.actions["BackBtnTouched"];
    }

    private void OnEnable()
    {
        Debug.Log("Touch Pressed");
        _touchPressAction.performed += TouchPressed;
    }

    private void OnDisable()
    {
        Debug.Log("Touch Disabled");
        _touchPressAction.canceled -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        context.ReadValueAsButton();
    }
}
