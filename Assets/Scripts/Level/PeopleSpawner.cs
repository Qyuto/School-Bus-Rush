using System.Collections.Generic;
using Save;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level.Spawner
{
    public class PeopleSpawner : MonoBehaviour,IDataPersistence
    {
        [SerializeField] private List<GroupPeople> peoplesPrefab;
        [SerializeField] private List<Transform> spawnPositions;

        private void SpawnPeople()
        {
            foreach (var spawnTransform in spawnPositions)
            {
                GroupPeople people = Instantiate(GetRandomPrefab(), spawnTransform.position, Quaternion.identity,
                    spawnTransform);
            }
        }

        private GroupPeople GetRandomPrefab() => peoplesPrefab[Random.Range(0, peoplesPrefab.Count)];

        public void LoadGame(GameData gameData)
        {
            SpawnPeople();
        }

        public void SaveGame(ref GameData gameData)
        {
        }
    }
}