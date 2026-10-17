using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IndieGameManagersSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameGameControllerSystem
{
    public class SwitchCharacterListeners : MonoBehaviour
    {
        [Space(17)]
        [Header("Text Details UI")]
        [SerializeField] private TMP_Text textDetails1;
        [SerializeField] private TMP_Text textDetails2;
        [SerializeField] private TMP_Text textDetails3;
        [SerializeField] private TMP_Text textDetails4;

        public void _NextCharacter()
        {
            GameController gameController = GameManager.GetInstance._GetGameController();
            GameControllerData gameControllerData = gameController._GameControllerData;

            if (CharacterManager.GetInstance._Characters.Count - 1 > gameControllerData._CharacterIndex)
            {
                gameControllerData._CharacterIndex++;
            }
            else
            {
                gameControllerData._CharacterIndex = 0;
            }

            gameController._Character = CharacterManager.GetInstance._GetCharacterByIndex(gameControllerData._CharacterIndex);
            gameControllerData._CharacterName = gameController._Character.name;
            gameControllerData._CharacterAllyType = gameController._Character._AllyType;
            gameControllerData._CharacterCameraNameType = gameController._Character._CharacterNameType;

            setCharacterDetails(gameController._Character);
            CameraManager.GetInstance._SelectedCharacters(gameControllerData._CharacterCameraNameType);
        }

        public void _PrevCharacter()
        {
            GameController gameController = GameManager.GetInstance._GetGameController();
            GameControllerData gameControllerData = gameController._GameControllerData;

            if (gameControllerData._CharacterIndex > 0)
            {
                gameControllerData._CharacterIndex--;
            }
            else
            {
                gameControllerData._CharacterIndex = CharacterManager.GetInstance._Characters.Count - 1;
            }

            gameController._Character = CharacterManager.GetInstance._GetCharacterByIndex(gameControllerData._CharacterIndex);
            gameControllerData._CharacterName = gameController._Character.name;
            gameControllerData._CharacterAllyType = gameController._Character._AllyType;
            gameControllerData._CharacterCameraNameType = gameController._Character._CharacterNameType;

            setCharacterDetails(gameController._Character);
            CameraManager.GetInstance._SelectedCharacters(gameControllerData._CharacterCameraNameType);
        }

        private void setCharacterDetails(Character character)
        {
            textDetails1.text = "Name: " + character.name.ToString();
            textDetails2.text = "Strength: " + character._HealthSystem._DefenseData._STR;
            textDetails3.text = "Agility: " + character._HealthSystem._DefenseData._AGI;
            textDetails4.text = "Intelligence: " + character._HealthSystem._DefenseData._INT;
        }

        public void _CharacterLockin()
        {
            CameraManager.GetInstance._SelectedCharacters(_ECameraCharacterSelectParameters.Default);
        }
    }
}