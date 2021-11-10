using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{

    public Image powerButtonImage;
    public Sprite powerButtonOnSprite;
    public Sprite powerButtonOffSprite;
    public AudioSource audioSourcePowerOn;
    public AudioSource audioSourcePowerOff;
    public ToolSelect toolSelect;
    public Wand wand;

    private bool powerOn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePower()
    {
        powerOn = !powerOn;
        if (powerOn)
        {
            audioSourcePowerOn.Play();
            powerButtonImage.sprite = powerButtonOnSprite;
        }
        else
        {
            audioSourcePowerOff.Play();
            powerButtonImage.sprite = powerButtonOffSprite;
            wand.TurnOffWand();
        }
    }

    public bool IsMachineOn()
    {
        return powerOn;
    }

}
