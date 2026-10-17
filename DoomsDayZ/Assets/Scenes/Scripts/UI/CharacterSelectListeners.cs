using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using IndieGameManagersSystem;
using IndieGameCharacterControllerSystem;

namespace IndieGameGameControllerSystem
{
    public class CharacterSelectListeners : MonoBehaviour
    {
        [Header("Text Details UI")]
        //[SerializeField] private TextMeshProUGUI or TextMeshPro (TMP_Text)
        [SerializeField] private TMP_Text textDetails1;
        [SerializeField] private TMP_Text textDetails2;
        [SerializeField] private TMP_Text textDetails3;
        [SerializeField] private TMP_Text textDetails4;

        public void _Characters()
        {
            GameController gameController = GameManager.GetInstance._GetGameController();
            GameControllerData gameControllerData = gameController._GameControllerData;

            if (null == gameController._Character)
            {
                gameController._Character = CharacterManager.GetInstance._GetRandomCharacter;
                gameControllerData._CharacterIndex = CharacterManager.GetInstance._GetIndexByCharacter(gameController._Character);
                gameControllerData._CharacterName = gameController._Character.name;
                gameControllerData._CharacterAllyType = gameController._Character._AllyType;
                gameControllerData._CharacterCameraNameType = gameController._Character._CharacterNameType;
            }

            setCharacterDetails(gameController._Character);
            CameraManager.GetInstance._SelectedCharacters(gameController._Character._CharacterNameType);
        }

        private void setCharacterDetails(Character character)
        {
            textDetails1.text = "Name: " + character.name.ToString();
            textDetails2.text = "Strength: " + character._HealthSystem._DefenseData._STR;
            textDetails3.text = "Agility: " + character._HealthSystem._DefenseData._AGI;
            textDetails4.text = "Intelligence: " + character._HealthSystem._DefenseData._INT;
        }
    }
}