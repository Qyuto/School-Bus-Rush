using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

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
            if (!YandexGame.SDKEnabled) YandexGame.GetDataEvent += OnYandexSdkLoad;
            else PreLoadGame();
        }

        private void OnYandexSdkLoad()
        {
            if (YandexGame.SDKEnabled && gameObject.activeSelf)
            {
                PreLoadGame();
                StartCoroutine(CoroutineLoadGame());
            }
        }

        IEnumerator CoroutineLoadGame()
        {
            yield return null;
            LoadGame();
        }

        private void Start()
        {
            if (YandexGame.SDKEnabled) LoadGame();
        }

        private void NewGame()
        {
            data = new GameData(Application.version);
        }

        private void SaveGame()
        {
            foreach (var persistence in _dataSavePersistence) persistence.SaveGame(ref data);
            YandexSaveLoadAdapter.SaveForYandex(data);
            YandexGame.SaveProgress();
            // _dataHandler.Save(data);
        }

        private void PreLoadGame()
        {
            data = YandexSaveLoadAdapter.ConvertYandexSaveToGameData(YandexGame.savesData);
            // _dataHandler.Load(ref data);
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
            YandexGame.GetDataEvent -= OnYandexSdkLoad;
        }
    }
}