using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class CharacterFactory : ICharacterFactory
{
    private readonly DiContainer container;
    
    [Inject]
    public CharacterFactory(DiContainer container)
    {
        this.container = container;
    }
    
    public async Task<CharacterController> CreateCharacter(CharacterData characterData, Vector3 position)
    {
        try
        {
            AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(characterData.addressableKey);
            await asyncOperationHandle.Task;

            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject characterPrefab = asyncOperationHandle.Result;
                
                if (characterPrefab == null)
                {
                    return null;
                }
                GameObject characterGO = GameObject.Instantiate(characterPrefab, position, Quaternion.identity);
                MovementContext movementContext = characterGO.GetComponent<MovementContext>();
                if (movementContext == null)
                {
                    movementContext = characterGO.AddComponent<MovementContext>();
                }
                DefaultMovementStrategy movementStrategy = new DefaultMovementStrategy(characterData);
                movementContext.Construct(movementStrategy);
                CharacterController characterController = characterGO.GetComponent<CharacterController>();
                if (characterController == null)
                {
                    characterController = characterGO.AddComponent<CharacterController>();
                }
                characterController.Construct(movementContext);
                characterController.Initialize(characterData);
                return characterController;
            }
            else
            {
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }
    
    public void ReleaseCharacter(CharacterController character)
    {
        if (character != null && character.gameObject != null)
        {
            Addressables.Release(character.gameObject);
            GameObject.Destroy(character.gameObject);
        }
    }
}