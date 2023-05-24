using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] private Button placeButtonPrefab;

    private List<Button> placeButtons = new List<Button>();

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
            Button newButton = Instantiate(placeButtonPrefab, buttonsLayout);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = parent.gameObject.name;
            placeButtons.Add(newButton);
            newButton.onClick.AddListener(() => SetSlider(parent));
            currentLightParent = parent;
        }
    }

    private void SetSlider(Transform lightParent)
    {
        placeLabel.text = lightParent.gameObject.name;

        lightRangeSlider.value = lightControl.GetLightRange(lightParent) / 64;
        lightIntensitySlider.value = lightControl.GetLightIntensity(lightParent) / 2;

        Vector3 hsvValue = lightControl.GetLightColorHSV(lightParent);
        hueSlider.value = hsvValue.x;
        saturationSlider.value = hsvValue.y;
        valueSlider.value = hsvValue.z;
        
        lightRangeSlider.onValueChanged.AddListener((newValue) =>
        {
            lightControl.SetLightRange(lightParent, newValue * 64);
        });
        
        lightIntensitySlider.onValueChanged.AddListener((newValue) =>
        {
            lightControl.SetLightIntensity(lightParent, newValue * 2);
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
