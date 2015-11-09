using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AWSSDK.Examples.ChessGame
{
    // Some of the functionality of the New Game Menu.
    public class NewGameMenuBehaviors : MonoBehaviour
    {
        public FriendListPopulator FriendPopulator;
        public Button BackButton;
        public Button RefreshButton;

        void Start()
        {
            BackButton.onClick.AddListener(BackButtonBehavior);
            RefreshButton.onClick.AddListener(RefreshButtonBehavior);
        }

        public void BackButtonBehavior()
        {
            Application.LoadLevel("MainMenu");
        }

        public void RefreshButtonBehavior()
        {
            FriendPopulator.DetachListener();
            GameManager.Instance.DereferenceGameState();
            FriendPopulator.LoadItems();
            GameManager.Instance.Load();
        }
    }
}