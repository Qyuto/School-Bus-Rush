namespace Save
{
    public interface IDataPersistence
    {
        public void LoadGame(GameData gameData);
        public void SaveGame(ref GameData gameData);
    }
}