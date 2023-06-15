using System.ComponentModel;
using UnityEngine;

namespace Skins
{
    public class LoadMeshSkin : MonoBehaviour
    {
        [SerializeField] private bool immediateLoading = true;
        [SerializeField] private SkinType loadSkinType;
        private Transform _currentModel;

        private void Start()
        {
            if (immediateLoading) LoadSKin();
        }

        public void LoadSKin()
        {
            SkinInfo skinInfo;
            if (_currentModel != null) Destroy(_currentModel.gameObject);

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

            _currentModel = skinInfo.ReplaceSkin(transform);
            _currentModel.localPosition = Vector3.zero;
        }
    }
}