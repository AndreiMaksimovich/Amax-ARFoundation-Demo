// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;

namespace Amax.MobileARExample
{
    public class ARObjectAnchor: MonoBehaviour, IARObjectAnchor
    {
        public Vector3 LocalPosition => transform.localPosition;
        public Vector3 Position => transform.position;
        public Quaternion LocalRotation => transform.localRotation;
        public Quaternion Rotation => transform.rotation;
    }
}