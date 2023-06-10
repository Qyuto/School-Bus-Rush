using Save;
using Skins;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class ButtonSkinInteractor : MonoBehaviour
    {
        [SerializeField] private Image frontImage;
        [SerializeField] private TextMeshProUGUI textCost;
        [SerializeField] private SkinInfo skinInfo;

        private Button _buttonBuy;
        private ShopBuyHandler _buyHandler;
        private bool _isPurchased;

        private void Awake()
        {
            _buyHandler = GetComponentInParent<ShopBuyHandler>();
            _buttonBuy = GetComponent<Button>();
            _buttonBuy.onClick.AddListener(SelectSkin);
        }

        private void Start()
        {
            DataPersistence.Instance.onLoadFinished += OnLoadFinished;
        }

        private void OnLoadFinished()
        {
            if (skinInfo.iconSprite != null) frontImage.sprite = skinInfo.iconSprite;
            _isPurchased = _buyHandler.CheckSkinPurchased(skinInfo);
            textCost.text = _isPurchased ? "Select" : skinInfo.cost.ToString();
        }

        private void SelectSkin()
        {
            if (!_isPurchased)
            {
                if (!_buyHandler.AddNewSkin(skinInfo)) return;
                _isPurchased = true;
                textCost.text = "Select";
            }
            else
                _buyHandler.SelectCurrentSkin(skinInfo);
        }
    }
}