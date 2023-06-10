using UnityEngine;

namespace Skins
{
    public class ChangePassengerMesh : ChangeMesh
    {
        [SerializeField] private Animator animator;

        protected override void StartChangeModel()
        {
            ChangeModel(SkinDataCollection.Instance.passengerSkin);
        }

        public override void ChangeModel(SkinInfo skinInfo)
        {
            if (currentModel != null)
            {
                if (animator.avatar == skinInfo.avatar) return;
                Destroy(currentModel.gameObject);
            }

            currentModel = Instantiate(skinInfo.model, transform.position, transform.rotation, transform)
                .transform;
            currentModel.localPosition = Vector3.zero;
            animator.avatar = skinInfo.avatar;
        }
    }
}