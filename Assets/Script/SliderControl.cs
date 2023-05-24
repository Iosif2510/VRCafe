using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    private Slider mySlider;

    private float minValue;
    private float maxValue;
    private float incrementPortion;

    private void Awake()
    {
        mySlider = GetComponent<Slider>();
        minValue = mySlider.minValue;
        maxValue = mySlider.maxValue;
    }

    public void IncreaseValue()
    {
        float increment = (maxValue - minValue) / incrementPortion;
        increment = Math.Min(increment, maxValue - mySlider.value);

        mySlider.value += increment;
    }

    public void DecreaseValue()
    {
        float decrement = (maxValue - minValue) / incrementPortion;
        decrement = Math.Min(decrement, mySlider.value - minValue);

        mySlider.value -= decrement;
    }
}
