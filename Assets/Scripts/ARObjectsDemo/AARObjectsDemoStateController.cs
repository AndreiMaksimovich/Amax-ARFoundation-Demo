// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using UnityEngine;

namespace Amax.MobileARExample
{
    public abstract class AARObjectsDemoStateController: MonoBehaviour, IARObjectsDemoStateController
    {

        protected ARObjectsDemoController DemoController { get; set; }
        protected ARObjectsDemoUIController DemoUIController { get; set; }
        protected ARObjectManager ObjectManager { get; set; }
        
        private void Awake()
        {
            DemoController ??= GetComponentInParent<ARObjectsDemoController>();
            DemoUIController ??= GetComponentInParent<ARObjectsDemoUIController>();
            ObjectManager ??= GetComponentInParent<ARObjectManager>();
            OnAwake();
        }
        
        protected virtual void OnAwake() {}

        public bool Enabled { get; private set; }

        public virtual void Activate()
        {
            Enabled = true;
        }

        public virtual void Deactivate()
        {
            Enabled = false;
        }

        private void Update()
        {
            if (Enabled) StateUpdate();
        }
        
        protected virtual void StateUpdate() {}

        private void FixedUpdate()
        {
            if (Enabled) StateFixedUpdate();
        }
        
        protected virtual void StateFixedUpdate() {}

        private void LateUpdate()
        {
            if (Enabled) StateLateUpdate();
        }

        protected virtual void StateLateUpdate() { }
        
    }
}