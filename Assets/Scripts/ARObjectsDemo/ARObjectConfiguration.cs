// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System;
using UnityEngine;

namespace Amax.MobileARExample
{
    [Serializable]
    public class ARObjectConfiguration
    {

        [field: SerializeField] public bool RotationAllowed { get; set; } = true;
        [field: SerializeField] public bool MovementAllowed { get; set; } = true;

        public ARObjectConfiguration Clone()
            => new ()
            {
                MovementAllowed = MovementAllowed,
                RotationAllowed = RotationAllowed
            };

    }
}