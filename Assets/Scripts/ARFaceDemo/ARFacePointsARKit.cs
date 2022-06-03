// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Amax.MobileARExample
{
    public class ARFacePointsARKit: ARFacePointsBase
    {
        public ARFacePointsARKit(ARFace face) : base(face) { }
        public override Vector3 NoseTipModel => GetVertex(7);
        public override Vector3 EyeLeftModel => GetVerticesCenter(1076, 1062);
        public override Vector3 EyeLeft => Face.leftEye!=null ? Face.leftEye.position : base.EyeLeft;
        public override Vector3 EyeRightModel => GetVerticesCenter(1094, 1108);
        public override Vector3 EyeRight => Face.rightEye!=null ? Face.rightEye.position : base.EyeRight;
        public override Vector3 EyebrowLeftModel => GetVertex(327);
        public override Vector3 EyebrowRightModel => GetVertex(762);
        public override Vector3 ChinModel => GetVertex(1049);
        public override Vector3 LipsTopModel => GetVertex(22);
        public override Vector3 LipsBottomModel => GetVertex(26);
        public override Vector3 LipsLeftModel => GetVertex(635);
        public override Vector3 LipsRightModel => GetVertex(186);
        public override Vector3 MouthCenterModel => GetVerticesCenter(22, 26);
        public override Vector3 ForeheadModel => GetVertex(1022);
        public override Vector3 BorderTopModel => GetVertex(20);
        public override Vector3 BorderLeftModel => GetVertex(967);
        public override Vector3 BorderRightModel => GetVertex(999);
        public override Vector3 NoseBridgeModel => GetVertex(36);
    }
}