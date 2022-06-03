using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Amax.MobileARExample
{

    public class ARObjectsDemoController : MonoBehaviour, IEventBusListener<OnARSessionReset>
    {

        private ARObjectsDemoState state = ARObjectsDemoState.Viewing;
        public ARObjectsDemoState State
        {
            get => state;
            private set
            {
                state = value;
                OnStatePropertyChanged();
            }
        }
        
        [SerializeField] private bool showPlanes = false;
        public bool ShowPlanes
        {
            get => showPlanes;
            set
            {
                showPlanes = value;
                UpdatePlanesVisualization();
            }
        }
        
        [field: SerializeField] public ARSession ARSession { get; set; }
        [field: SerializeField] public ARPlaneManager PlaneManager { get; set; }
        [field: SerializeField] public ARRaycastManager RaycastManager { get; set; }
        [field: SerializeField] public ARObjectManager ARObjectManager { get; set; }
        [field: SerializeField] public ARObjectsDemoPointAndShootUtils PointAndShootUtils { get; set; }

        [field: Header("State Controllers")] 
        [field: SerializeField] private AARObjectsDemoStateController StateControllerViewing { get; set; }
        [field: SerializeField] private AARObjectsDemoStateController StateControllerEditing{ get; set; }
        [field: SerializeField] private AARObjectsDemoStateController StateControllerAdding { get; set; }
        private AARObjectsDemoStateController CurrentStateController { get; set; }
        
        private void UpdatePlanesVisualization()
        {
            foreach (var plane in PlaneManager.trackables)
            {
                UpdatePlaneVisualization(plane);
            }
        }

        private void UpdatePlaneVisualization(ARPlane plane)
        {
            plane.GetComponent<MeshRenderer>().enabled = ShowPlanes;
            plane.GetComponent<LineRenderer>().enabled = ShowPlanes;
            plane.GetComponent<ARPlaneMeshVisualizer>().enabled = ShowPlanes;
        }
        
        void Start()
        {
            PlaneManager.planesChanged += PlaneManagerOnPlanesChanged;
            State = ARObjectsDemoState.Viewing;
        }

        private void PlaneManagerOnPlanesChanged(ARPlanesChangedEventArgs obj)
        {
            if (obj.added.Count > 0)
            {
                foreach (var arPlane in obj.added)
                {
                    UpdatePlaneVisualization(arPlane);
                }
            }
        }
        
        public void Reset()
        {
            ARObjectManager.Reset();
            ARSession.Reset();
        }

        private void OnStatePropertyChanged()
        {
            CurrentStateController?.Deactivate();
            CurrentStateController = State switch
            {
                ARObjectsDemoState.Adding => StateControllerAdding,
                ARObjectsDemoState.Editing => StateControllerEditing,
                _ => StateControllerViewing
            };
            CurrentStateController.Activate();
            OnStateChanged?.Invoke();
        }
        
        public event Action OnStateChanged;

        public void SwitchState(ARObjectsDemoState desiredState)
        {
            State = desiredState;
        }

        private void Awake()
        {
            EventBus.AddListener(this);
        }

        private void OnDestroy()
        {
            EventBus.RemoveListener(this);
        }

        public void OnEvent(OnARSessionReset data)
        {
            if (State != ARObjectsDemoState.Viewing) SwitchState(ARObjectsDemoState.Viewing);
        }
    }

}
