using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    private ICharacterFactory characterFactory;
    private UIManager uiManager;
    private SaveManager saveManager;
    private List<CharacterData> characterDataList;
    
    private CharacterController currentCharacter;
    private CharacterData currentCharacterData;
    
    [Inject]
    public void Construct(
        ICharacterFactory characterFactory,
        UIManager uiManager,
        SaveManager saveManager,
        List<CharacterData> characterDataList)
    {
        this.characterFactory = characterFactory;
        this.uiManager = uiManager;
        this.saveManager = saveManager;
        this.characterDataList = characterDataList;
    }
    
    private void Start()
    {
        uiManager.Initialize(
            characterDataList,
            OnCharacterSelected,
            OnCharacterUpgraded
        );
        string lastSelectedCharacter = saveManager.GetSaveData().lastSelectedCharacter;
        
        if (!string.IsNullOrEmpty(lastSelectedCharacter))
        {
            CharacterData lastCharacterData = characterDataList.Find(c => c.characterName == lastSelectedCharacter);
            
            if (lastCharacterData != null)
            {
                LoadCharacter(lastCharacterData);
            }
            else
            {
                uiManager.ShowCharacterSelection();
            }
        }
        else
        {
            uiManager.ShowCharacterSelection();
        }
    }
    
    private async void LoadCharacter(CharacterData characterData)
    {
        saveManager.LoadCharacterUpgrades(characterData);
        if (currentCharacter != null)
        {
            characterFactory.ReleaseCharacter(currentCharacter);
            currentCharacter = null;
        }
        currentCharacter = await characterFactory.CreateCharacter(characterData, new Vector3(0, 1, 0));
        currentCharacterData = characterData;
        uiManager.ShowUpgradeUI(characterData);
        saveManager.SetLastSelectedCharacter(characterData.characterName);
    }
    
    private void OnCharacterSelected(CharacterData characterData)
    {
        LoadCharacter(characterData);
    }
    
    private void OnCharacterUpgraded(CharacterData characterData)
    {
        if (currentCharacter != null)
        {
            currentCharacter.UpdateCharacterStats(characterData);
        }
        saveManager.SaveCharacterUpgrades(characterData);
    }
}