using UnityEngine;

namespace Skins
{
    [CreateAssetMenu(fileName = "Cosmetic_Name", menuName = "Cosmetics", order = 0)]
    public class SkinInfo : ScriptableObject
    {
        public string key;
        public int cost;
        public Sprite iconSprite;
        public Avatar avatar;
        public GameObject model;
        public SkinType type;
    }

    public enum SkinType
    {
        Passenger,
        FinishModel
    }
}