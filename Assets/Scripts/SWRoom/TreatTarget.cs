using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreatTarget : MonoBehaviour
{

    private const int TARGET_PRESSURE = 25;
    private const int TARGET_FREQUENCY = 10;
    private const int TARGET_SHOCKS = 2000;
    private const int REVEAL_BASE = 0;
    private const int REVEAL_STRUCTURE = 1;
    private const int REVEAL_LABEL = 2;
    private const int REVEAL_INFLAMMATION = 3;

    //public TMP_Text patientDialogue;
    //public TMP_Text pressureValue;
    //public TMP_Text frequencyValue;
    public Results results;
    public WandDisplay wand;
    public List<AudioSource> painAudioSources;
    public GameObject patientBase;
    public List<GameObject> revealInflammations;
    public Sprite revealStructureSprite;
    public Sprite revealLabelSprite;
    public List<Sprite> revealInflammationSprites;

    private int randomSetup;
    private int currentReveal = REVEAL_BASE;
    private bool soundPlaying = false;

    public GameObject markerBlobPrefab;
    public GameObject gelBlobPrefab;
    public Treatment treatment;
    public Inventory inventory;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        generateTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        RaycastHit2D raycastHit0 = Physics2D.Raycast(worldPosition, Vector2.zero);
        if (raycastHit0.collider != null
            && raycastHit0.transform.parent != null
            && raycastHit0.transform.parent.tag.Equals("target"))
        {
            if (PlayerPrefs.GetString("currentTool").Equals("wand")
                && wand.IsWandOn())
            {
                inventory.HighlightCurrentTool();
            } else if (!PlayerPrefs.GetString("currentTool").Equals("wand"))
            {
                inventory.HighlightCurrentTool();
            } else
            {
                inventory.DimCurrentTool();
            }
        } else
        {
            inventory.DimCurrentTool();
        }

        if (Input.GetMouseButtonDown(0))
        {
            float distance = (new Vector2(transform.position.x, transform.position.y) - worldPosition).magnitude;
            float horizontalDistance = Mathf.Abs(transform.position.x - worldPosition.x);
            float verticalDistance = Mathf.Abs(transform.position.y - worldPosition.y);
            RaycastHit2D[] raycastHits = Physics2D.RaycastAll(worldPosition, Vector2.zero);
            if (PlayerPrefs.GetString("currentTool").Equals("glove"))
            {
                if (distance <= 0.8f)
                {
                    PlayPainSound(1.0f - distance);

                    if (distance <= 0.4f)
                    {
                        treatment.triggerTargetCorrect();
                    }
                    else
                    {
                        treatment.triggerTargetAlmostCorrect();
                    }
                } else
                {
                    RaycastHit2D raycastHit = Physics2D.Raycast(worldPosition, Vector2.zero);
                    if (raycastHit.collider != null
                        && raycastHit.transform.parent != null
                        && raycastHit.transform.parent.tag.Equals("target"))
                    {
                        if (horizontalDistance >= verticalDistance)
                        {
                            if (transform.position.x < worldPosition.x)
                            {
                                treatment.triggerTargetLeft();
                            }
                            else
                            {
                                treatment.triggerTargetRight();
                            }
                        }
                        else if (horizontalDistance < verticalDistance)
                        {
                            if (transform.position.y < worldPosition.y)
                            {
                                treatment.triggerTargetDown();
                            }
                            else
                            {
                                treatment.triggerTargetUp();
                            }
                        }
                    }
                }
            }
            else if (PlayerPrefs.GetString("currentTool").Equals("wand"))
            {
                if (wand.IsWandOn())
                {
                    foreach (RaycastHit2D raycastHit in raycastHits)
                    {
                        if (raycastHit.collider != null
                            && raycastHit.transform.parent != null
                            && raycastHit.transform.parent.tag.Equals("target"))
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
            else if (PlayerPrefs.GetString("currentTool").Equals("marker"))
            {
                bool onGel = false;
                foreach (RaycastHit2D raycastHit in raycastHits)
                {
                    if (raycastHit.collider != null
                        && raycastHit.transform.tag.Equals("gelBlob"))
                    {
                        onGel = true;
                    }
                }
                if (!onGel)
                {
                    foreach (RaycastHit2D raycastHit in raycastHits)
                    {
                        if (raycastHit.collider != null
                            && raycastHit.transform.parent != null
                            && raycastHit.transform.parent.tag.Equals("target"))
                        {
                            Instantiate(markerBlobPrefab, worldPosition, Quaternion.identity);
                            break;
                        }
                    }
                }
            }
            else if (PlayerPrefs.GetString("currentTool").Equals("gel"))
            {
                foreach (RaycastHit2D raycastHit in raycastHits)
                {
                    if (raycastHit.collider != null
                        && raycastHit.transform.parent != null
                        && raycastHit.transform.parent.tag.Equals("target"))
                    {
                        Instantiate(gelBlobPrefab, worldPosition, Quaternion.identity);
                        results.SetGelPosition(worldPosition);
                        break;
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
            AudioSource audioSource;
            if (PlayerPrefs.GetInt("patient") == 0)
            {
                audioSource = painAudioSources[Random.Range(0, 3)];
            }
            else
            {
                audioSource = painAudioSources[Random.Range(3, 6)];
            }
            
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

    public void generateTarget()
    {
        randomSetup = PlayerPrefs.GetInt("target");
        if (randomSetup == 0)
        {
            if (PlayerPrefs.GetInt("patient") == 0)
            {
                transform.position = new Vector2(-18.13f, 10.37f);
            }
            else
            {
                transform.position = new Vector2(-44f, -5f);
            }
        }
        else if (randomSetup == 1)
        {
            if (PlayerPrefs.GetInt("patient") == 0)
            {
                transform.position = new Vector2(-15.5f, 9.0f);
            }
            else
            {
                transform.position = new Vector2(-44.5f, -3.5f);
            }
        }
        else if (randomSetup == 2)
        {
            if (PlayerPrefs.GetInt("patient") == 0)
            {
                transform.position = new Vector2(-17.13f, 9.5f);
            }
            else
            {
                transform.position = new Vector2(-38f, -5f);
            }
        }
        results.SetTargetPosition(transform.position);
    }

    public void resetWand()
    {
        wand.resetWand();
    }

}
