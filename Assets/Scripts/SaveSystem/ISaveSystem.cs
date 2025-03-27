// Save System Interface
public interface ISaveSystem
{
    void SaveGame(SaveData saveData);
    SaveData LoadGame();
    bool SaveExists();
}