using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.PlayerUnits;

namespace Assets.Scripts.GameLogic.Utilities
{
    internal class PointerSelectableChecker
    {
        private LayerMask _ui = 5;
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _results;

        public PointerSelectableChecker()
        {
            _pointerEventData = new PointerEventData(EventSystem.current);
            _results = new List<RaycastResult>();
        }

        public bool IsPointerOverSelectableObject()
        {
            _results.Clear();
            _pointerEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            EventSystem.current.RaycastAll(_pointerEventData, _results);

            foreach (RaycastResult result in _results)
            {
                if (result.gameObject.layer == _ui || result.gameObject.GetComponent<Selectable>() != null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsPointerOverSelectableObject(Vector3 touch)
        {
            _results.Clear();
            _pointerEventData.position = new Vector2(touch.x, touch.y);
            EventSystem.current.RaycastAll(_pointerEventData, _results);

            foreach (RaycastResult result in _results)
            {
                if (result.gameObject.layer == _ui || result.gameObject.GetComponent<Selectable>() != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
