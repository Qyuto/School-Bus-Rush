using System.ComponentModel;
using UnityEngine;

namespace Skins
{
    public class LoadMeshSkin : MonoBehaviour
    {
        [SerializeField] private bool immediateLoading = true;
        [SerializeField] private SkinType loadSkinType;
        [SerializeField] private Transform currentModel;

        private void Start()
        {
            if (immediateLoading) LoadSKin();
        }

        public void LoadSKin()
        {
            SkinInfo skinInfo;
            if (currentModel != null) Destroy(currentModel.gameObject);

            switch (loadSkinType)
            {
                case SkinType.Passenger:
                    skinInfo = PlayerSkinCollection.Instance.passengerSkin;
                    break;
                case SkinType.FinishModel:
                    skinInfo = PlayerSkinCollection.Instance.schoolSkin;
                    break;
                default: throw new InvalidEnumArgumentException("Skin type not found");
            }

            currentModel = skinInfo.ReplaceSkin(transform);
            currentModel.localPosition = Vector3.zero;
        }
    }
}