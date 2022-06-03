// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System;
using UnityEngine;

namespace Amax.MobileARExample
{
    
    public enum ARObjectsDemoState
    {
        Viewing, Adding, Editing
    }

    public interface IARObjectsDemoStateController
    {
        public bool Enabled { get; }
        public void Activate();
        public void Deactivate();
    }
    
    public enum ARObjectDisplayMode
    {
        Normal,
        Preview,
        Edit
    }

    public enum ARObjectType
    {
        Vertical,
        Horizontal
    }

    public interface IARObjectInstance
    {
        public GameObject GameObject { get; }
        public void Initialize(ARObject arObject, ARObjectConfiguration configuration = null);
        public ARObject ARObject { get; }
        public ARObjectConfiguration Configuration { get; set; }
        public IARObjectVisualizer Visualizer { get; }
        public IARObjectPositioner Positioner { get; }
        public IARObjectAnchor Anchor { get; }
    }
    
    public interface IARObjectVisualizer
    {
        public ARObjectDisplayMode DisplayMode { get; set; }
    }

    public interface IARObjectSelector
    {
        public ARObject SelectedObject { get; }
        public event Action<ARObject> OnSelectedObjectChanged;
    }

    public interface IARObjectAnchor
    {
        public Vector3 LocalPosition { get; }
        public Vector3 Position { get; }
        public Quaternion LocalRotation { get; }
        public Quaternion Rotation { get; }
    }

    public interface IARObjectPositioner
    {
        Vector3 DesiredPosition { get; set; }
        Vector3 DesiredRotation { get; set; }
        public bool IsAttachedToPlain { get; set; }
        public float Rotation { get; set; }
        public void UpdatePositionAndRotation();
    }
    
    public class OnARSessionReset: EventBusBaseEvent {}
    
}