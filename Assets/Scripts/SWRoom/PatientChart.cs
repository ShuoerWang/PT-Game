using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PatientChart : MonoBehaviour
{

    public SWRoomManager sw;
    public GameObject patientChart;
    public GameObject inventory;
    public GameObject dialogueButton;

    public string[] commonMedicine;
    public string[] commonMedicalHis;
    public string[] tabooMedicine;
    public string[] tabooMedicalHis;

    public ChoiceDialogueNode CounterIndicationDialogue;
    public GameObject finishButton;
    public GameObject chartButton;

    public DialogueChannel dialogueChannel;

    private List<string> allMed;
    private string counterIndication;
    public Results results;


    public TextMeshProUGUI patientName;
    public TextMeshProUGUI medicalHis;
    public TextMeshProUGUI medicine;
    public TextMeshProUGUI symptoms;
    public TextMeshProUGUI cause;
    public TextMeshProUGUI history;
    public TextMeshProUGUI goal;
    public TextMeshProUGUI socialHis;

    public ChartComponent[] chartComponent;

    public RectTransform chartTarget;


    // Start is called before the first frame update
    void Start()
    {
        allMed = new List<string>();
        generatePatientInfo();
        modifyChoiceDialogueNode();
        counterIndication = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake ()
    {
        dialogueChannel.OnDialogueStart += onDialogueStart;
        dialogueChannel.OnDialogueEnd += onDialogueEnd;
        dialogueChannel.onChoiceMade += chooseCounterIndication;
    }

    void OnDestroy()
    {
        dialogueChannel.OnDialogueStart -= onDialogueStart;
        dialogueChannel.OnDialogueEnd -= onDialogueEnd;
        dialogueChannel.onChoiceMade -= chooseCounterIndication;
    }

    public void generatePatientInfo2()
    {
        patientName.text = chartComponent[PlayerPrefs.GetInt("patient")].patientName;
        symptoms.text = chartComponent[PlayerPrefs.GetInt("patient")].symptoms;
        cause.text = chartComponent[PlayerPrefs.GetInt("patient")].cause;
        history.text = chartComponent[PlayerPrefs.GetInt("patient")].history;
        goal.text = chartComponent[PlayerPrefs.GetInt("patient")].goal;
        socialHis.text = chartComponent[PlayerPrefs.GetInt("patient")].socialHis;


        if (PlayerPrefs.GetInt("hasCounterIndication") == 1)
        {
            int rand0 = Random.Range(0, 2);
            if (rand0 == 0)
            {
                int rand1 = Random.Range(0, tabooMedicine.Length);
                medicine.text = tabooMedicine[rand1];

                int rand2 = Random.Range(0, commonMedicalHis.Length);
                medicalHis.text = commonMedicalHis[rand2];

            } else
            {
                int rand1 = Random.Range(0, commonMedicine.Length);
                medicine.text = commonMedicine[rand1];

                int rand2 = Random.Range(0, tabooMedicalHis.Length);
                medicalHis.text = tabooMedicalHis[rand2];
            }  
        } else
        {
            int rand1 = Random.Range(0, commonMedicine.Length);
            medicine.text = commonMedicine[rand1];

            int rand2 = Random.Range(0, commonMedicalHis.Length);
            medicalHis.text = commonMedicalHis[rand2];
        }


    }

    public void generatePatientInfo()
    {
        patientName.text = chartComponent[PlayerPrefs.GetInt("patient")].patientName;
        symptoms.text = chartComponent[PlayerPrefs.GetInt("patient")].symptoms;
        cause.text = chartComponent[PlayerPrefs.GetInt("patient")].cause;
        history.text = chartComponent[PlayerPrefs.GetInt("patient")].history;
        goal.text = chartComponent[PlayerPrefs.GetInt("patient")].goal;

        allMed = new List<string>();

        int total = 0;
        string medicalHisText = "";
        bool hasCI = false;
        string medicineText = "";
        for (int i = 0; i < commonMedicine.Length; i++)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                total += 1;
                allMed.Add(commonMedicine[i]);
                medicineText += commonMedicine[i];
                medicineText += ", ";
            }
            if (total > 1)
            {
                break;
            }

            if (!hasCI && PlayerPrefs.GetInt("hasCounterIndication") == 1)
            {
                int rand2 = Random.Range(0, 4);
                if (rand2 == 0)
                {
                    hasCI = true;
                    int rand3 = Random.Range(0, tabooMedicine.Length);
                    total += 1;
                    allMed.Add(tabooMedicine[rand3]);
                    medicineText += tabooMedicine[rand3];
                    counterIndication = tabooMedicine[rand3];
                    medicineText += ", ";
                }
            }
            if (total > 1)
            {
                break;
            }
        }
        if (medicineText.Length > 1)
        {
            medicine.text = medicineText.Remove(medicineText.Length - 2, 2) + ".";
        }


        total = 0;

        for (int i = 0; i < commonMedicalHis.Length; i++)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                total += 1;
                allMed.Add(commonMedicalHis[i]);
                medicalHisText += commonMedicalHis[i];
                medicalHisText += ", ";
            }
            if (total > 1)
            {
                break;
            }

            if (!hasCI && PlayerPrefs.GetInt("hasCounterIndication") == 1)
            {
                int rand2 = Random.Range(0, 4);
                if (rand2 == 0)
                {
                    hasCI = true;
                    int rand3 = Random.Range(0, tabooMedicalHis.Length);
                    total += 1;
                    allMed.Add(tabooMedicalHis[rand3]);
                    medicalHisText += tabooMedicalHis[rand3];
                    counterIndication = tabooMedicalHis[rand3];
                    medicalHisText += ", ";
                }
            }
            if (total > 1)
            {
                break;
            }
        }
        if (!hasCI && PlayerPrefs.GetInt("hasCounterIndication") == 1)
        {
            int rand3 = Random.Range(0, tabooMedicalHis.Length);
            allMed.Add(tabooMedicalHis[rand3]);
            medicalHisText += tabooMedicalHis[rand3];
            counterIndication = tabooMedicalHis[rand3];
            medicalHisText += ", ";
        }

        if (medicalHisText.Length > 1)
        {
            medicalHis.text = medicalHisText.Remove(medicalHisText.Length - 2, 2) + ".";
        }

        int randomSetup = PlayerPrefs.GetInt("target");

        if (randomSetup == 0)
        {
            if (PlayerPrefs.GetInt("patient") == 0)
            {
                chartTarget.localPosition = new Vector2(246.5f, 105.87f);
            }
            else
            {
                chartTarget.localPosition = new Vector2(267.4f, 224.8f);
            }
        }
        else if (randomSetup == 1)
        {
            if (PlayerPrefs.GetInt("patient") == 0)
            {
                chartTarget.localPosition = new Vector2(250.9f, 124.5f);
            }
            else
            {
                chartTarget.localPosition = new Vector2(264.5f, 238f);
            }
        }
        else if (randomSetup == 2)
        {
            if (PlayerPrefs.GetInt("patient") == 0)
            {
                chartTarget.localPosition = new Vector2(248.5f, 114.8f);
            }
            else
            {
                chartTarget.localPosition = new Vector2(270.5f, 208.7f);
            }
        }
    }

    public void openPatientChart()
    {
        sw.disableArrows();
        patientChart.SetActive(true);
        inventory.SetActive(false);
        dialogueButton.SetActive(false);
        chartButton.SetActive(false);
        finishButton.SetActive(false);
    }

    public void closePatientChart()
    {
        sw.updateCanvas();
        patientChart.SetActive(false);
        inventory.SetActive(true);
        chartButton.SetActive(true);
        finishButton.SetActive(true);
    }

    public void chooseCounterIndication(string choice)
    {
        if (PlayerPrefs.GetInt("terminated") == 1)
        {
            gameObject.SetActive(false);

            results.setCounterIndication(counterIndication);
            results.setChooseCorrect(counterIndication.Equals(choice));

            results.DisplayCIResult(choice);
            inventory.SetActive(true);
        }
    }

    public void modifyChoiceDialogueNode()
    {
        CounterIndicationDialogue.Choices = new DialogueChoice[allMed.Count];
        for (int i = 0; i < allMed.Count; i++)
        {
            CounterIndicationDialogue.Choices[i] = new DialogueChoice();
            CounterIndicationDialogue.Choices[i].preview = allMed[i];
        }
    }

    public void triggerCIDialogue()
    {
        PlayerPrefs.SetInt("terminated", 1);
        dialogueChannel.RaiseDialogueNodeStart(CounterIndicationDialogue);
    }

    public void resetChart()
    {
        generatePatientInfo();
        modifyChoiceDialogueNode();
    }

    public void onDialogueStart(DialogueNode node)
    {
        chartButton.SetActive(false);
        finishButton.SetActive(false);
    }

    public void onDialogueEnd(DialogueNode node)
    {
        chartButton.SetActive(true);
        finishButton.SetActive(true);
    }
}
