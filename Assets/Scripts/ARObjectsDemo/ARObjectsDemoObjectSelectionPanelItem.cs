using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Amax.MobileARExample
{

    public class ARObjectsDemoObjectSelectionPanelItem : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] private Image iconImage;
        [SerializeField] private Image selectionBackgroundImage;

        private ARObject arObject;
        public ARObject ARObject
        {
            get => arObject;
            set
            {
                arObject = value;
                iconImage.sprite = arObject.Icon;
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                selectionBackgroundImage.gameObject.SetActive(_isSelected);
            }
        }

        public event Action<ARObjectsDemoObjectSelectionPanelItem> OnClick; 

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }
        
    }

}
