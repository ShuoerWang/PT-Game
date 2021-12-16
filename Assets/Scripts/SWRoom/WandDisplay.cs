using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WandDisplay : MonoBehaviour
{

    private const int MAX_PRESSURE = 40;
    private const int MAX_FREQUENCY = 20;
    private const float SPEED_SHOCKS = 0.2f;

    public TMP_Text pressureValue;
    public TMP_Text frequencyValue;
    public TMP_Text shocksValue;
    public Inventory toolSelect;
    public AudioSource audioSourceProbe;

    private int currentPressure = 0;
    private int currentFrequency = 0;
    private float currentShocks = 0.0f;
    private bool wandOn = false;
    private bool shouldPlaySound = false;
    private bool soundPlaying = false;

    public Treatment treatment;

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
        currentPressure = 0;
        currentFrequency = 0;

        if (toolSelect.IsWand())
        {
            toolSelect.DimCurrentTool();
        }
    }

    public void ToggleWand()
    {
        if (wandOn)
        {
            TurnOffWand();
        }
        else if (PlayerPrefs.GetInt("machineOn") == 1
            && PlayerPrefs.GetInt("wandPlugged") == 1)
        {
            wandOn = !wandOn;
        } else if (PlayerPrefs.GetInt("machineOn") == 1)
        {
            treatment.triggerMachineOnWandhint();
        } else
        {
            treatment.triggerMachineOffWandhint();
        }
        DisplaySettings();
    }

    public void DisplaySettings()
    {
        if (wandOn && PlayerPrefs.GetInt("machineOn") == 1
            && PlayerPrefs.GetInt("wandPlugged") == 1)
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
        if (PlayerPrefs.GetInt("machineOn") == 1
            && PlayerPrefs.GetInt("wandPlugged") == 1)
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
        if (PlayerPrefs.GetInt("machineOn") == 1
            && PlayerPrefs.GetInt("wandPlugged") == 1)
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
        if (PlayerPrefs.GetInt("machineOn") == 1
            && PlayerPrefs.GetInt("wandPlugged") == 1)
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
        if (PlayerPrefs.GetInt("machineOn") == 1
            && PlayerPrefs.GetInt("wandPlugged") == 1)
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

    public void resetWand()
    {
        TurnOffWand();
        currentShocks = 0.0f;
    }

}
