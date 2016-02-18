using UnityEngine;
using UnityEngine.UI;

namespace AWSSDK.Examples.ChessGame
{
    // Some of the functionality of the Settings Menu.
    class SettingsMenuBehaviors : MonoBehaviour
    {
        public InputField YourIdInputField;
        public InputField YourNameInputField;
        public InputField FriendIdInputField;
        public Button SaveNameButton;
        public Button AddFriendButton;
        public Button BackButton;
        public Text StatusText;

        private GameManager.SelfAvailableHandler SelfAvailableHandler;
        private GameManager.FriendAddedHandler FriendAddedHandler;
        private GameState.PlayerInfo Self;

        void Start()
        {
            SaveNameButton.onClick.AddListener(SaveNameButtonBehavior);
            AddFriendButton.onClick.AddListener(AddFriendButtonBehavior);
            BackButton.onClick.AddListener(BackButtonBehavior);
            RevertIdText();
            RevertNameText(true);
            // Ignore/revert input to field, but allow user to highlight/copy
            YourIdInputField.onValueChange.AddListener((s) =>
            {
                RevertIdText();
            });
            // Ignore/revert input to field, but allow user to highlight/copy
            YourNameInputField.onValueChange.AddListener((s) =>
            {
                RevertNameText(false);
            });
        }

        void OnEnable()
        {
            AttachListeners();
        }

        void OnDisable()
        {
            DetachListeners();
        }

        public void DetachListeners()
        {
            if (SelfAvailableHandler != null)
            {
                GameManager.Instance.UnregisterOnSelfAvailableHandler(SelfAvailableHandler);
            }
            if (FriendAddedHandler != null)
            {
                GameManager.Instance.UnregisterOnFriendAddedHandler(FriendAddedHandler);
            }
        }

        public void AttachListeners()
        {
            SelfAvailableHandler = delegate(GameState.PlayerInfo self)
            {
                Self = self;
                if (self == null)
                {
                    YourIdInputField.text = "No Id";
                    YourNameInputField.text = "No Name";
                }
                else
                {
                    YourIdInputField.text = self.Id;
                    YourNameInputField.text = self.Name;
                }
            };
            GameManager.Instance.RegisterOnSelfAvailableHandler(SelfAvailableHandler);

            FriendAddedHandler = delegate(string requestedId, GameState.PlayerInfo player)
            {
                if (player == null)
                {
                    StatusText.text += string.Format("Could not find a user with ID {0}.\n", requestedId);
                }
                else
                {
                    StatusText.text += string.Format("Added Friend \"{0}\" who has ID {1}.\n", player.Name, player.Id);
                }
            };
            GameManager.Instance.RegisterFriendAddedHandler(FriendAddedHandler);
        }

        private void RevertIdText()
        {
            // Do not allow id to be edited, i.e. revert it back.
            if (Self == null || Self.Id == null)
            {
                YourIdInputField.text = "No Id Yet";
            }
            else
            {
                YourIdInputField.text = Self.Id;
            }
        }

        private void RevertNameText(bool forceRevert)
        {
            // Do not allow id to be edited, but only if it is loading.
            if (Self == null || Self.Name == null)
            {
                YourNameInputField.text = "Name is loading";
            }
            else if (forceRevert)
            {
                YourNameInputField.text = Self.Name;
            }
        }

        public void BackButtonBehavior()
        {
            Application.LoadLevel("MainMenu");
        }

        public void SaveNameButtonBehavior()
        {
            // self being null means the gamestate hasn't yet loaded, so do not allow the user to make changes.
            if (Self != null)
            {
                GameManager.Instance.UpdateName(YourNameInputField.text);
                StatusText.text = "Name saved.\n";
            }
        }

        public void AddFriendButtonBehavior()
        {
            string id = FriendIdInputField.text.Trim();
            StatusText.text = string.Format("Searching for friend with ID {0}...\n", id);
            GameManager.Instance.AddFriend(id);
        }
    }
}