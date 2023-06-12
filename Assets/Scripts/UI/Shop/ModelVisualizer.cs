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
            transform.position = Vector3.zero;
            if (_currentModel != null) Destroy(_currentModel);
            _currentModel = Instantiate(info.model, transform.position, transform.rotation, transform);

            if (info.type == SkinType.FinishModel)
            {
                float scaleFactor = 10;
                _currentModel.transform.localScale = Vector3.one / scaleFactor;
                float y = _currentModel.GetComponent<MeshFilter>().mesh.bounds.size.y / 2;
                _currentModel.transform.localPosition += new Vector3(0, y / scaleFactor, 0);
            }
        }

        private void Update()
        {
            if (_currentModel != null)
                _currentModel.transform.Rotate(0f, 1f, 0);
        }
    }
}