using System.Collections;
using Bus;
using Passenger;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private BusLevelCompletion levelCompletion;
    [SerializeField] private PassengerCount passengerCount;

    [SerializeField] private float duration;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Vector3 viewCameraOffset;
    [SerializeField] private Vector3 dischargePosition;

    private Camera _camera;
    private Vector3 _dischargeTarget;

    private delegate void AfterMoveRef(ref Vector3 target);

    private void Awake()
    {
        _camera = Camera.main;
        levelCompletion.onBusArrivedAtEnd.AddListener(MoveCameraToDischarge);
        passengerCount.onBusLostPassenger.AddListener(AddDischargeOffset);

        Vector3 target = new Vector3(_camera.transform.position.x, _camera.transform.position.y, cameraTarget.position.z) + viewCameraOffset;
        StartCoroutine(MoveToTarget(target, duration, UpdateCameraTarget));
    }

    private void AddDischargeOffset(IPassengerModifier modifier)
    {
        _dischargeTarget += (Vector3.up - Vector3.forward) * 0.05f;
    }

    private void MoveCameraToDischarge()
    {
        StopAllCoroutines();
        _dischargeTarget = _camera.transform.position + dischargePosition;
        StartCoroutine(MoveToTarget(_dischargeTarget, duration * Time.fixedDeltaTime, UpdateDischargeTarget));
    }

    private void UpdateDischargeTarget(ref Vector3 target)
    {
        target = _dischargeTarget;
    }

    private void UpdateCameraTarget(ref Vector3 target)
    {
        target = new Vector3(_camera.transform.position.x, _camera.transform.position.y, cameraTarget.position.z) + viewCameraOffset;
    }

    private IEnumerator MoveToTarget(Vector3 target, float delta, AfterMoveRef afterMoveAction)
    {
        while (enabled)
        {
            _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, target, delta);
            afterMoveAction?.Invoke(ref target);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_dischargeTarget, 1f);
    }
}