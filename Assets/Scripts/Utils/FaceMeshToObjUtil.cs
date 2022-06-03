// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System.Text;
using UnityEngine.XR.ARFoundation;

namespace Amax.MobileARExample
{
    public static class FaceMeshToObjUtil
    {

        public static string MeshToObj(ARFace face, bool addFaces = true)
        {
            var sb = new StringBuilder();

            // Vertices
            sb.AppendLine("# Vertices");
            var index = 0;
            foreach (var meshVertex in face.vertices)
            {
                sb.AppendLine($"v {meshVertex.x:N6} {meshVertex.y:N6} {meshVertex.z:N6} 1.0");
                index++;
            }
            
            // Vertex Normals
            if (face.normals.Length > 0)
            {
                sb.AppendLine("# Vertex Normals");
                foreach (var meshNormal in face.normals)
                {
                    sb.AppendLine($"vn {meshNormal.x:N6} {meshNormal.y:N6} {meshNormal.z:N6}");
                }
            }

            // Faces
            if (addFaces && face.indices.IsCreated && face.indices.Length > 0)
            {
                sb.AppendLine("# Polygonal faces");
                var faceCount = face.indices.Length / 3;
                for (var i = 0; i < faceCount; i++)
                {
                    index = i * 3;
                    var vertices = new int[] {face.indices[index], face.indices[index + 1], face.indices[index + 2]};
                    sb.AppendLine($"f {vertices[0]} {vertices[1]} {vertices[2]}");
                }
            }

            return sb.ToString();
        }
        
    }
}