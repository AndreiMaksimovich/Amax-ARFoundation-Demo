// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

#region

using Amax.Navigation;
using Amax.UI.ScreenTransition;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace Amax.MobileARDemo.UI
{
    
    [RequireComponent(typeof(Button))]
    public class SceneNavigationBackButton : MonoBehaviour
    {

        public bool IsVisible
        {
            get => gameObject.activeSelf;
            set => gameObject.SetActive(value);
        }

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnButtonClick);
            IsVisible = SceneNavigation.History.Count > 1;
        }

        private void OnButtonClick()
        {
            ScreenFadeAnimation.FadeOut(() => SceneNavigation.LoadPreviousScene());
        }

    }
    
}