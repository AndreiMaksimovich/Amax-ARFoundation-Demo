using System;
using System.Collections;
using System.Collections.Generic;
using Amax.MobileARDemo;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ARFaceDemo))]
public class ARFaceDemoFeatureTogglesController : MonoBehaviour
{

    [Header("Toggles")] 
    [SerializeField] private Toggle toggleHat;
    [SerializeField] private Toggle toggleGlasses, maskOcclusion;
    
    private ARFaceDemo _faceDemo;

    private void Awake()
    {
        _faceDemo = GetComponent<ARFaceDemo>();
    }
    
    void Start()
    {
        // hat
        toggleHat.isOn = _faceDemo.ShowHat;
        toggleHat.onValueChanged.AddListener(value => _faceDemo.ShowHat = value);
        
        // glasses
        toggleGlasses.isOn = _faceDemo.ShowGlasses;
        toggleGlasses.onValueChanged.AddListener(value => _faceDemo.ShowGlasses = value);
        
        // mask occlusion
        maskOcclusion.isOn = _faceDemo.UseMaskOcclusion;
        maskOcclusion.onValueChanged.AddListener(value => _faceDemo.UseMaskOcclusion = value);
    }
    
    
}
