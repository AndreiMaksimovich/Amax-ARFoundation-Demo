// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;

namespace Amax.MobileARExample
{
    public static class ARObjectRaycastUtils
    {

        public const string ARObjectsLayerName = "ARObjects";
        private static readonly int ARObjectsLayerMask;

        private static Camera Camera => Camera.main;
        
        static ARObjectRaycastUtils()
        {
            ARObjectsLayerMask = LayerMask.GetMask(ARObjectsLayerName);
        }

        public static IARObjectInstance GetARObjectCameraLookingAt()
        {
            var cameraTransform = Camera.transform;
            var ray = new Ray(cameraTransform.position, cameraTransform.forward);
            if (Physics.Raycast(ray, out var hit, float.MaxValue, ARObjectsLayerMask, QueryTriggerInteraction.Collide))
            {
                return hit.collider.gameObject.GetComponentInParent<IARObjectInstance>();
            }
            return null;
        }

    }
    
}