using Amax.UI.Dialogs;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

namespace Amax.MobileARExample
{

    public class ARObjectsDemoStateControllerEditing : AARObjectsDemoStateController
    {

        [field: SerializeField] private Button cancelButton;

        [field: Header("Edit buttons")] 
        [field: SerializeField] private Button moveButton;
        [field: SerializeField] private ButtonTouchEventListener moveButtonListener; 
        [field: SerializeField] private Button rotateButton;
        [field: SerializeField] private ButtonTouchEventListener rotateButtonListener;
        [field: SerializeField] private Button deleteButton;
        
        private EditingMode Mode { get; set; } = EditingMode.None;

        
        private IARObjectInstance selectedObject;
        private IARObjectInstance SelectedObject
        {
            get => selectedObject;
            set
            {
                if (selectedObject == value) return;
                if (selectedObject != null) selectedObject.Visualizer.DisplayMode = ARObjectDisplayMode.Normal;
                selectedObject = value;
                if (selectedObject != null)
                {
                    selectedObject.Visualizer.DisplayMode = ARObjectDisplayMode.Edit;
                    DemoController.PlaneManager.requestedDetectionMode =
                        selectedObject.ARObject.Type == ARObjectType.Vertical
                            ? PlaneDetectionMode.Vertical
                            : PlaneDetectionMode.Horizontal;
                }
                UpdateButtons();
            }
        }

        private void UpdateButtons()
        {
            moveButton.interactable = SelectedObject != null && SelectedObject.Configuration.MovementAllowed;
            rotateButton.interactable = SelectedObject != null && SelectedObject.Configuration.RotationAllowed;
            deleteButton.interactable = SelectedObject != null;
        }
        
        protected override void OnAwake()
        {
            base.OnAwake();
            
            // Cancel button
            cancelButton.onClick.AddListener(() => DemoController.SwitchState(ARObjectsDemoState.Viewing));
            
            // Move button
            moveButtonListener.OnTouchDown += MoveButtonOnOnTouchDown;
            moveButtonListener.OnTouchUp += MoveButtonOnOnTouchUp;
            
            // Rotate button
            rotateButtonListener.OnTouchDown += RotateButtonOnOnTouchDown;
            rotateButtonListener.OnTouchUp += RotateButtonOnOnTouchUp;
            
            // Delete button
            deleteButton.onClick.AddListener(OnDeleteButtonClick);
        }

        private Vector3 rotationCameraStartPosition;
        private Vector3 rotationCameraStartForwardVector;
        private float rotationARObjectStartRotation;
        
        private Camera Camera => Camera.main;
        
        private void RotateButtonOnOnTouchDown(ButtonTouchEventListener obj)
        {
            if (Mode != EditingMode.None || SelectedObject==null || SelectedObject.Configuration.RotationAllowed==false) return;
            Mode = EditingMode.Rotation;
            var cameraTransform = Camera.transform;
            rotationCameraStartPosition = cameraTransform.position;
            rotationCameraStartForwardVector = cameraTransform.forward;
            rotationARObjectStartRotation = SelectedObject.Positioner.Rotation;
        }
        
        private void MoveButtonOnOnTouchDown(ButtonTouchEventListener obj)
        {
            if (Mode != EditingMode.None || SelectedObject==null || SelectedObject.Configuration.MovementAllowed==false) return;
            Mode = EditingMode.Movement;
        }

        private void MoveButtonOnOnTouchUp(ButtonTouchEventListener obj)
        {
            Mode = EditingMode.None;
        }
        
        private void RotateButtonOnOnTouchUp(ButtonTouchEventListener obj)
        {
            Mode = EditingMode.None;
        }

        
        
        private void OnDeleteButtonClick()
        {
            if (SelectedObject != null && Mode == EditingMode.None)
            {
                Mode = EditingMode.Deletion;
                Dialogs.ShowConfirmationDialog
                (
                    "Delete?", 
                    "Delete?",
                    result =>
                    {
                        if (result)
                        {
                            var obj = SelectedObject;
                            SelectedObject = null;
                            ObjectManager.DestroyInstance(obj);
                        }
                        Reset();
                    }
                );
            }
        }

        public override void Deactivate()
        {
            base.Deactivate();
            Reset();
        }

        public override void Activate()
        {
            base.Activate();
            UpdateButtons();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Reset();
        }

        private void Reset()
        {
            DemoController.PlaneManager.requestedDetectionMode =
                PlaneDetectionMode.Horizontal | PlaneDetectionMode.Vertical;
            SelectedObject = null;
            Mode = EditingMode.None;
        }

        protected override void StateLateUpdate()
        {
            base.StateLateUpdate();
            
            switch (Mode)
            {
                // None
                case EditingMode.None:
                    SelectedObject = ARObjectRaycastUtils.GetARObjectCameraLookingAt();
                    break;
                // Move
                case EditingMode.Movement:
                    Move();
                    break;
                // Rotate
                case EditingMode.Rotation:
                    Rotate();
                    break;
            }
        }

        private void Move()
        {
            DemoController.PointAndShootUtils.UpdatePositionAndRotation(SelectedObject);
        }

        private void Rotate()
        {
            var initialDistance =
                Vector3.Distance(SelectedObject.Positioner.DesiredPosition, rotationCameraStartPosition);
            var currentDistance =
                Vector3.Distance(Camera.transform.position, SelectedObject.Positioner.DesiredPosition);
            var rotation = rotationARObjectStartRotation + (initialDistance - currentDistance) * 400;
            SelectedObject.Positioner.Rotation = rotation;
            SelectedObject.Positioner.UpdatePositionAndRotation();
        }
        
        public enum EditingMode
        {
            None, Movement, Rotation, Deletion
        }
        
    }

}
