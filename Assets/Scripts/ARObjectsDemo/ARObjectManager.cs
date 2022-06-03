// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Amax.MobileARExample
{
    public class ARObjectManager: MonoBehaviour
    {
        
        [field: SerializeField] public ARObjectGroup ARObjects { get; set; }
        [SerializeField] private Transform arObjectRoot;

        public List<IARObjectInstance> SpawnedObjects { get; set; } = new List<IARObjectInstance>();

        public IARObjectInstance SpawnARObject(ARObject arObject, Vector3 position, Quaternion rotation, ARObjectDisplayMode displayMode = ARObjectDisplayMode.Normal)
        {
            var arObjectGameObject = Instantiate(arObject.Prefab, position, rotation, arObjectRoot);
            var arObjectInstance = arObjectGameObject.GetComponent<IARObjectInstance>();
            arObjectInstance.Initialize(arObject);
            arObjectInstance.Visualizer.DisplayMode = displayMode;
            SpawnedObjects.Add(arObjectInstance);
            return arObjectInstance;
        }

        public void Reset()
        {
            foreach (var aroInstance in SpawnedObjects)
            {
                Destroy(aroInstance.GameObject);
            }
            SpawnedObjects.Clear();
        }

        public void DestroyInstance(IARObjectInstance arObjectInstance)
        {
            SpawnedObjects.Remove(arObjectInstance);
            Destroy(arObjectInstance.GameObject);
        }
        
    }
}