using System;
using Skins;
using UnityEngine;

namespace UI.Shop
{
    public class ModelVisualizer : MonoBehaviour
    {
        [SerializeField] private ShopBuyHandler shopHandler;
        private GameObject _currentModel;

        private void Awake()
        {
            shopHandler.onCurrentSkinChange += ChangeModel;
        }

        private void ChangeModel(SkinInfo info)
        {
            transform.localScale = Vector3.one;
            if (_currentModel != null) Destroy(_currentModel);
            _currentModel = Instantiate(info.model, transform.position, transform.rotation, transform);

            if (info.type == SkinType.FinishModel)
                transform.localScale = Vector3.one / 10;
        }

        private void Update()
        {
            if (_currentModel != null)
                _currentModel.transform.Rotate(0f, 1f, 0);
        }
    }
}