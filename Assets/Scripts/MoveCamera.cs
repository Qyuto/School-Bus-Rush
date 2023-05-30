using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float deltaMove;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Vector3 cameraOffset;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position,
            new Vector3(_camera.transform.position.x, _camera.transform.position.y, cameraTarget.position.z) + cameraOffset,
            deltaMove * Time.deltaTime);
    }
}