// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Amax.MobileARExample
{
    public abstract class ARFacePointsBase: IARFacePoints
    {

        protected ARFace Face { get; set; }

        public ARFacePointsBase(ARFace face)
        {
            Face = face;
        }
        
        public abstract Vector3 NoseTipModel { get; }
        public virtual Vector3 NoseTip => ModelToWorldPoint(NoseTipModel);
        
        public abstract Vector3 EyeRightModel { get; }
        public virtual Vector3 EyeRight => ModelToWorldPoint(EyeRightModel);
        
        public abstract Vector3 EyeLeftModel { get; }
        public virtual Vector3 EyeLeft => ModelToWorldPoint(EyeLeftModel);
        
        public abstract Vector3 EyebrowLeftModel { get; }
        public virtual Vector3 EyebrowLeft => ModelToWorldPoint(EyebrowLeftModel);
        
        public abstract Vector3 EyebrowRightModel { get; }
        public virtual Vector3 EyebrowRight => ModelToWorldPoint(EyebrowRightModel);
        
        public abstract Vector3 ChinModel { get; }
        public virtual Vector3 Chin => ModelToWorldPoint(ChinModel);
        
        public abstract Vector3 LipsTopModel { get; }
        public abstract Vector3 LipsBottomModel { get; }
        public abstract Vector3 LipsLeftModel { get; }
        public abstract Vector3 LipsRightModel { get; }
        
        public virtual Vector3 LipsTop => ModelToWorldPoint(LipsTopModel);
        public virtual Vector3 LipsBottom => ModelToWorldPoint(LipsBottomModel);
        public virtual Vector3 LipsLeft => ModelToWorldPoint(LipsLeftModel);
        public virtual Vector3 LipsRight => ModelToWorldPoint(LipsRightModel);
        
        public abstract Vector3 MouthCenterModel { get; }
        public virtual Vector3 MouthCenter => ModelToWorldPoint(MouthCenterModel);
        public abstract Vector3 ForeheadModel { get; }
        public virtual Vector3 Forehead => ModelToWorldPoint(ForeheadModel);
        
        public abstract Vector3 BorderTopModel { get; }
        public virtual Vector3 BorderTop => ModelToWorldPoint(BorderTopModel);
        
        public abstract Vector3 BorderLeftModel { get; }
        public virtual Vector3 BorderLeft => ModelToWorldPoint(BorderLeftModel);
        
        public abstract Vector3 BorderRightModel { get; }
        public virtual Vector3 BorderRight => ModelToWorldPoint(BorderRightModel);
        public abstract Vector3 NoseBridgeModel { get; }
        public virtual Vector3 NoseBridge => ModelToWorldPoint(NoseBridgeModel);

        protected Vector3 GetVertex(int index) => Face.vertices[index];
        
        protected Vector3 GetVerticesCenter(params int[] vertexIndexes)
        {
            if (vertexIndexes == null || vertexIndexes.Length == 0) return Vector3.negativeInfinity;
            var vertices = new Vector3[vertexIndexes.Length];
            for (var i = 0; i < vertexIndexes.Length; i++)
            {
                vertices[i] = Face.vertices[vertexIndexes[i]];
            }
            return GetVerticesCenter(vertices);
        }

        protected Vector3 GetVerticesCenter(params Vector3[] vertices)
        {
            if (vertices == null || vertices.Length == 0) return Vector3.negativeInfinity;
            var result = vertices[0];
            for (var i = 1; i < vertices.Length; i++)
            {
                result += vertices[i];
            }
            return result / vertices.Length;
        }

        protected Vector3 GetModelNormal(int vertexIndex) => Face.normals[vertexIndex];

        protected Vector3 GetNormal(int vertexIndex) => ModelToWorldNormal(GetModelNormal(vertexIndex));

        protected Vector3 ModelToWorldNormal(Vector3 normal) => Face.transform.localToWorldMatrix.MultiplyVector(normal).normalized;
        
        protected Vector3 ModelToWorldPoint(Vector3 point) => Face.transform.localToWorldMatrix.MultiplyPoint3x4(point);

    }
}