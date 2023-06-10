namespace Save
{
    public interface ISaveDataPersistence
    {
        void SaveGame(ref GameData gameData);
    }

    public interface ILoadDataPersistence
    {
        void LoadGame(GameData gameData);
    }
}