using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using IndieGameCharacterControllerSystem;
using IndieGameGameControllerSystem;
using IndieGameManagersSystem;
using IndieGameWeaponSystem;

namespace IndieGameUISystem
{
    public class GameUI : MonoBehaviour
    {
        public Slider _SliderScreenUIPlayerHealthBar;
        public Slider _SliderScreenUIPlayerStaminaBar;
        public GameController _GameController;
        public List<CanvasContainerBase> _CanvasContainerList = new List<CanvasContainerBase>();

        //some tester of scriptable object
        #region tester scriptable object
        //[SerializeField] private WeaponScriptableTester tester;
        //[SerializeField] private WeaponTesterData testerData;
        //[SerializeField] private WeaponAttributes attributes;
        private WeaponMelee weaponMelee;
        #endregion

        private bool menuPanel;
        [SerializeField] private GameObject menuPanelObject;

        private void Awake()
        {
            if (null != menuPanelObject)
            {
                menuPanelObject.SetActive(false);
            }

            Time.timeScale = 0.2f;
        }

        private void Start()
        {
            if (null == _GameController)
            {
                _GameController = GameManager.GetInstance._GetGameController();
                setCharacterHealthBar();
                setCharacterStamina();
            }
        }

        private void Update()
        {
            if (VirtualInputManager.GetInstance._IsPressedESC)
            {
                menuPanel = !menuPanel;
                menuPanelObject.SetActive(menuPanel);
            }

            //check if the game is paused
            Time.timeScale = menuPanelObject.activeInHierarchy ? 0 : 1;
        }

        public void _AddDicCanvasContainer(CanvasContainerBase container)
        {
            if (!_CanvasContainerList.Contains(container))
            {
                _CanvasContainerList.Add(container);
            }
        }

        public GameObject _GetDamageTextRootParent()
        {
            return Instantiate(Resources.Load<GameObject>("DamageTextRootParent"));
        }

        private void setCharacterHealthBar()
        {
            foreach (CanvasContainerBase container in _CanvasContainerList)
            {
                if (container._CanvasContainerType == _ECanvasContainerType.CharacterCanvas)
                {
                    CharacterCanvasContainer characterCanvasContainer = (CharacterCanvasContainer)container;

                    Character owner = characterCanvasContainer._CharacterOwner;
                    Slider healthBar = characterCanvasContainer._CharacterHealthBar;
                    Image visualDamageImg = characterCanvasContainer._VisualDamageIMG;

                    healthBar.maxValue = owner._HealthSystem._DefenseData._MaximumHealth;
                    healthBar.value = owner._HealthSystem._DefenseData._CurrentHealth;
                    visualDamageImg.fillAmount = owner._HealthSystem._DefenseData._CurrentHealth / owner._HealthSystem._DefenseData._MaximumHealth;
                }
            }
            _SliderScreenUIPlayerHealthBar.maxValue = _GameController._Character._HealthSystem._DefenseData._MaximumHealth;
            _SliderScreenUIPlayerHealthBar.value = _GameController._Character._HealthSystem._DefenseData._CurrentHealth;
        }

        private void setCharacterStamina()
        {
            _SliderScreenUIPlayerStaminaBar.maxValue = _GameController._Character._HealthSystem._DefenseData._MaximumStamina;
            _SliderScreenUIPlayerStaminaBar.value = _GameController._Character._HealthSystem._DefenseData._CurrentStamina;
        }

        public void _UpdatePlayerHealthBar(Character character)
        {
            CharacterCanvasContainer characterCanvasContainer = getCharacterCanvas(character);

            //Character owner = characterCanvasContainer._CharacterOwner;
            Slider healthBar = characterCanvasContainer._CharacterHealthBar;
            Image visualDamageImg = characterCanvasContainer._VisualDamageIMG;

            float currentHealth = character._HealthSystem._DefenseData._CurrentHealth;
            healthBar.value = Mathf.Lerp(healthBar.value, currentHealth, 8f * Time.deltaTime);
            float targetFill = currentHealth / character._HealthSystem._DefenseData._MaximumHealth;
            visualDamageImg.fillAmount = Mathf.Lerp(visualDamageImg.fillAmount, targetFill, 2f * Time.deltaTime);

            //UI healthbar of the player
            _SliderScreenUIPlayerHealthBar.value = _GameController._Character._HealthSystem._DefenseData._CurrentHealth;
        }

        private CharacterCanvasContainer getCharacterCanvas(Character character)
        {
            foreach (CanvasContainerBase canvasBase in _CanvasContainerList)
            {
                if (canvasBase._CanvasContainerType != _ECanvasContainerType.CharacterCanvas)
                {
                    continue;
                }

                CharacterCanvasContainer characterCanvasContainer = (CharacterCanvasContainer)canvasBase;

                if (character == characterCanvasContainer._CharacterOwner)
                {
                    return characterCanvasContainer;
                }
            }
            return null;
        }

        public void _UpdatePlayerStaminaBar()
        {
            _SliderScreenUIPlayerStaminaBar.value = _GameController._Character._HealthSystem._DefenseData._CurrentStamina;
        }

        //IEnumerator iEHealthBarIMGTransition(CharacterCanvasContainer characterContainer, float timeDelay)
        //{
        //    Character owner = characterContainer._CharacterOwner;
        //    Image hpIMG = characterContainer._HealthBarIMG;
        //    yield return new WaitForSeconds(timeDelay);
        //    float targetFill = owner._HealthSystem._CurrentHealth / owner._HealthSystem._MaximumHealth;
        //    while (!Mathf.Approximately(hpIMG.fillAmount, targetFill))
        //    {
        //        hpIMG.fillAmount = Mathf.MoveTowards(hpIMG.fillAmount, targetFill, 5f * Time.deltaTime);
        //        yield return null;
        //    }
        //}
    }
}