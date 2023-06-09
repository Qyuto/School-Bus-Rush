using System.IO;
using UnityEngine;

namespace Save
{
    public class FileDataHandler
    {
        public string fileName { get; set; }
        
        public void Save(GameData data)
        {
            string patch = Path.Combine(Application.persistentDataPath, "save" , fileName);
            Debug.Log(patch);
            string dataJs = JsonUtility.ToJson(data, true);
            
            Directory.CreateDirectory(Path.GetDirectoryName(patch) ?? string.Empty);
            
            using (FileStream stream = new FileStream(patch,FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                    writer.Write(dataJs);
            }
        }

        public void Load(ref GameData gameData)
        {
            string patch = Path.Combine(Application.persistentDataPath, "save" , fileName);
            if(!File.Exists(patch)) return;
            
            using (FileStream stream = new FileStream(patch,FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string data = reader.ReadToEnd();
                    gameData = JsonUtility.FromJson<GameData>(data);
                }
            }
        }
    }
}