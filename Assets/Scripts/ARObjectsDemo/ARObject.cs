// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;

namespace Amax.MobileARExample
{
    
    [CreateAssetMenu(menuName = "Amax.MobileARExample/ARObject", fileName = "ARObject.asset")]
    public class ARObject: ScriptableObject
    {

        [field: SerializeField] public GameObject Prefab { get; set; }
        [field: SerializeField] public ARObjectType Type { get; set; }
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public ARObjectConfiguration Configuration { get; set; }

    }
    
}