// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Amax.MobileARExample
{
    public class ARObjectsDemoPointAndShootUtils: MonoBehaviour
    {
        [SerializeField] private float defaultARObjectDistanceFromCamera = 1.5f;
        [SerializeField] private ARObjectsDemoController arObjectsDemoController;
        
        private Camera Camera => Camera.main;
        
        public void UpdatePositionAndRotation(IARObjectInstance obj)
        {
            var objTransform = obj.GameObject.transform;
            var (position, plane) = RaycastCenterPlane();
            
            if (plane == null)
            {
                var cameraTransform = Camera.transform;
                position = cameraTransform.position + cameraTransform.forward * defaultARObjectDistanceFromCamera;
            }
            
            objTransform.position = position;

            if (obj.ARObject.Type == ARObjectType.Vertical)
            {
                if (plane == null)
                {
                    objTransform.LookAt(Camera.transform.position);
                    var angles = objTransform.eulerAngles;
                    angles.x = 0;
                    objTransform.eulerAngles = angles;
                }
                else
                {
                    objTransform.LookAt(objTransform.position + plane.normal);
                }
                obj.Positioner.DesiredRotation = objTransform.eulerAngles;
            }
            
            obj.Positioner.DesiredPosition = objTransform.position;
            obj.Positioner.IsAttachedToPlain = plane != null;
            obj.Positioner.UpdatePositionAndRotation();
        }
        
        private (Vector3, ARPlane) RaycastCenterPlane()
        {
            var screenPosition = Camera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            arObjectsDemoController.RaycastManager.Raycast(screenPosition, hits, TrackableType.Planes);
            return hits.Count > 0 ? (hits[0].pose.position, arObjectsDemoController.PlaneManager.GetPlane(hits[0].trackableId)) : (Vector3.zero, null);
        }
    }
}