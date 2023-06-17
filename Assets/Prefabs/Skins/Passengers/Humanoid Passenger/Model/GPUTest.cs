using UnityEngine;

public class GPUTest : MonoBehaviour
{
    private void Awake()
    {
        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
        SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();
        renderer.SetPropertyBlock(propertyBlock);
    }
}
