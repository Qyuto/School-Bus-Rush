using UnityEngine;

public class TransformSizeScaler
{
    private readonly Transform _transform;

    public TransformSizeScaler(Transform transform)
    {
        _transform = transform;
    }

    public void IncreaseScale(Vector3 addScale, bool autoLifting)
    {
        float positionDifferent = _transform.position.y / _transform.localScale.y;
        _transform.localScale += addScale;
        if (!autoLifting) return;
        _transform.position = new Vector3(_transform.position.x + addScale.x, positionDifferent * _transform.localScale.y,
            _transform.position.z + addScale.z);
    }

    public void IncreaseScale(float size, bool autoLifting)
    {
        Vector3 addScale = new Vector3(size, size, size);
        float positionDifferent = _transform.position.y / _transform.localScale.y;
        _transform.localScale += addScale;
        if (!autoLifting) return;
        _transform.position = new Vector3(_transform.position.x + addScale.x, positionDifferent * _transform.localScale.y,
            _transform.position.z + addScale.z);
    }

    public void ReduceScale(Vector3 reduceScale, bool autoLifting)
    {
        float positionDifferent = _transform.position.y / _transform.localScale.y;
        _transform.localScale -= reduceScale;
        if (!autoLifting) return;
        _transform.position = new Vector3(_transform.position.x - reduceScale.x,
            positionDifferent / _transform.localScale.y,
            _transform.position.z - reduceScale.z);
    }

    public void ReduceScale(float size, bool autoLifting)
    {
        Vector3 reduceScale = new Vector3(size, size, size);
        float positionDifferent = _transform.position.y / _transform.localScale.y;
        _transform.localScale -= reduceScale;
        if (!autoLifting) return;
        _transform.position = new Vector3(_transform.position.x - reduceScale.x,
            positionDifferent / _transform.localScale.y,
            _transform.position.z - reduceScale.z);
    }

    public void SetScale(Vector3 newScale, bool autoLifting)
    {
        float positionDifferent = _transform.position.y / _transform.localScale.y;
        _transform.localScale = newScale;
        if (!autoLifting) return;
        _transform.position = new Vector3(_transform.position.x, positionDifferent * _transform.localScale.y,
            _transform.position.z);
    }
}