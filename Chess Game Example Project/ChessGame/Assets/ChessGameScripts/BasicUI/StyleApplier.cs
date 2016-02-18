using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace AWSSDK.Examples.ChessGame
{
    // Applies the attached style to the attached game objects.
    public class StyleApplier : MonoBehaviour
    {
        public Style Style;
        public Camera Cam;
        public List<Button> Buttons;
        public List<Image> BackgroundImages;
        public List<Text> LabelTexts;
        public List<Text> TitleTexts;


        [ExecuteInEditMode]
        public void Start()
        {
            foreach (var b in Buttons)
            {
                ApplyStyleToButton(b);
            }
            foreach (var i in BackgroundImages)
            {
                ApplyStyleToBackgroundImage(i);
            }
            foreach (var t in LabelTexts)
            {
                ApplyStyleToLabelText(t);
            }
            foreach (var t in TitleTexts)
            {
                ApplyStyleToTitleText(t);
            }
            ApplyStyleToCamera(Cam);
        }

        public void ApplyStyleToButton(Button button)
        {
            var colorBlock = new ColorBlock();
            colorBlock.highlightedColor = Style.ButtonHighlightedColor;
            colorBlock.pressedColor = Style.ButtonPressedColor;
            colorBlock.disabledColor = Style.ButtonDisabledColor;
            colorBlock.normalColor = Style.ButtonNormalColor;
            colorBlock.colorMultiplier = Style.ButtonColorMultiplier;
            colorBlock.fadeDuration = 0.1f;
            var buttonImage = button.gameObject.GetComponent<Image>();
            buttonImage.color = Style.ButtonImage.color;
            buttonImage.sprite = Style.ButtonImage.sprite;
            buttonImage.material = Style.ButtonImage.material;
            button.colors = colorBlock;
        }

        public void ApplyStyleToCamera(Camera camera)
        {
            camera.backgroundColor = Style.BackgroundColor;
        }

        public void ApplyStyleToBackgroundImage(Image image)
        {
            image.color = Style.BackgroundColor;
        }

        public void ApplyStyleToTitleText(Text text)
        {
            text.color = Style.TitleTextColor;
        }

        public void ApplyStyleToLabelText(Text text)
        {
            text.color = Style.LabelTextColor;
        }
    }
}