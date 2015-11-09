using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AWSSDK.Examples.ChessGame
{
    // Some of the functionality of the Main Menu.
    public class MainMenuBehaviors : MonoBehaviour
    {
        public MatchesListPopulator GamePopulator;
        public Button NewGameButton;
        public Button SignInButton;
        public Button RefreshButton;
        public Button SettingsButton;

        void Start()
        {
            NewGameButton.onClick.AddListener(NewGameButtonBehavior);
            SignInButton.onClick.AddListener(SignInButtonBehavior);
            RefreshButton.onClick.AddListener(RefreshButtonBehavior);
            SettingsButton.onClick.AddListener(SettingsButtonBehavior);
            GameManager.Instance.Load();
        }

        public void NewGameButtonBehavior()
        {
            Application.LoadLevel("NewGameMenu");
        }

        public void SignInButtonBehavior()
        {
            GameManager.Instance.LogInToFacebook();
        }

        public void RefreshButtonBehavior()
        {
            GamePopulator.DetachListener();
            GameManager.Instance.DereferenceGameState();
            GamePopulator.LoadItems();
            GameManager.Instance.Load();
        }

        public void SettingsButtonBehavior()
        {
            Application.LoadLevel("SettingsMenu");
        }
    }
}
