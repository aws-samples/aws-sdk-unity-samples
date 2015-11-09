using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AWSSDK.Examples.ChessGame
{
    // Attached to a panel, shows a list of matches and creates a game with that friend when
    // an element is clicked.
    public class MatchesListPopulator : MonoBehaviour
    {
        public Button MatchesChoiceButtonPrefab;
        public Text LoadingTextPrefab;
        public StyleApplier StyleApplier;
        private GameManager.StatesAvailableHandler MatchStatesAvailableHandler;

        public void DetachListener()
        {
            if (MatchStatesAvailableHandler != null)
            {
                GameManager.Instance.UnregisterOnStatesAvailableHandler(MatchStatesAvailableHandler);
            }
        }

        public void LoadItems()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            var loadingText = Instantiate(LoadingTextPrefab) as Text;
            StyleApplier.ApplyStyleToTitleText(loadingText);
            loadingText.transform.SetParent(transform, false);

            MatchStatesAvailableHandler = delegate(List<GameState.MatchState> matchStates)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                foreach (var matchState in matchStates)
                {
                    // Create instance of button prefab
                    var button = Instantiate(MatchesChoiceButtonPrefab) as Button;
                    // The container allows us to reference and manipulate the button's text fields
                    var contentContainer = button.GetComponent<MatchChoiceButtonContentContainer>();
                    contentContainer.Self.text = "You";
                    contentContainer.Opponent.text = matchState.Opponent.Name;
                    //Bold the text of the player whose turn it is
                    StyleApplier.ApplyStyleToLabelText(contentContainer.Self);
                    StyleApplier.ApplyStyleToLabelText(contentContainer.Opponent);
                    StyleApplier.ApplyStyleToLabelText(contentContainer.Vs);
                    StyleApplier.ApplyStyleToButton(button);
                    Text currentTurnText = matchState.IsSelfTurn() ? contentContainer.Self : contentContainer.Opponent;
                    currentTurnText.color = Color.blue;
                    button.transform.SetParent(transform, false);
                    // Unchanging reference so that the onClick listener is referring to the correct matchState
                    var consistentRefMatchState = matchState;
                    button.onClick.AddListener(delegate
                    {
                        GameManager.Instance.LoadMatch(consistentRefMatchState);
                    });
                }
            };
            GameManager.Instance.RegisterOnStatesAvailableHandler(MatchStatesAvailableHandler);
        }

        void OnEnable()
        {
            LoadItems();
        }

        void OnDisable()
        {
            DetachListener();
        }
    }
}