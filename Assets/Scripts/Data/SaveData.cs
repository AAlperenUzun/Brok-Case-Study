using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public Dictionary<string, CharacterUpgradeData> characterUpgrades = new Dictionary<string, CharacterUpgradeData>();
    public string lastSelectedCharacter;
}
[Serializable]
public class CharacterUpgradeData
{
    public int walkSpeedLevel;
    public int runSpeedLevel;
    public int jumpForceLevel;
}