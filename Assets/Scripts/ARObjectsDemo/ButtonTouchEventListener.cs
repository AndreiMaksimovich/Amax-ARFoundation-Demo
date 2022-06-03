// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Amax.MobileARExample
{
    
    [RequireComponent(typeof(Button))]
    public class ButtonTouchEventListener: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            OnTouchDown?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnTouchUp?.Invoke(this);
        }

        public event Action<ButtonTouchEventListener> OnTouchDown, OnTouchUp;

    }
}