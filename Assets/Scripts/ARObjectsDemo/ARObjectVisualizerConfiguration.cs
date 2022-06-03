// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;

namespace Amax.MobileARExample
{
    
    [CreateAssetMenu(menuName = "Amax.MobileARExample/ARObjectVisualizerConfiguration", fileName = "ARObjectVisualizerConfiguration.asset")]
    public class ARObjectVisualizerConfiguration: ScriptableObject
    {

        [field: SerializeField] public Color EditModeTint { get; set; }
        [field: SerializeField] public Color PreviewModeTint { get; set; }

    }
}