using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Results : MonoBehaviour
{

    private readonly Color COLOR_GREEN = new Color(0.0f, 0.68f, 0.26f);
    private readonly Color COLOR_YELLOW = new Color(1.0f, 0.87f, 0.0f);
    private readonly Color COLOR_RED = new Color(0.93f, 0.18f, 0.22f);

    private bool usedGel = false;
    private Vector2 gelPosition;
    private bool correctPressure = false;
    private bool correctFrequency = false;
    private bool correctShocks = false;
    private Vector2 targetPosition;
    private float totalMagnitude = 0.0f;
    private int numMagnitude = 0;

    public GameObject minigameResults;
    public GameObject minigameCIResults;
    public Image gelCorrectIcon;
    public Image pressureCorrectIcon;
    public Image frequencyCorrectIcon;
    public Image shocksCorrectIcon;
    public Image locationCorrectIcon;
    public Image CounterIndicationCorrectIcon;
    public Image ChooseCorrectCIIcon;
    public TreatTarget target;
    public WandDisplay wand;
    public Inventory toolSelect;
    public GameObject finishButton;
    public GameObject patientChartButton;
    public GameObject chooseCI;
    public TextMeshProUGUI correctCItext;
    private bool chooseCorrect;
    private string counterIndication;

    // Start is called before the first frame update
    void Start()
    {
        finishButton.SetActive(true);
        patientChartButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void SetTargetPosition(Vector2 position)
    {
        targetPosition = position;
    }

    public void DisplayResults()
    {
        if (PlayerPrefs.GetInt("hasCounterIndication") == 1) {
            DisplayCIResult("");
            return;
        }
        finishButton.SetActive(false);
        patientChartButton.SetActive(false);
        wand.TurnOffWand();
        target.SetMachineResults();
        toolSelect.DisableTools();

        minigameResults.SetActive(true);
        if (usedGel)
        {
            float magnitude = (targetPosition - gelPosition).magnitude;
            if (magnitude <= 0.4f)
            {
                gelCorrectIcon.color = COLOR_GREEN;
            }
            else if (magnitude <= 0.8f)
            {
                gelCorrectIcon.color = COLOR_YELLOW;
            }
            else
            {
                gelCorrectIcon.color = COLOR_RED;
            }
        }
        else
        {
            gelCorrectIcon.color = COLOR_RED;
        }
        if (correctPressure)
        {
            pressureCorrectIcon.color = COLOR_GREEN;
        }
        else
        {
            pressureCorrectIcon.color = COLOR_RED;
        }
        if (correctFrequency)
        {
            frequencyCorrectIcon.color = COLOR_GREEN;
        }
        else
        {
            frequencyCorrectIcon.color = COLOR_RED;
        }
        if (correctShocks)
        {
            shocksCorrectIcon.color = COLOR_GREEN;
        }
        else
        {
            shocksCorrectIcon.color = COLOR_RED;
        }
        float averageMagnitude = 1.0f;
        if (numMagnitude > 0)
        {
            averageMagnitude = totalMagnitude / numMagnitude;
        }
        if (averageMagnitude <= 0.4f)
        {
            locationCorrectIcon.color = COLOR_GREEN;
        }
        else if (averageMagnitude <= 0.8f)
        {
            locationCorrectIcon.color = COLOR_YELLOW;
        }
        else
        {
            locationCorrectIcon.color = COLOR_RED;
        }

    }

    public void DisplayCIResult(string choice)
    {
        finishButton.SetActive(false);
        patientChartButton.SetActive(false);
        wand.TurnOffWand();
        target.SetMachineResults();
        toolSelect.DisableTools();

        minigameCIResults.SetActive(true);
        if (PlayerPrefs.GetInt("hasCounterIndication") == 1)
        {
            if (PlayerPrefs.GetInt("treatedForAWhile") == 1)
            {
                CounterIndicationCorrectIcon.color = COLOR_RED;
            }
            else if (PlayerPrefs.GetInt("treated") == 1)
            {
                CounterIndicationCorrectIcon.color = COLOR_YELLOW;
            }
            else
            {
                CounterIndicationCorrectIcon.color = COLOR_GREEN;
            }
            correctCItext.text = "The correct Counter Indication is: "
                + counterIndication;
            if (chooseCorrect)
            {
                ChooseCorrectCIIcon.color = COLOR_GREEN;
            } else
            {
                ChooseCorrectCIIcon.color = COLOR_RED;
            }

        } else
        {
            correctCItext.text = choice
                + " is not a Counter Indication.";
            CounterIndicationCorrectIcon.color = COLOR_RED;
            ChooseCorrectCIIcon.color = COLOR_RED;
        }
        
    }

    public void OnDestroy()
    {
        
    }

    public void ContinueQuiz()
    {
        GameManager.currentQuestion += 1;
        SceneManager.LoadScene("Quiz");
    }

    public void SetGelPosition(Vector2 position)
    {
        usedGel = true;
        gelPosition = position;
    }

    public void SetCorrectPressureTrue()
    {
        correctPressure = true;
    }

    public void SetCorrectFrequencyTrue()
    {
        correctFrequency = true;
    }

    public void SetCorrectShocksTrue()
    {
        correctShocks = true;
    }

    public void AddProbePosition(Vector2 position)
    {
        totalMagnitude += (targetPosition - position).magnitude;
        numMagnitude += 1;
        PlayerPrefs.SetInt("treated", 1);
        if (numMagnitude == 5)
        {
            PlayerPrefs.SetInt("treatedForAWhile", 1);
        }
    }

    public void closeResult()
    {
        usedGel = false;
        correctPressure = false;
        correctFrequency = false;
        correctShocks = false;
        totalMagnitude = 0.0f;
        numMagnitude = 0;

        minigameResults.SetActive(false);
        minigameCIResults.SetActive(false);

        finishButton.SetActive(true);
        patientChartButton.SetActive(true);
    }

    public void setCounterIndication(string CI)
    {
        counterIndication = CI;
    }

    public void setChooseCorrect(bool correct)
    {
        chooseCorrect = correct;
    }

}
