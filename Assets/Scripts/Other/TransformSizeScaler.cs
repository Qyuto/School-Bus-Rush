using UnityEngine;

public class TransformSizeScaler : MonoBehaviour
{
    public void IncreaseScale(Vector3 addScale, bool autoLifting)
    {
        float positionDifferent = transform.position.y / transform.localScale.y;
        transform.localScale += addScale;
        if (!autoLifting) return;
        transform.position = new Vector3(transform.position.x + addScale.x, positionDifferent * transform.localScale.y,
            transform.position.z + addScale.z);
    }

    public void IncreaseScale(float size, bool autoLifting)
    {
        Vector3 addScale = new Vector3(size, size, size);
        float positionDifferent = transform.position.y / transform.localScale.y;
        transform.localScale += addScale;
        if (!autoLifting) return;
        transform.position = new Vector3(transform.position.x + addScale.x, positionDifferent * transform.localScale.y,
            transform.position.z + addScale.z);
    }

    public void ReduceScale(Vector3 reduceScale, bool autoLifting)
    {
        float positionDifferent = transform.position.y / transform.localScale.y;
        transform.localScale -= reduceScale;
        if (!autoLifting) return;
        transform.position = new Vector3(transform.position.x - reduceScale.x,
            positionDifferent / transform.localScale.y,
            transform.position.z - reduceScale.z);
    }

    public void ReduceScale(float size, bool autoLifting)
    {
        Vector3 reduceScale = new Vector3(size, size, size);
        float positionDifferent = transform.position.y / transform.localScale.y;
        transform.localScale -= reduceScale;
        if (!autoLifting) return;
        transform.position = new Vector3(transform.position.x - reduceScale.x,
            positionDifferent / transform.localScale.y,
            transform.position.z - reduceScale.z);
    }

    public void SetScale(Vector3 newScale, bool autoLifting)
    {
        float positionDifferent = transform.position.y / transform.localScale.y;
        transform.localScale = newScale;
        if (!autoLifting) return;
        transform.position = new Vector3(transform.position.x, positionDifferent * transform.localScale.y,
            transform.position.z);
    }
}