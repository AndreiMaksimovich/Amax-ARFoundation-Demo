// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;

namespace Amax.MobileARExample
{

    public interface IARFacePoints
    {

        public Vector3 NoseTipModel { get; }
        public Vector3 NoseTip { get; }

        public Vector3 EyeRightModel { get; }
        public Vector3 EyeRight { get; }

        public Vector3 EyeLeftModel { get; }
        public Vector3 EyeLeft { get; }

        public Vector3 EyebrowLeftModel { get; }
        public Vector3 EyebrowLeft { get; }

        public Vector3 EyebrowRightModel { get; }
        public Vector3 EyebrowRight { get; }

        public Vector3 ChinModel { get; }
        public Vector3 Chin { get; }
        
        public Vector3 LipsTopModel { get; }
        public Vector3 LipsBottomModel { get; }
        public Vector3 LipsLeftModel { get; }
        public Vector3 LipsRightModel { get; }
        
        public Vector3 LipsTop { get; }
        public Vector3 LipsBottom { get; }
        public Vector3 LipsLeft { get; }
        public Vector3 LipsRight { get; }
        
        public Vector3 MouthCenterModel { get; }
        public Vector3 MouthCenter { get; }
        
        public Vector3 ForeheadModel { get; }
        public Vector3 Forehead { get; }
        
        public Vector3 BorderTopModel { get; }
        public Vector3 BorderTop { get; }
        
        public Vector3 BorderLeftModel { get; }
        public Vector3 BorderLeft { get; }
        
        public Vector3 BorderRightModel { get; }
        public Vector3 BorderRight { get; }
        
        public Vector3 NoseBridgeModel { get; }
        public Vector3 NoseBridge { get; }
        
    }

}
