// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Amax.MobileARExample
{
    
    public static class ARFacePointsFactory
    {

        public static IARFacePoints CreateInstance(ARFace face)
            => Application.platform switch
            {
                RuntimePlatform.Android => new ARFacePointsARCore(face),
                RuntimePlatform.IPhonePlayer => new ARFacePointsARKit(face),
                _ => null
            };

    }
    
}