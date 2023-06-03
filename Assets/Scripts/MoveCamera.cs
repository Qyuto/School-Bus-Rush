using Bus;
using DG.Tweening;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private BusLevelCompletion levelCompletion;
    [SerializeField] private float deltaMove;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Vector3 viewCameraOffset;
    [SerializeField] private Vector3 finishLevelPosition;
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
        levelCompletion.onBusArrivedAtEnd.AddListener(MoveCameraToSecondPos);
    }

    private void MoveCameraToSecondPos()
    {
        StopAllCoroutines();
        _camera.transform.DOLocalMove(transform.position + finishLevelPosition, 1);
    }

    private void FixedUpdate()
    {
        if(!levelCompletion.isArrived)
            MoveToMainTarget();
    }

    private void MoveToMainTarget()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position,
            new Vector3(_camera.transform.position.x, _camera.transform.position.y, cameraTarget.position.z) + viewCameraOffset,
            deltaMove * Time.deltaTime);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, cameraTarget.position.z) + viewCameraOffset,1f);
        Gizmos.DrawWireSphere(transform.position + finishLevelPosition,1f);
    }
}