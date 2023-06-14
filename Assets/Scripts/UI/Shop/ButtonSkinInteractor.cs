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

        private SkinInfo _skinInfo;
        private Button _buttonBuy;
        private ShopBuyHandler _buyHandler;
        private bool _isPurchased;

        public void InitButton(SkinInfo info)
        {
            _buyHandler = GetComponentInParent<ShopBuyHandler>();
            _buttonBuy = GetComponent<Button>();
            _buttonBuy.onClick.AddListener(SelectSkin);

            if (info.iconSprite != null) frontImage.sprite = info.iconSprite;
            _isPurchased = _buyHandler.CheckSkinPurchased(info);
            textCost.text = _isPurchased ? "Select" : info.cost.ToString();
            _skinInfo = info;
        }


        private void SelectSkin()
        {
            if (!_isPurchased)
            {
                if (!_buyHandler.AddNewSkin(_skinInfo)) return;
                _isPurchased = true;
                textCost.text = "Select";
            }
            else
                _buyHandler.SelectCurrentSkin(_skinInfo);
        }
    }
}