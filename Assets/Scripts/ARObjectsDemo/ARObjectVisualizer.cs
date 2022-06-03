// Copyright (c) 2022 Andrei Maksimovich
// See LICENSE.md

using System.Collections.Generic;
using UnityEngine;

namespace Amax.MobileARExample
{
    public class ARObjectVisualizer: MonoBehaviour, IARObjectVisualizer
    {
        
        [field: SerializeField] private ARObjectVisualizerConfiguration Configuration { get; set; }

        private ARObjectDisplayMode displayMode = ARObjectDisplayMode.Normal;
        public ARObjectDisplayMode DisplayMode
        {
            get => displayMode;
            set
            {
                if (value == displayMode) return;
                displayMode = value;
                OnDisplayModeChanged();
            } 
        }

        private readonly List<Renderer> _renderers = new List<Renderer>();
        private readonly List<List<Color>> _initialColors = new List<List<Color>>();

        private bool isInitialized;
        private void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;
            foreach (var arObjRenderer in GetComponentsInChildren<Renderer>())
            {
                _renderers.Add(arObjRenderer);
                var colorList = new List<Color>();
                _initialColors.Add(colorList);
                foreach (var material in arObjRenderer.materials)
                {
                    colorList.Add(material.color);
                }
            }
        }

        private void OnDisplayModeChanged()
        {
            
            Initialize();
            if (DisplayMode == ARObjectDisplayMode.Normal)
            {
                for (var i = 0; i < _renderers.Count; i++)
                {
                    for (var j = 0; j < _renderers[i].materials.Length; j++)
                    {
                        _renderers[i].materials[j].color = _initialColors[i][j];
                    }
                }
            }
            else
            {
                var tintColor = DisplayMode switch
                {
                    ARObjectDisplayMode.Edit => Configuration.EditModeTint,
                    _ => Configuration.PreviewModeTint
                };
                for (var i = 0; i < _renderers.Count; i++)
                {
                    for (var j = 0; j < _renderers[i].materials.Length; j++)
                    {
                        _renderers[i].materials[j].color = _initialColors[i][j] * tintColor;
                    }
                }
            }
        }
        
    }
}