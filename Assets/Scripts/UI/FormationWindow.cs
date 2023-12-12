using System;
using System.Collections.Generic;
using Formations;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class FormationWindow: MonoBehaviour
    {
        public static void ShowFormations(VisualElement root, List<string> names, bool isOffense)
        {
            var selectedFormationName = isOffense ? OffensiveFormations.DefaultName : DefensiveFormations.DefaultName;
            Action callbackAfterPopupIsClosed = () => FormationGenerator.LoadFormation(selectedFormationName, isOffense);
            
            // Create visual element for popup
            var popupElement = new VisualElement
            {
                style =
                {
                    position = new StyleEnum<Position>(Position.Absolute),
                    height = new StyleLength(new Length(100, LengthUnit.Percent)),
                    width = new StyleLength(new Length(100, LengthUnit.Percent))
                }
            };
                
            var popupContent = new Box
            {
                style =
                {
                    backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.9f),
                    width = new Length(100, LengthUnit.Percent),
                    borderTopLeftRadius = 20, borderTopRightRadius = 20,
                    borderBottomLeftRadius = 20, borderBottomRightRadius = 20,
                    alignItems = Align.Center,
                },
            };
            
            var popupLabel = new Label
            {
                text = $"Select the {(isOffense ? "offensive" : "defensive")} formation", 
                style =
                {
                    color = Color.white,
                    fontSize = 32,
                    width = new Length(100, LengthUnit.Percent),
                    unityTextAlign = TextAnchor.MiddleCenter,
                    paddingTop = 40,
                }
            };
            
            var listView = new ListView(names, 75)
            {
                style =
                {
                    width = new Length(100, LengthUnit.Percent),
                    paddingTop = 50, paddingBottom = 50,
                },
                makeItem = () => new Button
                {
                    style =
                    {
                        color = Color.white,
                        backgroundColor = new Color(0.2f, 0.2f, 0.2f),
                        borderBottomColor = new Color(0.2f, 0.2f, 0.2f), borderTopColor = new Color(0.2f, 0.2f, 0.2f),
                        borderRightColor = new Color(0.2f, 0.2f, 0.2f), borderLeftColor = new Color(0.2f, 0.2f, 0.2f),
                        width = new Length(100, LengthUnit.Percent),
                        fontSize = 32,
                    }
                },
                bindItem = (element, i) =>
                {
                    var button = (Button) element;
                    button.text = names[i];
                    button.RegisterCallback<MouseEnterEvent>(evt => OnButtonHoverEnter(button));
                    button.RegisterCallback<MouseLeaveEvent>(evt => OnButtonHoverLeave(button));
                    button.clicked += () =>
                    {
                        selectedFormationName = names[i];
                        PopupClose(root, popupElement, callbackAfterPopupIsClosed);
                    };
                }
            };
            
            var popupCloseButton = new Button
            {
                text = "Close",
                style =
                {
                    width = 300, height = 40,
                    fontSize = 24,
                    backgroundColor = new Color(0.2f, 0.2f, 0.2f),
                    color = Color.white,
                    borderTopLeftRadius = 6, borderTopRightRadius = 6,
                    borderBottomLeftRadius = 6, borderBottomRightRadius = 6,
                }
            };
            
            popupCloseButton.RegisterCallback<MouseEnterEvent>(evt => OnButtonHoverEnter(popupCloseButton));
            popupCloseButton.RegisterCallback<MouseLeaveEvent>(evt => OnButtonHoverLeave(popupCloseButton));
            
            popupContent.Add(popupLabel);
            popupContent.Add(listView);
            popupContent.Add(popupCloseButton);
            
            Open(root, popupElement, popupContent, popupCloseButton, callbackAfterPopupIsClosed);
        }
        
        private static void OnButtonHoverEnter(Button button)
        {
            button.style.backgroundColor = new Color(0.4f, 0.4f, 0.4f); // Lighter background on hover
        }

        private static void OnButtonHoverLeave(Button button)
        {
            button.style.backgroundColor = new Color(0.2f, 0.2f, 0.2f); // Revert to default background on hover exit
        }
        
        private static void Open(VisualElement rootElementForPopup, VisualElement popupElement, VisualElement popupContent, 
            Button popupCloseButton, 
            Action callbackAfterPopupIsClosed) 
        {
            //Popup background element which will close popup on click
            var background = new VisualElement
            {
                style =
                {
                    position = new StyleEnum<Position>(Position.Absolute),
                    height = new StyleLength(new Length(100, LengthUnit.Percent)),
                    width = new StyleLength(new Length(100, LengthUnit.Percent)),
                    opacity = new StyleFloat(0.4f),
                    backgroundColor = new StyleColor(Color.black)
                },
            };
            background.Focus();
            background.RegisterCallback<MouseDownEvent>(evt =>
            {
                evt.StopImmediatePropagation();
                PopupClose(rootElementForPopup, popupElement, callbackAfterPopupIsClosed);
            });
            popupElement.Add(background);
     
            if (popupCloseButton != null)
            {
                popupCloseButton.clickable.clicked += () =>
                {
                    PopupClose(rootElementForPopup, popupElement, callbackAfterPopupIsClosed);
                };
            }
     
            //Set content size
            popupContent.style.width = new StyleLength(new Length(72, LengthUnit.Percent));
            popupContent.style.height = new StyleLength(new Length(82, LengthUnit.Percent));
     
            //Show popupContent in the middle of the screen
            popupContent.style.position = new StyleEnum<Position>(Position.Absolute);
            popupContent.style.top = new StyleLength(new Length(10f, LengthUnit.Percent));
            popupContent.style.left = new StyleLength(new Length(24, LengthUnit.Percent));
            
            popupElement.Add(popupContent);
     
            rootElementForPopup.Add(popupElement);
        }
 
        private static void PopupClose(VisualElement popupRoot, VisualElement popup, Action callbackAfterPopupIsClosed) 
        { 
            popupRoot.Remove(popup);
            callbackAfterPopupIsClosed?.Invoke();
        }
    }
}