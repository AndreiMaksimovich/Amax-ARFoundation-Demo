// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

#region

using Amax.Navigation;
using Amax.UI.ScreenTransition;
using UnityEngine;
using UnityEngine.EventSystems;

#endregion

namespace Amax.MobileARDemo.UI
{

    public class SceneNavigationOpenSceneOnClick : MonoBehaviour, IPointerClickHandler
    {

        public SceneNavigationConfiguration.SceneConfiguration sceneConfiguration;

        public void OnPointerClick(PointerEventData eventData)
        {
            ScreenFadeAnimation.FadeOut(() => SceneNavigation.LoadScene(sceneConfiguration.ToScene()));
        }
    }

}
