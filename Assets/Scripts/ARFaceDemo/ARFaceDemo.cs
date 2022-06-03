// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using Amax.UI.Dialogs;
using Amax.MobileARExample;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Amax.MobileARDemo
{
    public class ARFaceDemo: MonoBehaviour
    {

        [SerializeField] private ARCameraManager cameraManager;
        [SerializeField] private ARFaceManager faceManager;
        [Header("Eyes")]
        [SerializeField] private Transform leftEye;
        [SerializeField] private Transform rightEye;
        
        [Header("Nose")]
        [SerializeField] private Transform nose;
        [SerializeField] private Transform noseBridge;
        
        [Header("")]
        [SerializeField] private Transform mouth;
        [SerializeField] private Transform forehead;
        [SerializeField] private Transform chin;
        [SerializeField] private Transform headTop;
        
        [Header("Lips")] [SerializeField] private Transform lipsTop;
        [SerializeField] private Transform
            lipsBottom,
            lipsLeft,
            lipsRight;


        [Header("Eyebrows")]
        [SerializeField] private Transform eyebrowLeft;
        [SerializeField] private Transform eyebrowRight;

        [Header("Borders")] [SerializeField] private Transform borderTop;
        [SerializeField] private Transform borderLeft, borderRight;

        [Header("Hat")] [SerializeField] private Transform hat;
        [Header("Glasses")] [SerializeField] private Transform glasses;
        [SerializeField] private Transform glassesLensLeft, glassesLensRight, glassesNoseBridge;

        [SerializeField] private bool showHat;
        public bool ShowHat
        {
            get => showHat;
            set
            {
                showHat = value;
                hat.gameObject.SetActive(showHat);
            }
        }
        
        [SerializeField] private bool showGlasses = true;
        public bool ShowGlasses
        {
            get => showGlasses;
            set
            {
                showGlasses = value;
                glasses.gameObject.SetActive(showGlasses);
            }
        }

        [SerializeField] private bool useMaskOcclusion = true;
        public bool UseMaskOcclusion
        {
            get => useMaskOcclusion;
            set
            {
                useMaskOcclusion = value;
                UpdateFaceMask();
            }
        }

        private void UpdateFaceMask()
        {
            if (Face == null) return;
            Face.GetComponent<ARFaceMeshVisualizer>().enabled = UseMaskOcclusion;
            Face.GetComponent<MeshRenderer>().enabled = UseMaskOcclusion;
        }

        private IARFacePoints FacePoints { get; set; }
        
        private void Start()
        {
            faceManager.facesChanged += OnfacesChanged;
            ShowGlasses = showGlasses;
            ShowHat = showHat;
            UseMaskOcclusion = useMaskOcclusion;
        }

        private const float HeadTopCoefficient = 0.85f;

        private void Update()
        {
            if (IsFaceAvailable)
            {

                var faceRegions = FacePoints;

                var faceLocalToWorldMatrix = Face.transform.localToWorldMatrix;
                
                // Head Top
                var headCenter = Face.transform.position;
                var headToDirection = faceLocalToWorldMatrix.MultiplyVector(Vector3.up).normalized;
                var headTopPosition = headCenter + headToDirection * (Vector3.Distance(faceRegions.BorderLeft, faceRegions.BorderRight) * HeadTopCoefficient);
                headTop.position = headTopPosition;
                headTop.rotation = faceLocalToWorldMatrix.rotation;

                // Hat
                if (ShowHat)
                {
                    hat.position = headTopPosition;
                    hat.rotation = faceLocalToWorldMatrix.rotation;
                }
                
                // Glasses
                if (ShowGlasses)
                {
                    var eyeDistance = Vector3.Distance(faceRegions.EyeLeft, faceRegions.EyebrowRight);
                    var glassLensesDistance = Vector3.Distance(glassesLensLeft.position, glassesLensRight.position);
                    var scaleMod = eyeDistance / glassLensesDistance;
                    
                    glasses.position = (glasses.position - glassesNoseBridge.position) + faceRegions.NoseBridge;
                    glasses.rotation = faceLocalToWorldMatrix.rotation;
                }

                // Nose
                nose.position = faceRegions.NoseTip;
                nose.rotation = faceLocalToWorldMatrix.rotation;
                
                // Nose
                noseBridge.position = faceRegions.NoseBridge;
                noseBridge.rotation = faceLocalToWorldMatrix.rotation;
                
                // Forehead
                forehead.position = faceRegions.Forehead;
                
                // Chin
                chin.position = faceRegions.Chin;
                
                // Eyes
                leftEye.position = faceRegions.EyeLeft;
                leftEye.rotation = faceLocalToWorldMatrix.rotation;
                
                rightEye.position = faceRegions.EyeRight;
                rightEye.rotation = faceLocalToWorldMatrix.rotation;
                
                // Eyebrows
                eyebrowLeft.position = faceRegions.EyebrowLeft;
                eyebrowLeft.rotation = faceLocalToWorldMatrix.rotation;
                
                eyebrowRight.position = faceRegions.EyebrowRight;
                eyebrowRight.rotation = faceLocalToWorldMatrix.rotation;
                
                // Mouth
                mouth.position = faceRegions.MouthCenter;
                mouth.rotation = faceLocalToWorldMatrix.rotation;

                // Lips
                lipsTop.position = faceRegions.LipsTop;
                lipsBottom.position = faceRegions.LipsBottom;
                lipsLeft.position = faceRegions.LipsLeft;
                lipsRight.position = faceRegions.LipsRight;
                
                // Borders
                borderTop.position = faceRegions.BorderTop;
                borderLeft.position = faceRegions.BorderLeft;
                borderRight.position = faceRegions.BorderRight;
            }
        }

        private void OnfacesChanged(ARFacesChangedEventArgs obj)
        {
            // Remove
            if (obj.removed.Count > 0)
            {
                IsFaceAvailable = false;
                Face = null;
                FacePoints = null;
            }
            // Add
            if (obj.added.Count > 0)
            {
                IsFaceAvailable = true;
                Face = obj.added[0];
                FacePoints = ARFacePointsFactory.CreateInstance(Face);
                UpdateFaceMask();
            }
        }

        public void OnButtonClick_Hat()
        {
            ShowHat = !ShowHat;
        } 
        
        public void OnButtonClick_Glasses()
        {
            ShowGlasses = !ShowGlasses;
        } 

        private bool _isFaceAvailable = false;
        public bool IsFaceAvailable
        {
            get => _isFaceAvailable;
            private set
            {
                _isFaceAvailable = value;
            }
        }
        
        public ARFace Face { get; private set; }
        
    }
}