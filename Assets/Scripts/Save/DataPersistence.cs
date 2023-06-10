using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Save
{
    public class DataPersistence : MonoBehaviour
    {
        private List<ILoadDataPersistence> _dataLoadPersistence;
        private List<ISaveDataPersistence> _dataSavePersistence;

        private FileDataHandler _dataHandler;
        public GameData data;
        public static DataPersistence Instance;
        public Action onLoadFinished;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        private void Start()
        {
            _dataHandler = new FileDataHandler() { fileName = "player.allure" };
            _dataLoadPersistence = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ILoadDataPersistence>().ToList();
            _dataSavePersistence = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<ISaveDataPersistence>().ToList();
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

        private void LoadGame()
        {
            _dataHandler.Load(ref data);
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