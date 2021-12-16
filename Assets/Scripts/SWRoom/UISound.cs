using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioSource uiClick0;
    public AudioSource uiClick1;
    public AudioSource uiClick2;
    public AudioSource powerButton;

    // Start is called before the first frame update
    void Start()
    {
        uiClick0.volume = 0.5f;
        uiClick1.volume = 0.5f;
        uiClick2.volume = 0.5f;
        powerButton.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickArrow()
    {
        uiClick2.Play();
    }

    public void ItemCollect()
    {
        uiClick1.Play();
    }

    public void ClickButton()
    {
        uiClick0.Play();
    }

    public void powerButtonClick()
    {
        powerButton.Play();
    }
}
