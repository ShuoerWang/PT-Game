using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Target : MonoBehaviour
{

    private const int TARGET_PRESSURE = 25;
    private const int TARGET_FREQUENCY = 10;
    private const int TARGET_SHOCKS = 2000;
    private const int REVEAL_BASE = 0;
    private const int REVEAL_STRUCTURE = 1;
    private const int REVEAL_LABEL = 2;
    private const int REVEAL_INFLAMMATION = 3;

    public TMP_Text patientDialogue;
    public ToolSelect toolSelect;
    public TMP_Text pressureValue;
    public TMP_Text frequencyValue;
    public Results results;
    public Wand wand;
    public List<AudioSource> painAudioSources;
    public GameObject patientBase;
    public List<GameObject> revealInflammations;
    public Sprite revealStructureSprite;
    public Sprite revealLabelSprite;
    public List<Sprite> revealInflammationSprites;

    private int randomSetup;
    private int currentReveal = REVEAL_BASE;
    private bool soundPlaying = false;

    private void Awake()
    {
        randomSetup = Random.Range(0, 3);
        if (randomSetup == 0)
        {
            transform.position = new Vector2(-2.25f, 1.0f);
        }
        else if (randomSetup == 1)
        {
            transform.position = new Vector2(-1.35f, 0.5f);
        }
        else if (randomSetup == 2)
        {
            transform.position = new Vector2(0.0f, 0.0f);
        }
        results.SetTargetPosition(transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        if (Input.GetMouseButtonDown(0))
        {
            float distance = (new Vector2(transform.position.x, transform.position.y) - worldPosition).magnitude;
            float horizontalDistance = Mathf.Abs(transform.position.x - worldPosition.x);
            float verticalDistance = Mathf.Abs(transform.position.y - worldPosition.y);
            if (toolSelect.IsGlove())
            {
                if (distance <= 0.8f)
                {
                    PlayPainSound(1.0f - distance);
                    if (distance <= 0.4f)
                    {
                        patientDialogue.text = "I feel a lot of pain in this area.";
                    }
                    else
                    {
                        patientDialogue.text = "I feel some pain in this area.";
                    }
                }
                else
                {
                    RaycastHit2D raycastHit = Physics2D.Raycast(worldPosition, Vector2.zero);
                    if (raycastHit.collider != null && raycastHit.transform.parent != null && raycastHit.transform.parent.tag.Equals("foot"))
                    {
                        if (horizontalDistance >= verticalDistance)
                        {
                            if (transform.position.x < worldPosition.x)
                            {
                                patientDialogue.text = "That's not the correct spot. The pain is more to the left.";
                            }
                            else
                            {
                                patientDialogue.text = "That's not the correct spot. The pain is more to the right.";
                            }
                        }
                        else if (horizontalDistance < verticalDistance)
                        {
                            if (transform.position.y < worldPosition.y)
                            {
                                patientDialogue.text = "That's not the correct spot. The pain is lower down.";
                            }
                            else
                            {
                                patientDialogue.text = "That's not the correct spot. The pain is higher up.";
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (toolSelect.IsWand())
            {
                if (wand.IsWandOn())
                {
                    RaycastHit2D[] raycastHits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
                    foreach (RaycastHit2D raycastHit in raycastHits)
                    {
                        if (raycastHit.collider != null && raycastHit.transform.parent != null && raycastHit.transform.parent.tag.Equals("foot"))
                        {
                            wand.SetShouldPlaySound(true);
                            wand.AddShocks();
                            results.AddProbePosition(worldPosition);
                            if (wand.GetPressure() > TARGET_PRESSURE || wand.GetFrequency() > TARGET_FREQUENCY)
                            {
                                float overPressure = (wand.GetPressure() - TARGET_PRESSURE) / 15.0f;
                                float overFrequency = (wand.GetFrequency() - TARGET_FREQUENCY) / 10.0f;
                                PlayPainSound(0.2f + overPressure + overFrequency);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            wand.SetShouldPlaySound(false);
        }
    }

    private void PlayPainSound(float volume)
    {
        if (!soundPlaying)
        {
            StartCoroutine(DisableSound());
            AudioSource audioSource = painAudioSources[Random.Range(0, 3)];
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    private IEnumerator DisableSound()
    {
        soundPlaying = true;
        yield return new WaitForSeconds(0.5f);
        soundPlaying = false;
    }

    public void DisplayPain()
    {
        currentReveal += 1;
        if (currentReveal > REVEAL_INFLAMMATION)
        {
            currentReveal = REVEAL_BASE;
        }
        GameObject revealInflammation = revealInflammations[randomSetup];
        if (currentReveal == REVEAL_BASE)
        {
            patientBase.SetActive(true);
            revealInflammation.SetActive(false);
        }
        else
        {
            patientBase.SetActive(false);
            revealInflammation.SetActive(true);
            if (currentReveal == REVEAL_STRUCTURE)
            {
                revealInflammation.GetComponent<SpriteRenderer>().sprite = revealStructureSprite;
            }
            else if (currentReveal == REVEAL_LABEL)
            {
                revealInflammation.GetComponent<SpriteRenderer>().sprite = revealLabelSprite;
            }
            else if (currentReveal == REVEAL_INFLAMMATION)
            {
                revealInflammation.GetComponent<SpriteRenderer>().sprite = revealInflammationSprites[randomSetup];
            }
        }
    }

    public void SetMachineResults()
    {
        if (TARGET_PRESSURE == wand.GetPressure())
        {
            results.SetCorrectPressureTrue();
        }
        if (TARGET_FREQUENCY == wand.GetFrequency())
        {
            results.SetCorrectFrequencyTrue();
        }
        if (Mathf.Abs(TARGET_SHOCKS - wand.GetShocks()) < 100.0f)
        {
            results.SetCorrectShocksTrue();
        }
    }

}
