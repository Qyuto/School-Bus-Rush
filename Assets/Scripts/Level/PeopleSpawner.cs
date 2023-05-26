using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Level.Spawner
{
    public class PeopleSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> peoplesPrefab;
        [SerializeField] private List<Transform> spawnPositions;

        private void Start()
        {
            SpawnPeople();
        }

        private void SpawnPeople()
        {
            foreach (var spawnTransform in spawnPositions)
                Instantiate(GetRandomPrefab(), spawnTransform.position, Quaternion.identity, spawnTransform);
        }

        private GameObject GetRandomPrefab() => peoplesPrefab[Random.Range(0, peoplesPrefab.Count)];
    }
}

