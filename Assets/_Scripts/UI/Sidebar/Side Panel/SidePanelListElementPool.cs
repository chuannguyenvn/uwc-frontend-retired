using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Sidebar.SidePanel
{
    public class SidePanelListElementPool : Singleton<SidePanelListElementPool>
    {
        [SerializeField] private SidePanelListElement _SidePanelListElementPrefab;
        private List<SidePanelListElement> _pool = new();

        private void Start()
        {
            for (int i = 0; i < 30; i++)
            {
                var listElement = Instantiate(_SidePanelListElementPrefab, transform, false);
                _pool.Add(listElement);
                listElement.Hide();
            }
        }

        public SidePanelListElement GetElement(Transform transform)
        {
            for (var i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].gameObject.activeSelf) continue;
                _pool[i].gameObject.transform.SetParent(transform);
                return _pool[i];
            }

            var newElement = Instantiate(_SidePanelListElementPrefab, transform, false);
            _pool.Add(newElement);
            return newElement;
        }

        public void ReturnElement(SidePanelListElement element)
        {
            element.gameObject.transform.SetParent(transform);
            element.Hide();
        }
        
        public void ReturnAllElements()
        {
            foreach (var element in _pool)
            {
                element.gameObject.transform.SetParent(transform);
                element.Hide();
            }
        }
    }
}