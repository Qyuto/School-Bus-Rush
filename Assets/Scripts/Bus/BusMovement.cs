using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

[RequireComponent(typeof(CharacterController))]
public class BusMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference pcMoveReference;
    [SerializeField] private InputActionReference mobileMoveReference;
    [SerializeField] private float busSpeed;

    public UnityEvent onBusFinished;
    public UnityEvent onBusFailFinished;

    private CharacterController _characterController;
    private float _horizontalMove;
    public bool canMove { get; private set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        canMove = true;
    }

    private void OnEnable()
    {
#if Mobile
        mobileMoveReference.action.performed += MobileReadInputValue;
        mobileMoveReference.action.Enable();
#else
        pcMoveReference.action.performed += PcReadInputValue;
        pcMoveReference.action.canceled += PcReadInputValue;
        pcMoveReference.action.Enable();
#endif
    }

    private void MobileReadInputValue(InputAction.CallbackContext obj)
    {
        var vector2 = Touchscreen.current.primaryTouch.position;
        Debug.Log(Touchscreen.current.primaryTouch.value.phase);
        if (Touchscreen.current.primaryTouch.value.phase == TouchPhase.Ended)
            _horizontalMove = 0;
        else if (vector2.value.x >= Screen.width / 2) _horizontalMove = 1;
        else _horizontalMove = -1f;
    }

    private void OnDisable()
    {
#if Mobile
        mobileMoveReference.action.performed -= MobileReadInputValue;
        mobileMoveReference.action.Disable();
        #else
        pcMoveReference.action.performed -= PcReadInputValue;
        pcMoveReference.action.canceled -= PcReadInputValue;
        pcMoveReference.action.Disable();
#endif
    }

    private void PcReadInputValue(InputAction.CallbackContext obj)
    {
        _horizontalMove = obj.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        if (canMove)
            MoveBus();
    }

    private void MoveBus()
    {
        Vector3 moveDirection =
            (transform.forward + transform.right * _horizontalMove) * busSpeed * Time.fixedDeltaTime;
        _characterController.Move(moveDirection);
    }

    public void BusFinished(bool isWon)
    {
        canMove = false;
        if (isWon) onBusFinished?.Invoke();
        else onBusFailFinished?.Invoke();
    }
}