using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Valve.VR.InteractionSystem;

public class ControlUI : MonoBehaviour
{
    [SerializeField] private GameObject buttonsUI;
    [SerializeField] private GameObject slidersUI;
    [SerializeField] private Transform buttonsLayout;

    [SerializeField] private TextMeshProUGUI placeLabel;
    [SerializeField] private Slider hueSlider;
    [SerializeField] private Slider saturationSlider;
    [SerializeField] private Slider valueSlider;

    [SerializeField] private Slider lightRangeSlider;
    [SerializeField] private Slider lightIntensitySlider;

    [SerializeField] private LightControl lightControl;

    private List<Transform> lightParentList;
    private Transform currentLightParent = null;

    [SerializeField] private UIElement placeButtonPrefab;

    private List<UIElement> placeButtons = new List<UIElement>();

    private void Awake()
    {
        lightParentList = lightControl.LightParentList;
    }

    private void Start()
    {
        SliderUIOut();
        SetButtons();
    }

    private void Update()
    {
    }

    public void SliderUIOut()
    {
        buttonsUI.SetActive(true);
        slidersUI.SetActive(false);
    }

    private void SetButtons()
    {
        foreach (var button in placeButtons)
        {
            Destroy(button.gameObject);
        }
        
        foreach (var parent in lightParentList)
        {
            UIElement newButton = Instantiate(placeButtonPrefab, buttonsLayout);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = parent.gameObject.name;
            placeButtons.Add(newButton);
            newButton.onHandClick.AddListener((hand) => SetSlider(parent));
            currentLightParent = parent;
        }
    }

    private void SetSlider(Transform lightParent)
    {
        placeLabel.text = lightParent.gameObject.name;

        lightRangeSlider.value = lightControl.GetLightRange(lightParent);
        lightIntensitySlider.value = lightControl.GetLightIntensity(lightParent);

        Vector3 hsvValue = lightControl.GetLightColorHSV(lightParent);
        hueSlider.value = hsvValue.x;
        saturationSlider.value = hsvValue.y;
        valueSlider.value = hsvValue.z;
        
        lightRangeSlider.onValueChanged.AddListener((newValue) =>
        {
            lightControl.SetLightRange(lightParent, newValue);
        });
        
        lightIntensitySlider.onValueChanged.AddListener((newValue) =>
        {
            lightControl.SetLightIntensity(lightParent, newValue);
        });
        
        hueSlider.onValueChanged.AddListener((newValue) =>
        {
            Color newColor = Color.HSVToRGB(newValue, saturationSlider.value, valueSlider.value);
            lightControl.SetLightColor(lightParent, newColor);
        });
        
        saturationSlider.onValueChanged.AddListener((newValue) =>
        {
            Color newColor = Color.HSVToRGB(hueSlider.value, newValue, valueSlider.value);
            lightControl.SetLightColor(lightParent, newColor);
        });
        
        valueSlider.onValueChanged.AddListener((newValue) =>
        {
            Color newColor = Color.HSVToRGB(hueSlider.value, saturationSlider.value, newValue);
            lightControl.SetLightColor(lightParent, newColor);
        });

        buttonsUI.SetActive(false);
        slidersUI.SetActive(true);
    }
}
