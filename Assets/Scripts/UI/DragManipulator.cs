using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class DragManipulator : PointerManipulator
    {
        
        private bool _debug = false;
        
        // Write a constructor to set target and store a reference to the
        // root of the visual tree.
        public DragManipulator(VisualElement target, Box zone)
        {
            this.target = target;
            root = target.parent;
            Zone = zone;
            
            if (_debug) Debug.Log($"[DragManipulator] enabled for target {target} and zone {zone}");
        }

        protected override void RegisterCallbacksOnTarget()
        {
            // Register the four callbacks on target.
            if (_debug) Debug.Log("RegisterCallbacksOnTarget");
            target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
            target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
            target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
            target.RegisterCallback<GeometryChangedEvent>(evt =>
            {
                ZoneMin = Zone.worldBound.position - target.worldBound.position;
                ZoneMax = Zone.worldBound.size - target.worldBound.size + ZoneMin;
                if (_debug) Debug.Log($"[DragManipulator] set ZoneMin {ZoneMin} ZoneMax {ZoneMax}");
            });
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            // Un-register the four callbacks from target.
            target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
            target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
            target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
            target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
        }

        private Vector2 targetStartPosition { get; set; }
        private Vector3 pointerStartPosition { get; set; }

        private bool enabled { get; set; }

        private VisualElement root { get; }
        private Box Zone { get; }
        
        private Vector2 ZoneMin { get; set; }
        private Vector2 ZoneMax { get; set; }

        // This method stores the starting position of target and the pointer,
        // makes target capture the pointer, and denotes that a drag is now in progress.
        private void PointerDownHandler(PointerDownEvent evt)
        {
            targetStartPosition = target.transform.position;
            pointerStartPosition = evt.position;
            target.CapturePointer(evt.pointerId);
            enabled = true;
            if (_debug) Debug.Log($"[PointerDownHandler] enabled: set targetStartPosition to {targetStartPosition}");
        }

        // This method checks whether a drag is in progress and whether target has captured the pointer.
        // If both are true, calculates a new position for target within the bounds of the window.
        private void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (enabled && target.HasPointerCapture(evt.pointerId))
            {
                Vector3 pointerDelta = evt.position - pointerStartPosition;
                
                target.transform.position = new Vector2(
                    Mathf.Clamp(targetStartPosition.x + pointerDelta.x, ZoneMin.x, ZoneMax.x),
                    Mathf.Clamp(targetStartPosition.y + pointerDelta.y, ZoneMin.y, ZoneMax.y)
                );

                if (_debug) Debug.Log("[PointerMoveHandler] enabled: set target.transform.position to " + target.transform.position);
            }
        }

        // This method checks whether a drag is in progress and whether target has captured the pointer.
        // If both are true, makes target release the pointer.
        private void PointerUpHandler(PointerUpEvent evt)
        {
            if (enabled && target.HasPointerCapture(evt.pointerId))
            {
                target.ReleasePointer(evt.pointerId);
            }
        }

        // This method checks whether a drag is in progress. If true, queries the root
        // of the visual tree to find all slots, decides which slot is the closest one
        // that overlaps target, and sets the position of target so that it rests on top
        // of that slot. Sets the position of target back to its original position
        // if there is no overlapping slot.
        private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
        {
            if (enabled)
            {
                if (_debug) Debug.Log("PointerCaptureOutHandler enabled");
            }
        }

        private bool OverlapsTarget(VisualElement slot)
        {
            return target.worldBound.Overlaps(slot.worldBound);
        }

        private VisualElement FindClosestSlot(UQueryBuilder<VisualElement> slots)
        {
            List<VisualElement> slotsList = slots.ToList();
            float bestDistanceSq = float.MaxValue;
            VisualElement closest = null;
            foreach (VisualElement slot in slotsList)
            {
                Vector3 displacement =
                    RootSpaceOfSlot(slot) - target.transform.position;
                float distanceSq = displacement.sqrMagnitude;
                if (distanceSq < bestDistanceSq)
                {
                    bestDistanceSq = distanceSq;
                    closest = slot;
                }
            }
            return closest;
        }

        private Vector3 RootSpaceOfSlot(VisualElement slot)
        {
            Vector2 slotWorldSpace = slot.parent.LocalToWorld(slot.layout.position);
            return root.WorldToLocal(slotWorldSpace);
        }
    }
}