using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Amax.MobileARExample
{

    public class ARObjectsDemoStateControllerViewing : AARObjectsDemoStateController
    {

        [SerializeField] private Button addButton;
        [SerializeField] private Button editButton;

        protected override void OnAwake()
        {
            base.OnAwake();
            addButton.onClick.AddListener(() => DemoController.SwitchState(ARObjectsDemoState.Adding));
            editButton.onClick.AddListener(() => DemoController.SwitchState(ARObjectsDemoState.Editing));
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Deactivate()
        {
            base.Deactivate();
        }
        
    }

}
