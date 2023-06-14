using System.Collections.Generic;
using Passenger;
using Save;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level.Spawner
{
    public class PeopleSpawner : MonoBehaviour, ILoadDataPersistence
    {
        [SerializeField] private List<PassengerGroup> peoplesPrefab;
        [SerializeField] private List<Transform> spawnPositions;

        private void SpawnPeople()
        {
            foreach (var spawnTransform in spawnPositions)
            {
                PassengerGroup people = Instantiate(GetRandomPrefab(), spawnTransform.position, Quaternion.identity, spawnTransform);
            }
        }

        private PassengerGroup GetRandomPrefab() => peoplesPrefab[Random.Range(0, peoplesPrefab.Count)];

        public void LoadGame(GameData gameData)
        {
            SpawnPeople();
        }
    }
}