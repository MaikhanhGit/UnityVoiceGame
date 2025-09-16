using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private TouchControl _touchControl;

    private void Awake()
    {
        _touchControl = new TouchControl();
    }

    private void OnEnable()
    {
        _touchControl.Enable();
    }

    private void OnDisable()
    {
        _touchControl?.Disable();
    }

    private void Start()
    {
        _touchControl.Touch.TouchPress.started += ctx => StartTouch(ctx);
        _touchControl.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Start Touch");
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("End Touch");
    }

}
