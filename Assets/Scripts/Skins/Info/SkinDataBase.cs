using System.Collections.Generic;
using UnityEngine;

namespace Skins
{
    [CreateAssetMenu(fileName = "SkinDataBase", menuName = "Skin/DataBase", order = 0)]
    public class SkinDataBase : ScriptableObject
    {
        [SerializeField] private List<SkinInfo> skinsData;
        public List<SkinInfo> SkinsData => skinsData;
        public SkinInfo FindSkinByKey(string key) => skinsData.Find(info => info.key == key);
    }
}