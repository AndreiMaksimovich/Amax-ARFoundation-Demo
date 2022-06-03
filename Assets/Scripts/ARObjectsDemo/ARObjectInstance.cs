// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Amax.MobileARExample
{
    
    public class ARObjectInstance: MonoBehaviour, IARObjectInstance
    {

        public GameObject GameObject => gameObject;
        public ARObject ARObject { get; private set; }
        public ARObjectConfiguration Configuration { get; set; }
        public IARObjectVisualizer Visualizer { get; private set; }
        public IARObjectPositioner Positioner { get; private set; }
        public IARObjectAnchor Anchor { get; private set; }
        
        public void Initialize(ARObject arObject, ARObjectConfiguration configuration = null)
        {
            ARObject = arObject;
            Configuration = configuration ?? arObject.Configuration;
            Anchor = GetComponentInChildren<IARObjectAnchor>();
            Visualizer = GetComponentInChildren<IARObjectVisualizer>();
            Positioner = GetComponentInChildren<IARObjectPositioner>();
        }

        
    }
    
}