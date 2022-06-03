using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Amax.MobileARExample
{

    public class ARObjectsDemoObjectSelectionPanel : MonoBehaviour, IARObjectSelector
    {

        [SerializeField] private RectTransform itemRoot;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private ARObjectManager arObjectManager;

        private List<ARObjectsDemoObjectSelectionPanelItem> items = new List<ARObjectsDemoObjectSelectionPanelItem>();
        public ARObject SelectedObject { get; private set; }
        public event Action<ARObject> OnSelectedObjectChanged;

        public void Select(ARObject arObject)
        {
            SelectedObject = arObject;
            foreach (var item in items)
            {
                item.IsSelected = item.ARObject == arObject;
            }
            OnSelectedObjectChanged?.Invoke(SelectedObject);
        }
        
        private void Awake()
        {
            foreach (var arObj in arObjectManager.ARObjects.Objects)
            {
                var itemGO = Instantiate(itemPrefab, itemRoot);
                var item = itemGO.GetComponent<ARObjectsDemoObjectSelectionPanelItem>();
                item.ARObject = arObj;
                item.OnClick += OnItemClick;
                items.Add(item);
            }
            Select(items[0].ARObject);
        }

        private void OnItemClick(ARObjectsDemoObjectSelectionPanelItem item)
        {
            Select(item.ARObject);
        }
    }

}
