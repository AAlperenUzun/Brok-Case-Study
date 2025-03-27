using System.Threading.Tasks;
using UnityEngine;

public interface ICharacterFactory
{
    Task<CharacterController> CreateCharacter(CharacterData characterData, Vector3 position);
    void ReleaseCharacter(CharacterController character);
}