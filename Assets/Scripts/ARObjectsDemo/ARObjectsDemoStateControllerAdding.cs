using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Amax.MobileARExample
{

    public class ARObjectsDemoStateControllerAdding : AARObjectsDemoStateController
    {
        
        [field: SerializeField] private ARObjectsDemoObjectSelectionPanel ARObjectsDemoSelectionPanel { get; set; }
        [field: SerializeField] private Button cancelButton;
        [field: SerializeField] private Button addButton;
        
        private IARObjectInstance PreviewObject { get; set; }
        private ARPlane Plane { get; set; }
        private ARObject SelectedObject => ARObjectsDemoSelectionPanel.SelectedObject;

        protected override void OnAwake()
        {
            base.OnAwake();
            ARObjectsDemoSelectionPanel.OnSelectedObjectChanged += OnSelectedObjectChanged;
            cancelButton.onClick.AddListener(() => DemoController.SwitchState(ARObjectsDemoState.Viewing));
            addButton.onClick.AddListener(SpawnObject);
        }
        
        private void SpawnObject()
        {
            if (PreviewObject == null) return;
            PreviewObject.Visualizer.DisplayMode = ARObjectDisplayMode.Normal;
            SpawnPreviewObject();
        }

        private void OnSelectedObjectChanged(ARObject obj)
        {
            if (!Enabled) return;
            if (PreviewObject != null) DestroyPreviewObject();
            DemoController.PlaneManager.requestedDetectionMode = obj.Type == ARObjectType.Horizontal
                ? PlaneDetectionMode.Horizontal
                : PlaneDetectionMode.Vertical;
            SpawnPreviewObject();
        }

        public override void Activate()
        {
            base.Activate();
            if (ARObjectsDemoSelectionPanel.SelectedObject != null)
            {
                OnSelectedObjectChanged(ARObjectsDemoSelectionPanel.SelectedObject);
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
            if (PreviewObject != null)
            {
                DestroyPreviewObject();
            }
            DemoController.PlaneManager.requestedDetectionMode = PlaneDetectionMode.Horizontal | PlaneDetectionMode.Vertical;
        }

        private void SpawnPreviewObject()
        {
            if (ARObjectsDemoSelectionPanel.SelectedObject == null) return;
            PreviewObject = ObjectManager.SpawnARObject(SelectedObject, Vector3.zero, Quaternion.identity,
                ARObjectDisplayMode.Preview);
            UpdatePreviewObject();
        }

        private void DestroyPreviewObject()
        {
            ObjectManager.DestroyInstance(PreviewObject);
            PreviewObject = null;
        }

        protected override void StateFixedUpdate()
        {
            base.StateFixedUpdate();
            UpdatePreviewObject();
        }

        private void UpdatePreviewObject()
        {
            if (PreviewObject == null) return;
            DemoController.PointAndShootUtils.UpdatePositionAndRotation(PreviewObject);
        }
        
    }

}
