using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Save
{
    public class DataPersistence : MonoBehaviour
    {
        private List<IDataPersistence> _dataPersistence;
        private FileDataHandler _dataHandler;
        public GameData data;
        public static DataPersistence Instance; 

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        private void Start()
        {
            _dataHandler = new FileDataHandler() { fileName = "player.allure" };
            _dataPersistence = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>().ToList();
            LoadGame();
        }

        private void NewGame()
        {
            data = new GameData(Application.version);
        }

        private void SaveGame()
        {
            foreach (var persistence in _dataPersistence) persistence.SaveGame(ref data);
            _dataHandler.Save(data);
        }

        private void LoadGame()
        {
            _dataHandler.Load(ref data);
            if ((data != null && data.lastGameVersion != Application.version) || data == null) NewGame();
            foreach (var persistence in _dataPersistence) persistence.LoadGame(data);
        }

        private void OnDestroy()
        {
            SaveGame();
        }
    }
}