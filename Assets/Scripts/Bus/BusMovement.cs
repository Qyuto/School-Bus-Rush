using Bus;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BusMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference pcMoveReference;
    [SerializeField] private InputActionReference mobileMoveReference;
    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private Transform fronLeftWheelModel;
    [SerializeField] private WheelCollider frontRightWheel;
    [SerializeField] private Transform fronRightWheelModel;
    [SerializeField] private WheelCollider backLeftWheel;
    [SerializeField] private WheelCollider backRightWheel;
    [SerializeField] private float velocityLimit = 5;
    [SerializeField] private float busSpeed = 500;
    [SerializeField] private float wheelSteerAngel = 30;

    private float _horizontalMove;
    private Rigidbody _busRigidBody;
    private int _lastIndexFinger;
    private BusLevelCompletion _levelCompletion;
    
    public bool canMove { get; private set; }

    private void Awake()
    {
        _levelCompletion = GetComponent<BusLevelCompletion>();
        _busRigidBody = GetComponent<Rigidbody>();
        frontLeftWheel.motorTorque = busSpeed;
        frontRightWheel.motorTorque = busSpeed;
        _levelCompletion.onBusArrivedAtEnd.AddListener(StopBus);
        _levelCompletion.onBusLevelFailComplete.AddListener(StopBus);
        canMove = true;
    }

    private void OnEnable()
    {
#if Mobile
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        mobileMoveReference.action.Enable();
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += OnAnyFingerDown;
        Touch.onFingerUp += OnAnyFingerUp;
        Touch.onFingerMove += OnAnyFingerMove;
#else
        pcMoveReference.action.performed += PcReadInputValue;
        pcMoveReference.action.canceled += PcReadInputValue;
        pcMoveReference.action.Enable();
#endif
    }

    private void OnDisable()
    {
#if Mobile
        Touch.onFingerDown -= OnAnyFingerDown;
        Touch.onFingerUp -= OnAnyFingerUp;
        Touch.onFingerMove -= OnAnyFingerMove;
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

    private void OnAnyFingerMove(Finger obj)
    {
        if (obj.index != _lastIndexFinger) return;
        if (obj.screenPosition.x >= Screen.width / 2) _horizontalMove = 1;
        else _horizontalMove = -1f;
    }

    private void OnAnyFingerUp(Finger obj)
    {
        if (obj.index == _lastIndexFinger)
            _horizontalMove = 0;
    }

    private void OnAnyFingerDown(Finger obj)
    {
        _lastIndexFinger = obj.index;
        if (obj.screenPosition.x >= Screen.width / 2) _horizontalMove = 1;
        else _horizontalMove = -1f;
    }

    private void FixedUpdate()
    {
        if (canMove)
            MoveBus();
    }

    private void MoveBus()
    {
        float wheelAngelRotation = _horizontalMove * wheelSteerAngel;
        frontLeftWheel.steerAngle = wheelAngelRotation;
        frontRightWheel.steerAngle = wheelAngelRotation;

        fronRightWheelModel.localRotation = Quaternion.Euler(0, wheelAngelRotation, 0);
        fronLeftWheelModel.localRotation = Quaternion.Euler(0, wheelAngelRotation, 0);


        Vector3 currentVelocity = _busRigidBody.velocity;
        if (currentVelocity.magnitude >= velocityLimit)
            _busRigidBody.velocity = currentVelocity.normalized * velocityLimit;
    }

    private void StopBus()
    {
        canMove = false;
        frontLeftWheel.motorTorque = 0;
        frontRightWheel.motorTorque = 0;
        frontLeftWheel.brakeTorque = 500000f;
        frontRightWheel.brakeTorque = 500000f;
        backLeftWheel.brakeTorque = 1000000f;
        backRightWheel.brakeTorque = 1000000f;
    }
}
