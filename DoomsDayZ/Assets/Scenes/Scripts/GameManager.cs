using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieGameGameControllerSystem;
using IndieGameUISystem;

namespace IndieGameManagersSystem
{
    public class GameManager : Singleton<GameManager>
    {
        private ItemHotBarContainer itemHotBarContainer;
        private PlayerVault playerVault;
        private GameController gameController;
        [SerializeField] private GameUI gameUI;

        public GameController _GetGameController()
        {
            if (null == gameController)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("GameController"));
                gameController = obj.GetComponent<GameController>();
            }
            return gameController;
        }

        public ItemHotBarContainer _GetItemHotbarContainer()
        {
            if (null == itemHotBarContainer)
            {
                itemHotBarContainer = FindObjectOfType<ItemHotBarContainer>();
            }
            return itemHotBarContainer;
        }

        public PlayerVault _GetPlayerVault()
        {
            if (null == playerVault)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("PlayerVault"));
                playerVault = obj.GetComponent<PlayerVault>();
            }
            return playerVault;
        }

        public GameUI _GetGameUI()
        {
            if (null == gameUI)
            {
                gameUI = FindAnyObjectByType<GameUI>();
            }
            return gameUI;
        }
    }
}
