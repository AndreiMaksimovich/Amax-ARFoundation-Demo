// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;
using UnityEngine.UI;

namespace Amax.MobileARExample
{
    
    [RequireComponent(typeof(ARObjectsDemoController))]
    public class ARObjectsDemoUIController: MonoBehaviour
    {
        
        [Header("Debug")] 
        [SerializeField] private Toggle showPlanesToggle;
        [SerializeField] private Button buttonReset;

        [Header("State UI Roots")] 
        [SerializeField] private RectTransform viewingStateUIRoot;
        [SerializeField] private RectTransform editingStateUIRoot, addingStateUIRoot;
        
        private ARObjectsDemoController ARObjectsDemoController { get; set; }

        private void Awake()
        {
            ARObjectsDemoController = GetComponent<ARObjectsDemoController>();
            ARObjectsDemoController.OnStateChanged += ARObjectsDemoOnOnStateChanged;
            
            // Reset
            buttonReset.onClick.AddListener(() =>
            {
                EventBus.RaiseEvent(new OnARSessionReset());
                ARObjectsDemoController.Reset();
            });
            
            // Tracking Plane Visibility
            showPlanesToggle.isOn = ARObjectsDemoController.ShowPlanes;
            showPlanesToggle.onValueChanged.AddListener(value => ARObjectsDemoController.ShowPlanes = value);
        }

        private void ARObjectsDemoOnOnStateChanged()
        {
            var state = ARObjectsDemoController.State;
            viewingStateUIRoot.gameObject.SetActive(state == ARObjectsDemoState.Viewing);
            editingStateUIRoot.gameObject.SetActive(state == ARObjectsDemoState.Editing);
            addingStateUIRoot.gameObject.SetActive(state == ARObjectsDemoState.Adding);
        }
        
    }
    
}