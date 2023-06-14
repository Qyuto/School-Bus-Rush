using UnityEngine;

namespace Skins
{
    [CreateAssetMenu(fileName = "Skin_Name", menuName = "Skin/New animated skin", order = 0)]
    public class AnimatedSkinInfo : SkinInfo
    {
        public Avatar avatar;

        public override Transform ReplaceSkin(Transform parent)
        {
            GameObject newModel = Instantiate(model, parent.transform.position, parent.transform.rotation,
                parent.transform);
            parent.GetComponent<Animator>().avatar = avatar;
            return newModel.transform;
        }
    }
}