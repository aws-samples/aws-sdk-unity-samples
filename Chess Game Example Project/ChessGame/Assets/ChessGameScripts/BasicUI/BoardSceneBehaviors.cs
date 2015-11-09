using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AWSSDK.Examples.ChessGame
{
    // Some of the functionality of the Board Scene.
    public class BoardSceneBehaviors : MonoBehaviour
    {
        public Button BackButton;
        public Button RefreshButton;
        public BoardUi BoardUi;

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
            if (BoardUi.CurrentMatchState != null && !BoardUi.CurrentMatchState.Opponent.IsLocalOpponent())
            {
                GameManager.Instance.LoadMatch(BoardUi.CurrentMatchState);
            }
        }
    }
}
