using Bus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using YG;

public class BusMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference pcMovementReference;
    [SerializeField] private FloatingJoystick floatingJoystick;
    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private Transform frontLeftWheelModel;
    [SerializeField] private WheelCollider frontRightWheel;
    [SerializeField] private Transform frontRightWheelModel;
    [SerializeField] private WheelCollider backLeftWheel;
    [SerializeField] private WheelCollider backRightWheel;
    [SerializeField] private float velocityLimit = 5;
    [SerializeField] private float busSpeed = 500;
    [SerializeField] private float wheelSteerAngel = 30;

    private float _horizontalMove;
    private float _horizontalKeyboardValue;
    private Rigidbody _busRigidBody;
    private int _lastIndexFinger;
    private BusLevelCompletion _levelCompletion;

    public bool canMove { get; private set; }

    private void Awake()
    {
        _levelCompletion = GetComponent<BusLevelCompletion>();
        _busRigidBody = GetComponent<Rigidbody>();

        _levelCompletion.onBusArrivedAtEnd.AddListener(StopBus);
        _levelCompletion.onBusLevelFailComplete.AddListener(StopBus);
    }

    private void OnEnable()
    {
#if Mobile
        mobileMoveReference.action.Enable();
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown += OnAnyFingerDown;
        Touch.onFingerUp += OnAnyFingerUp;
        Touch.onFingerMove += OnAnyFingerMove;
#endif
#if YandexSDK
        floatingJoystick.gameObject.SetActive(true);
        pcMovementReference.action.performed += ReadValue;
        pcMovementReference.action.canceled += ReadValue;
        pcMovementReference.action.Enable();
#endif
        frontLeftWheel.motorTorque = busSpeed;
        frontRightWheel.motorTorque = busSpeed;
        canMove = true;
    }

    private void ReadValue(InputAction.CallbackContext obj)
    {
        _horizontalKeyboardValue = obj.ReadValue<float>();
    }

    private void OnDisable()
    {
#if Mobile
        Touch.onFingerDown -= OnAnyFingerDown;
        Touch.onFingerUp -= OnAnyFingerUp;
        Touch.onFingerMove -= OnAnyFingerMove;
        mobileMoveReference.action.Disable();
#endif
#if YandexSDK
        pcMovementReference.action.performed -= ReadValue;
        pcMovementReference.action.canceled -= ReadValue;
        pcMovementReference.action.Disable();
#endif
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
#if YandexSDK

        _horizontalMove = floatingJoystick.isWorking ? floatingJoystick.Horizontal : _horizontalKeyboardValue;
        float wheelAngelRotation = 0;
        wheelAngelRotation = _horizontalMove < 0 ? Mathf.Lerp(0, -30, Mathf.Abs(_horizontalMove)) : Mathf.Lerp(0, 30, Mathf.Abs(_horizontalMove));
#else
        float wheelAngelRotation = _horizontalMove * wheelSteerAngel;
#endif

        frontLeftWheel.steerAngle = wheelAngelRotation;
        frontRightWheel.steerAngle = wheelAngelRotation;

        frontRightWheelModel.localRotation = Quaternion.Euler(0, wheelAngelRotation, 0);
        frontLeftWheelModel.localRotation = Quaternion.Euler(0, wheelAngelRotation, 0);


        Vector3 currentVelocity = _busRigidBody.velocity;
        if (currentVelocity.magnitude >= velocityLimit)
            _busRigidBody.velocity = currentVelocity.normalized * velocityLimit;
    }

    private void StopBus()
    {
        floatingJoystick.gameObject.SetActive(false);
        canMove = false;
        frontLeftWheel.motorTorque = 0;
        frontRightWheel.motorTorque = 0;
        frontLeftWheel.brakeTorque = 500000f;
        frontRightWheel.brakeTorque = 500000f;
        backLeftWheel.brakeTorque = 1000000f;
        backRightWheel.brakeTorque = 1000000f;
    }
}