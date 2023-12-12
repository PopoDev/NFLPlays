using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PlayerMarker: VisualElement
    {
        private bool _debug = false;
        
        public LinkedList<PlayerMove> Moves { get; } = new();
        
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlStringAttributeDescription m_PositionAttribute = new() { name = "position" };
            UxmlBoolAttributeDescription m_IsOffenseAttribute = new() { name = "isOffense" };
            UxmlIntAttributeDescription m_XAttribute = new() { name = "x" };
            UxmlIntAttributeDescription m_YAttribute = new() { name = "y" };
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                (ve as PlayerMarker).position = m_PositionAttribute.GetValueFromBag(bag, cc);
                (ve as PlayerMarker).isOffense = m_IsOffenseAttribute.GetValueFromBag(bag, cc);
                (ve as PlayerMarker).x = m_XAttribute.GetValueFromBag(bag, cc);
                (ve as PlayerMarker).y = m_YAttribute.GetValueFromBag(bag, cc);
            }
        }
        
        public new class UxmlFactory : UxmlFactory<PlayerMarker, UxmlTraits> { }
        
        public Button m_Button;    // Button to select the player
        Label m_Label;      // Label displaying the player's position
        string m_Position;  // Player's position
        bool m_IsOffense;   // Whether the player is on offense or defense
        int m_X;          // X coordinate of the player
        int m_Y;          // Y coordinate of the player
        public string position
        {
            get => m_Position;
            set
            {
                m_Position = value;
                m_Label.text = m_Position;
            }
        }
        
        public bool isOffense
        {
            get => m_IsOffense;
            set
            {
                m_IsOffense = value;
                m_Button.style.backgroundColor = m_IsOffense ? Color.blue : Color.red;
                name = "player-marker-" + (m_IsOffense ? "offense" : "defense");
            }
        }
        
        public int x
        {
            get => m_X;
            set { m_X = value; style.left = m_X; }
        }

        public int y
        {
            get => m_Y; 
            set { m_Y = value; style.top = m_Y; }
        }

        public PlayerMarker()
        {
            m_Button = new Button() {
                style =
                {
                    width = 50, height = 50, position = Position.Absolute,
                    borderTopLeftRadius = 100, borderTopRightRadius = 100, borderBottomLeftRadius = 100, borderBottomRightRadius = 100,
                },
            };
            
            m_Label = new Label()
            {
                style =
                {
                    color = Color.white, 
                    fontSize = 20, unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold)
                }
            };
            
            m_Button.Add(m_Label);
            /*
            m_Button.RegisterCallback<MouseOverEvent>(evt =>
            {
                if (_debug) Debug.Log($"Mouse hover {m_Position}");
                m_Button.style.scale = new StyleScale(new Vector2(1.2f, 1.2f));
            });
             */
            Add(m_Button);
        }

        public Vector2 GetPositionCenter()
        {
            Vector2 transformPosition = m_Button.transform.position;
            if (_debug) Debug.Log($"transformPosition {transformPosition}, worldBound.position {worldBound.position}, m_Button.worldBound.size {m_Button.worldBound.size}");
            return transformPosition + worldBound.position + m_Button.worldBound.size / 2;
        }

        public void EnableDrag(Box zone)
        {
            DragManipulator dragManipulator = new(m_Button, zone);
        }
        
        public void AddMove(PlayerMove move)
        {
            Moves.AddLast(move);
        }
    }
}