using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class BusMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference pcMoveReference;
    [SerializeField] private float busSpeed;

    public UnityEvent onBusFinished;
    private CharacterController _characterController;
    private float _horizontalMove;
    public bool canMove { get; private set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        onBusFinished.AddListener(StopBus);
        canMove = true;
    }

    private void OnEnable()
    {
        pcMoveReference.action.performed += PcReadInputValue;
        pcMoveReference.action.canceled += PcReadInputValue;
        pcMoveReference.action.Enable();
    }

    private void OnDisable()
    {
        pcMoveReference.action.performed -= PcReadInputValue;
        pcMoveReference.action.canceled -= PcReadInputValue;
        pcMoveReference.action.Disable();
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

    private void StopBus()
    {
        canMove = false;
    }
}