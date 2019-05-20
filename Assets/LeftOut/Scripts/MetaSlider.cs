﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaSlider : MonoBehaviour
{
    private static MetaSlider instance;
    public static MetaSlider GetInstance()
    {
        return instance;
    }

    public int activeSliderIndex = 0;
    float elapsedCompletionPct = 0;
    public float totalCompletionPct;
    public float currentSliderValue;

    int completedWorldCount;

    [System.Serializable]
    public class StageInfo
    {
        public int world, level;
    }

    [SerializeField]
    public StageInfo stageInfo;

    public GiantSlider[] sliders = new GiantSlider[4];

    public delegate void SliderActivatedEvent();
    public static event SliderActivatedEvent OnActiveSliderChanged;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {

        SetSliderActive(activeSliderIndex);

        GiantSlider.OnValueChanged += UpdateMetaSlider;
    }

    public float GetSliderValue(int index)
    {
        return sliders[index].percent;
    }

    void UpdateMetaSlider()
    {
        // Find slider value and progress
        currentSliderValue = GetSliderValue(activeSliderIndex);
        totalCompletionPct = elapsedCompletionPct + currentSliderValue / 4;

        // Set stage info
        SetStageInfo();

    }

    public int FindSliderIndex(GiantSlider target)
    {
        for (int i = 0; i < 4; i++)
        {
            if (sliders[i] == target)
            {
                return i;
            }
        }
        Debug.LogError("Slider index not found.");
        return -1;
    }

    public void HandleSliderCompleted(GiantSlider completedSlider)
    {
        // Debug.Log("Slider completed with index: " + FindSliderIndex(completedSlider));

        if (activeSliderIndex == FindSliderIndex(completedSlider))
        {
            // Debug.Log("active slider index matched. slider index increased");

            activeSliderIndex++;

            if (activeSliderIndex >= 4)
            {
                // roll over when you hit 4
                activeSliderIndex = 0;
                completedWorldCount++;
            }

            SetStageInfo();
            SetSliderActive(activeSliderIndex);

            elapsedCompletionPct = activeSliderIndex * .25f; // round the completed value to nearest quarter
        }
    }

    void SetStageInfo()
    {
        stageInfo.world = completedWorldCount + Mathf.FloorToInt(totalCompletionPct) + 1;
        stageInfo.level = activeSliderIndex + 1;
    }

    void SetSliderActive(int index)
    {
        for (int i = 0; i < 4; i++)
        {
            sliders[i].GetComponent<GiantSlider>().isActive = i == index ? true : false;
        }

        if (OnActiveSliderChanged != null)
        {
            OnActiveSliderChanged();
        }
    }

    public int GetSliderIndex()
    {
        return activeSliderIndex;
    }

    public float GetCurrentSliderValue()
    {
        return currentSliderValue;
    }

}