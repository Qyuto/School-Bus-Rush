using UnityEngine;

namespace Skins
{
    [CreateAssetMenu(fileName = "Skin_Name", menuName = "Skin/New animated skin", order = 0)]
    public class AnimatedSkinInfo : SkinInfo
    {
        public Avatar avatar;

        public override Transform ReplaceSkin(Transform parent)
        {
            Transform newModel = base.ReplaceSkin(parent);
            Animator animator = parent.GetComponent<Animator>();

            if (animator.avatar != avatar) animator.avatar = avatar;
            return newModel;
        }
    }
}