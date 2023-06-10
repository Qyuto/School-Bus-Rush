using UnityEngine;

namespace Skins
{
    public class ChangeMesh : MonoBehaviour
    {
        [SerializeField] private bool startChange;
        [SerializeField] protected Transform currentModel;

        protected void Start()
        {
            if(startChange) StartChangeModel();
        }

        protected virtual void StartChangeModel()
        {
            ChangeModel(SkinDataCollection.Instance.schoolSkin);
        }
        
        public virtual void ChangeModel(SkinInfo skinInfo)
        {
            if (currentModel != null) Destroy(currentModel.gameObject);
            currentModel = Instantiate(skinInfo.model, transform.position, transform.rotation, transform)
                .transform;
            currentModel.localPosition = Vector3.zero;
        }
    }
}