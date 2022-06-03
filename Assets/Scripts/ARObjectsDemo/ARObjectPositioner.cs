// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System;
using UnityEngine;

namespace Amax.MobileARExample
{
    public class ARObjectPositioner: MonoBehaviour, IARObjectPositioner
    {

        private IARObjectInstance arObjectInstance;
        
        private void Awake()
        {
            arObjectInstance = GetComponentInParent<IARObjectInstance>();
        }

        public Vector3 DesiredPosition { get; set; }
        public Vector3 DesiredRotation { get; set; }
        public bool IsAttachedToPlain { get; set; }
        public float Rotation { get; set; }
        
        public void UpdatePositionAndRotation()
        {
            var objTransform = arObjectInstance.GameObject.transform;
            
            objTransform.position = DesiredPosition;
            objTransform.eulerAngles = DesiredRotation;
            
            // Anchor
            if (IsAttachedToPlain && arObjectInstance.Anchor!=null)
            {
                objTransform.position += objTransform.position - arObjectInstance.Anchor.Position;
            }
            
            // Rotation
            objTransform.Rotate(arObjectInstance.ARObject.Type==ARObjectType.Horizontal ? Vector3.up : Vector3.forward, Rotation);
        }
        
    }
}