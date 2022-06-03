// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Amax.MobileARExample
{
    
    public class ARFacePointsARCore: ARFacePointsBase
    {
        public ARFacePointsARCore(ARFace face) : base(face) { }
        public override Vector3 NoseTipModel => GetVertex(1);
        public override Vector3 EyeRightModel => GetVerticesCenter(144, 158);
        public override Vector3 EyeLeftModel => GetVerticesCenter(385, 373);
        public override Vector3 EyebrowLeftModel => GetVerticesCenter(66, 105);
        public override Vector3 EyebrowRightModel => GetVerticesCenter(296, 334);
        public override Vector3 ChinModel => GetVerticesCenter(175);
        public override Vector3 LipsTopModel => GetVertex(11);
        public override Vector3 LipsBottomModel => GetVertex(16);
        public override Vector3 LipsLeftModel => GetVertex(76);
        public override Vector3 LipsRightModel => GetVertex(306);
        public override Vector3 MouthCenterModel => GetVerticesCenter(11, 16);
        public override Vector3 ForeheadModel => GetVertex(151);
        public override Vector3 BorderTopModel => GetVertex(10);
        public override Vector3 BorderLeftModel => GetVertex(323);
        public override Vector3 BorderRightModel => GetVertex(93);
        public override Vector3 NoseBridgeModel => GetVertex(6);
    }
    
}