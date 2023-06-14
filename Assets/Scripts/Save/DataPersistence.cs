using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Save
{
    public class DataPersistence : MonoBehaviour
    {
        private List<ILoadDataPersistence> _dataLoadPersistence;
        private List<ISaveDataPersistence> _dataSavePersistence;

        private FileDataHandler _dataHandler;
        public GameData data;
        public Action onLoadFinished;
        public static DataPersistence Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            _dataHandler = new FileDataHandler() { fileName = "player.allure" };
            _dataLoadPersistence = FindObjectsOfType<MonoBehaviour>().OfType<ILoadDataPersistence>().ToList();
            _dataSavePersistence = FindObjectsOfType<MonoBehaviour>().OfType<ISaveDataPersistence>().ToList();
            PreLoadGame();
        }

        private void Start()
        {
            LoadGame();
        }

        private void NewGame()
        {
            data = new GameData(Application.version);
        }

        private void SaveGame()
        {
            foreach (var persistence in _dataSavePersistence) persistence.SaveGame(ref data);
            _dataHandler.Save(data);
        }

        private void PreLoadGame()
        {
            _dataHandler.Load(ref data);
        }

        private void LoadGame()
        {
            if ((data != null && data.lastGameVersion != Application.version) || data == null) NewGame();
            foreach (var persistence in _dataLoadPersistence) persistence.LoadGame(data);
            onLoadFinished?.Invoke();
        }

        private void OnDestroy()
        {
            SaveGame();
            Instance = null;
            onLoadFinished = null;
        }
    }
}