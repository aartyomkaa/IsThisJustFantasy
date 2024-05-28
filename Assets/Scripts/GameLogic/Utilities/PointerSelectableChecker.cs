using Assets.Scripts.PlayerUnits;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
                Debug.Log(result);

                if (result.gameObject.layer == _ui || result.gameObject.GetComponent<Selectable>() != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
