using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace AWSSDK.Examples.ChessGame
{
    // Attached to a panel, shows a list of Friends and creates a game with that friend when
    // an element is clicked.
    public class FriendListPopulator : MonoBehaviour
    {
        public StyleApplier StyleApplier;
        public Button FriendChoiceButtonPrefab;
        public Text LoadingTextPrefab;
        private GameManager.FriendsAvailableHandler FriendsAvailableHandler;

        void OnEnable()
        {
            LoadItems();
        }

        void OnDisable()
        {
            DetachListener();
        }

        public void DetachListener()
        {
            if (FriendsAvailableHandler != null)
            {
                GameManager.Instance.UnregisterOnFriendsAvailableHandler(FriendsAvailableHandler);
            }
        }

        public void LoadItems()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Text loadingText = Instantiate(LoadingTextPrefab) as Text;
            StyleApplier.ApplyStyleToTitleText(loadingText);
            loadingText.transform.SetParent(transform, false);

            FriendsAvailableHandler = delegate(List<GameState.PlayerInfo> friends)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                foreach (var friend in friends)
                {
                    // Create instance of button prefab
                    var button = Instantiate(FriendChoiceButtonPrefab) as Button;
                    // The container allows us to reference and manipulate the button's text field
                    var contentContainer = button.GetComponent<FriendChoiceButtonContentContainer>();
                    contentContainer.Text.text = friend.Name;
                    StyleApplier.ApplyStyleToLabelText(contentContainer.Text);
                    StyleApplier.ApplyStyleToButton(button);
                    // Unchanging reference so that the onClick listener is referring to the correct friend
                    var consistentRefFriend = friend;
                    button.onClick.AddListener(delegate
                    {
                        GameManager.Instance.LoadNewMatch(consistentRefFriend);
                    });
                    button.transform.SetParent(gameObject.transform, false);
                }
            };
            GameManager.Instance.RegisterOnFriendsAvailableHandler(FriendsAvailableHandler);
        }
    }
}