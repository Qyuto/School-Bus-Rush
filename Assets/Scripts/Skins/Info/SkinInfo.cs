using UnityEngine;

namespace Skins
{
    [CreateAssetMenu(fileName = "Skin_Name", menuName = "Skin/New skin", order = 0)]
    public class SkinInfo : ScriptableObject
    {
        public string key;
        public int cost;
        public Sprite iconSprite;
        public GameObject model;
        public SkinType type;

        public virtual Transform ReplaceSkin(Transform parent)
        {
            GameObject newModel = Instantiate(model, parent.transform.position, parent.transform.rotation,
                parent.transform);
            return newModel.transform;
        }
    }
}