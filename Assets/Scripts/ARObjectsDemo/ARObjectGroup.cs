// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System.Collections.Generic;
using UnityEngine;

namespace Amax.MobileARExample
{
    
    [CreateAssetMenu(menuName = "Amax.MobileARExample/ARObjectGroup", fileName = "ARObjectGroup.asset")]
    public class ARObjectGroup: ScriptableObject
    {
        
        [field: SerializeField] public List<ARObject> Objects { get; set; }

    }
}