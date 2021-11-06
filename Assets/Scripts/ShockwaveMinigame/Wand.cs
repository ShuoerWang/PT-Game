using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wand : MonoBehaviour
{

    private const int MAX_PRESSURE = 40;
    private const int MAX_FREQUENCY = 20;
    private const float SPEED_SHOCKS = 0.2f;

    public TMP_Text pressureValue;
    public TMP_Text frequencyValue;
    public TMP_Text shocksValue;
    public ToolSelect toolSelect;
    public AudioSource audioSourceProbe;
    public Machine machine;

    private int currentPressure = 0;
    private int currentFrequency = 0;
    private float currentShocks = 0.0f;
    private bool wandOn = false;
    private bool shouldPlaySound = false;
    private bool soundPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        if (wandOn && currentFrequency > 0)
        {
            if (shouldPlaySound && !soundPlaying)
            {
                StartCoroutine(DisableSound());
                audioSourceProbe.Play();
            }
        }
    }

    public void SetShouldPlaySound(bool shouldPlaySound)
    {
        this.shouldPlaySound = shouldPlaySound;
    }

    public bool IsWandOn()
    {
        return wandOn;
    }

    public void TurnOffWand()
    {
        wandOn = false;
    }

    public void ToggleWand()
    {
        if (machine.IsMachineOn())
        {
            wandOn = !wandOn;
            if (wandOn)
            {
                toolSelect.HighlightCurrentTool();
            }
            else
            {
                toolSelect.DimCurrentTool();
            }
        }
        DisplaySettings();
    }

    public void DisplaySettings()
    {
        if (machine.IsMachineOn())
        {
            if (currentPressure % 10 == 0)
            {
                pressureValue.text = (currentPressure / 10).ToString() + ".0";
            }
            else
            {
                pressureValue.text = (currentPressure / 10.0f).ToString();
            }
            frequencyValue.text = currentFrequency.ToString();
            shocksValue.text = Mathf.RoundToInt(currentShocks).ToString();
        }
        else
        {
            pressureValue.text = "";
            frequencyValue.text = "";
            shocksValue.text = "";
        }
    }

    public int GetPressure()
    {
        return currentPressure;
    }

    public void DecreasePressure()
    {
        if (machine.IsMachineOn())
        {
            if (currentPressure > 0)
            {
                currentPressure -= 1;
            }
            DisplaySettings();
        }
    }

    public void IncreasePressure()
    {
        if (machine.IsMachineOn())
        {
            if (currentPressure < MAX_PRESSURE)
            {
                currentPressure += 1;
            }
            DisplaySettings();
        }
    }

    public int GetFrequency()
    {
        return currentFrequency;
    }

    public void DecreaseFrequency()
    {
        if (machine.IsMachineOn())
        {
            if (currentFrequency > 0)
            {
                currentFrequency -= 1;
            }
            DisplaySettings();
        }
    }

    public void IncreaseFrequency()
    {
        if (machine.IsMachineOn())
        {
            if (currentFrequency < MAX_FREQUENCY)
            {
                currentFrequency += 1;
            }
            DisplaySettings();
        }
    }

    public float GetShocks()
    {
        return currentShocks;
    }

    public void AddShocks()
    {
        currentShocks += currentFrequency * SPEED_SHOCKS;
        DisplaySettings();
    }

    public void SetupWand()
    {
        soundPlaying = false;
        DisplaySettings();
    }

    private IEnumerator DisableSound()
    {
        soundPlaying = true;
        if (currentFrequency <= 5)
        {
            yield return new WaitForSeconds(0.4f);
        }
        else if (currentFrequency <= 10)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else if (currentFrequency <= 15)
        {
            yield return new WaitForSeconds(0.2f);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        soundPlaying = false;
    }

}
