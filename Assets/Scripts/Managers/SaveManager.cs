using Zenject;

public class SaveManager
{
    private readonly ISaveSystem saveSystem;
    private SaveData currentSaveData;
    
    [Inject]
    public SaveManager(ISaveSystem saveSystem)
    {
        this.saveSystem = saveSystem;
        LoadGame();
    }
    
    public void SaveGame()
    {
        saveSystem.SaveGame(currentSaveData);
    }
    
    public void LoadGame()
    {
        currentSaveData = saveSystem.LoadGame();
    }
    
    public SaveData GetSaveData()
    {
        return currentSaveData;
    }
    
    public void SetLastSelectedCharacter(string characterName)
    {
        currentSaveData.lastSelectedCharacter = characterName;
        SaveGame();
    }
    
    public void SaveCharacterUpgrades(CharacterData characterData)
    {
        CharacterUpgradeData upgradeData = new CharacterUpgradeData
        {
            walkSpeedLevel = characterData.walkSpeedLevel,
            runSpeedLevel = characterData.runSpeedLevel,
            jumpForceLevel = characterData.jumpForceLevel
        };
        
        currentSaveData.characterUpgrades[characterData.characterName] = upgradeData;
        SaveGame();
    }
    
    public void LoadCharacterUpgrades(CharacterData characterData)
    {
        if (currentSaveData.characterUpgrades.TryGetValue(characterData.characterName, out CharacterUpgradeData upgradeData))
        {
            characterData.walkSpeedLevel = upgradeData.walkSpeedLevel;
            characterData.runSpeedLevel = upgradeData.runSpeedLevel;
            characterData.jumpForceLevel = upgradeData.jumpForceLevel;
        }
        else
        {
            characterData.ResetUpgrades();
        }
    }
}