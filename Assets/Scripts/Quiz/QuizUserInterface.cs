using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizUserInterface : MonoBehaviour
{

    private readonly Color COLOR_GREEN = new Color(0.0f, 0.68f, 0.26f);
    private readonly Color COLOR_YELLOW = new Color(1.0f, 0.87f, 0.0f);
    private readonly Color COLOR_RED = new Color(0.93f, 0.18f, 0.22f);

    // questions
    public TMP_Text questionText;
    public TMP_Text answerText1;
    public TMP_Text answerText2;
    public TMP_Text answerText3;
    public TMP_Text answerText4;
    public GameObject clipboard;
    public TMP_Text feedbackText;
    public GameObject continueButton;
    public GameObject fadePanel;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    // results
    public GameObject results;
    public TMP_Text resultsQuestion;
    public TMP_Text description1;
    public TMP_Text description2;
    public TMP_Text description3;
    public TMP_Text description4;
    public TMP_Text feedback1;
    public TMP_Text feedback2;
    public TMP_Text feedback3;
    public TMP_Text feedback4;
    public Image colorCode1;
    public Image colorCode2;
    public Image colorCode3;
    public Image colorCode4;
    public TMP_Text count2;
    public TMP_Text count3;
    public TMP_Text count4;

    private List<QuizQuestion> questions = new List<QuizQuestion>();
    private QuizQuestion question;
    private int answer;
    private bool[] multipleAnswers = new bool[12];
    private int resultsIndex = 0;

    private void Awake()
    {
        QuizAnswer question1Answer1 = new QuizAnswer("make light conversation", "Starting with small talk will help the patient become more comfortable and confirm to the patient that their personal privacy is valued and protected.", 0);
        QuizAnswer question1Answer2 = new QuizAnswer("instruct patient on which room to go to", "It is better to start by building rapport with your patient in order to help them more easily communicate their needs and rehabilitaiton wants with you.", 1);
        QuizAnswer question1Answer3 = new QuizAnswer("ask a relevant medical question", "Discussing the patient's health information out in the open is a breach of their privacy and poor professional etiquette.", 2);
        questions.Add(new QuizQuestion("How do you greet the patient?", new List<QuizAnswer>() { question1Answer1, question1Answer2, question1Answer3 }));
        QuizAnswer question2Answer1 = new QuizAnswer("assess location and current quality of pain", "Assessing the location and quality of pain is the right action in this case.", 0);
        QuizAnswer question2Answer2 = new QuizAnswer("let the patient know that is expected and continue with treatment", "You should not simply ignore the pain.", 1);
        QuizAnswer question2Answer3 = new QuizAnswer("express concern about long healing time", "This would just create more concern and confusion.", 2);
        questions.Add(new QuizQuestion("How do you respond to the patient's mention of leg pain?", new List<QuizAnswer>() { question2Answer1, question2Answer2, question2Answer3 }));
        QuizAnswer question3Answer1 = new QuizAnswer("look at the results right away", "The results could be relevant to the treatment plan.", 0);
        QuizAnswer question3Answer2 = new QuizAnswer("look at the results after the treatment", "The results could be relevant to the treatment plan so it should be reviewed as soon as possible.", 1);
        QuizAnswer question3Answer3 = new QuizAnswer("disregard the results since it is not relevant for this session", "Collaboration with other medical professionals is an important component of sound patient care. The ultrasound report may provide new information that might alter your treatment plan.", 2);
        questions.Add(new QuizQuestion("What should you do with the patient's ultrasound results?", new List<QuizAnswer>() { question3Answer1, question3Answer2, question3Answer3 }));
        QuizAnswer question4Answer1 = new QuizAnswer("suggest shockwave therapy", "Shockwave therapy is a good candidate in this case.", 0);
        QuizAnswer question4Answer2 = new QuizAnswer("suggest more ultrasound treatment", "The patient's history and imaging results suggest a better option.", 1);
        QuizAnswer question4Answer3 = new QuizAnswer("suggest usual treatment", "There are other options in this scenario.", 2);
        QuizAnswer question4Answer4 = new QuizAnswer("decide to use shockwave therapy", "This may be the right choice but the patient should be be given details and provide consent.", 2);
        questions.Add(new QuizQuestion("What would you suggest as a treatment for the calcification of the achillies tendon?", new List<QuizAnswer>() { question4Answer1, question4Answer2, question4Answer3, question4Answer4}));
        QuizAnswer question5Answer1 = new QuizAnswer("patient's history and imaging results", "The patient's history and imaging results are why shockwave therapy should be used.", 0);
        QuizAnswer question5Answer2 = new QuizAnswer("easy and cheap", "Easy and cheap methods are desirable but not the primary reason to choose a treatment method.", 2);
        questions.Add(new QuizQuestion("Why is shockwave therapy appropriate here?", new List<QuizAnswer>() { question5Answer1, question5Answer2 }));
        QuizAnswer question6Answer1 = new QuizAnswer("check wire connections", "The problem should not be ignored and the wires are likely the source of the problem.", 0);
        QuizAnswer question6Answer2 = new QuizAnswer("use anyways since it should not be a problem", "The machine not working should be addressed.", 2);
        questions.Add(new QuizQuestion("There is a problem with the shockwave therapy machine, what do you do?", new List<QuizAnswer>() { question6Answer1, question6Answer2 }));
        QuizAnswer question7Answer1 = new QuizAnswer("lying prone", "This position is the best for the treatment.", 0);
        QuizAnswer question7Answer2 = new QuizAnswer("lying supine", "This would not be the idea position for the treatment.", 1);
        QuizAnswer question7Answer3 = new QuizAnswer("sitting upright", "This would not be a good position for the treatment.", 2);
        questions.Add(new QuizQuestion("Treatment is about to start, what position should the patient be in?", new List<QuizAnswer>() { question7Answer1, question7Answer2, question7Answer3 }));
        questions.Add(new QuizQuestion("Minigame Section", new List<QuizAnswer>() { }));
        QuizAnswer question9Answer1 = new QuizAnswer("discuss importance of pain assessments with patient", "It is important for the patient to understand the treatment process and why you may or may not change the intensity.", 0);
        QuizAnswer question9Answer2 = new QuizAnswer("keep the intensity where it is and continue", "This may be appropriate but it is important for the patient to understand the treatment process.", 1);
        QuizAnswer question9Answer3 = new QuizAnswer("increase the intensity in response", "Higher intensity is not always beneficial for treatment.", 2);
        questions.Add(new QuizQuestion("When assessing the patient's tolerance, the patient asks you to crank it up. What should you do?", new List<QuizAnswer>() { question9Answer1, question9Answer2, question9Answer3 }));
        QuizAnswer question10Answer1 = new QuizAnswer("assess treatment site and give care instructions to patient", "It is important to finish with an assessment and give care instructions to the patient.", 0);
        QuizAnswer question10Answer2 = new QuizAnswer("reassure the patient that next treatment will have lower intensity", "Redness is an expected side effect of shockwave therapy.", 1);
        QuizAnswer question10Answer3 = new QuizAnswer("swiftly wrap up session and prepare for next patient", "You should follow up on the pain and next steps as well as allow the patient to ask questions.", 2);
        questions.Add(new QuizQuestion("As you are wrapping up the treatment, you notice redness on the treatment area. What should you do?", new List<QuizAnswer>() { question10Answer1, question10Answer2, question10Answer3 }));
        colorCode1.color = COLOR_GREEN;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.currentQuestion == questions.Count)
        {
            DisplayResults();
        }
        else
        {
            question = questions[GameManager.currentQuestion];
            DisplayQuestion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("ShockwaveMinigame");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisplayResults();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.currentQuestion = 8;
        }
    }

    public void SubmitAnswer(int answerIndex)
    {
        answer = answerIndex;
        QuizAnswer quizAnswer = question.answers[answerIndex];
        int correctIndex = quizAnswer.correctIndex;
        string feedback = quizAnswer.feedback;
        if (correctIndex == 0)
        {
            feedbackText.text = "CORRECT: " + feedback;
        }
        else if (correctIndex == 1)
        {
            feedbackText.text = "PARTIALLY CORRECT: " + feedback;
        }
        else
        {
            feedbackText.text = "INCORRECT: " + feedback;
        }
        if (answerIndex == 1)
        {
            QuizResults.numAnswer2List[GameManager.currentQuestion] += 1;
        }
        else if (answerIndex == 2)
        {
            QuizResults.numAnswer3List[GameManager.currentQuestion] += 1;
        }
        else if (answerIndex == 3)
        {
            QuizResults.numAnswer4List[GameManager.currentQuestion] += 1;
        }
        clipboard.SetActive(true);
        continueButton.SetActive(true);
        fadePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        if (answer == 0)
        {
            GameManager.videoIndex = 1;
        }
        else if (answer == 1)
        {
            GameManager.videoIndex = 2;
        }
        else if (answer == 2)
        {
            GameManager.videoIndex = 3;
        }
        else
        {
            GameManager.videoIndex = 4;
        }
        SceneManager.LoadScene("Video");
    }

    private void DisplayQuestion()
    {
        questionText.text = question.question;
        int questionSize = question.answers.Count;
        if (questionSize == 2)
        {
            answerText1.text = question.answers[0].answer;
            answerText2.text = question.answers[1].answer;
            button3.SetActive(false);
            button4.SetActive(false);
        }
        else if (questionSize == 3)
        {
            answerText1.text = question.answers[0].answer;
            answerText2.text = question.answers[1].answer;
            answerText3.text = question.answers[2].answer;
            button3.SetActive(true);
            button4.SetActive(false);
        }
        else if (questionSize == 4)
        {
            answerText1.text = question.answers[0].answer;
            answerText2.text = question.answers[1].answer;
            answerText3.text = question.answers[2].answer;
            answerText4.text = question.answers[3].answer;
            button3.SetActive(true);
            button4.SetActive(true);
        }
        int setup = Random.Range(0, 24);
        Vector2 position1 = new Vector2(-350.0f, 0.0f);
        Vector2 position2 = new Vector2(350.0f, 0.0f);
        Vector2 position3 = new Vector2(-350.0f, -250.0f);
        Vector2 position4 = new Vector2(350.0f, -250.0f);
        if (setup == 0)
        {
            button1.transform.localPosition = position1;
            button2.transform.localPosition = position2;
            button3.transform.localPosition = position3;
            button4.transform.localPosition = position4;
        }
        else if (setup == 1)
        {
            button1.transform.localPosition = position1;
            button2.transform.localPosition = position2;
            button3.transform.localPosition = position4;
            button4.transform.localPosition = position3;
        }
        else if (setup == 2)
        {
            button1.transform.localPosition = position1;
            button2.transform.localPosition = position3;
            button3.transform.localPosition = position2;
            button4.transform.localPosition = position4;
        }
        else if (setup == 3)
        {
            button1.transform.localPosition = position1;
            button2.transform.localPosition = position3;
            button3.transform.localPosition = position4;
            button4.transform.localPosition = position2;
        }
        else if (setup == 4)
        {
            button1.transform.localPosition = position1;
            button2.transform.localPosition = position4;
            button3.transform.localPosition = position2;
            button4.transform.localPosition = position3;
        }
        else if (setup == 5)
        {
            button1.transform.localPosition = position1;
            button2.transform.localPosition = position4;
            button3.transform.localPosition = position3;
            button4.transform.localPosition = position2;
        }
        else if (setup == 6)
        {
            button1.transform.localPosition = position2;
            button2.transform.localPosition = position1;
            button3.transform.localPosition = position3;
            button4.transform.localPosition = position4;
        }
        else if (setup == 7)
        {
            button1.transform.localPosition = position2;
            button2.transform.localPosition = position1;
            button3.transform.localPosition = position4;
            button4.transform.localPosition = position3;
        }
        else if (setup == 8)
        {
            button1.transform.localPosition = position2;
            button2.transform.localPosition = position3;
            button3.transform.localPosition = position1;
            button4.transform.localPosition = position4;
        }
        else if (setup == 9)
        {
            button1.transform.localPosition = position2;
            button2.transform.localPosition = position3;
            button3.transform.localPosition = position4;
            button4.transform.localPosition = position1;
        }
        else if (setup == 10)
        {
            button1.transform.localPosition = position2;
            button2.transform.localPosition = position4;
            button3.transform.localPosition = position1;
            button4.transform.localPosition = position3;
        }
        else if (setup == 11)
        {
            button1.transform.localPosition = position2;
            button2.transform.localPosition = position4;
            button3.transform.localPosition = position3;
            button4.transform.localPosition = position1;
        }
        else if (setup == 12)
        {
            button1.transform.localPosition = position3;
            button2.transform.localPosition = position1;
            button3.transform.localPosition = position2;
            button4.transform.localPosition = position4;
        }
        else if (setup == 13)
        {
            button1.transform.localPosition = position3;
            button2.transform.localPosition = position1;
            button3.transform.localPosition = position4;
            button4.transform.localPosition = position2;
        }
        else if (setup == 14)
        {
            button1.transform.localPosition = position3;
            button2.transform.localPosition = position2;
            button3.transform.localPosition = position1;
            button4.transform.localPosition = position4;
        }
        else if (setup == 15)
        {
            button1.transform.localPosition = position3;
            button2.transform.localPosition = position2;
            button3.transform.localPosition = position4;
            button4.transform.localPosition = position1;
        }
        else if (setup == 16)
        {
            button1.transform.localPosition = position3;
            button2.transform.localPosition = position4;
            button3.transform.localPosition = position1;
            button4.transform.localPosition = position2;
        }
        else if (setup == 17)
        {
            button1.transform.localPosition = position3;
            button2.transform.localPosition = position4;
            button3.transform.localPosition = position2;
            button4.transform.localPosition = position1;
        }
        else if (setup == 18)
        {
            button1.transform.localPosition = position4;
            button2.transform.localPosition = position1;
            button3.transform.localPosition = position2;
            button4.transform.localPosition = position3;
        }
        else if (setup == 19)
        {
            button1.transform.localPosition = position4;
            button2.transform.localPosition = position1;
            button3.transform.localPosition = position3;
            button4.transform.localPosition = position2;
        }
        else if (setup == 20)
        {
            button1.transform.localPosition = position4;
            button2.transform.localPosition = position2;
            button3.transform.localPosition = position1;
            button4.transform.localPosition = position3;
        }
        else if (setup == 21)
        {
            button1.transform.localPosition = position4;
            button2.transform.localPosition = position2;
            button3.transform.localPosition = position3;
            button4.transform.localPosition = position1;
        }
        else if (setup == 22)
        {
            button1.transform.localPosition = position4;
            button2.transform.localPosition = position3;
            button3.transform.localPosition = position1;
            button4.transform.localPosition = position2;
        }
        else 
        {
            button1.transform.localPosition = position4;
            button2.transform.localPosition = position3;
            button3.transform.localPosition = position2;
            button4.transform.localPosition = position1;
        }
    }

    public void DisplayResults()
    {
        questionText.transform.parent.gameObject.SetActive(false);
        clipboard.SetActive(false);
        continueButton.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        results.SetActive(true);
        DisplayNextResult();
    }

    private void DisplayNextResult()
    {
        QuizQuestion currentQuestion = questions[resultsIndex];
        int questionSize = currentQuestion.answers.Count;
        QuizAnswer currentAnswer1 = null;
        QuizAnswer currentAnswer2 = null;
        QuizAnswer currentAnswer3 = null;
        QuizAnswer currentAnswer4 = null;
        if (questionSize == 2)
        {
            currentAnswer1 = currentQuestion.answers[0];
            currentAnswer2 = currentQuestion.answers[1];
            description3.text = "";
            feedback3.text = "";
            colorCode3.gameObject.SetActive(false);
            count3.text = "";
            description4.text = "";
            feedback4.text = "";
            colorCode4.gameObject.SetActive(false);
            count4.text = "";
        }
        else if (questionSize == 3)
        {
            currentAnswer1 = currentQuestion.answers[0];
            currentAnswer2 = currentQuestion.answers[1];
            currentAnswer3 = currentQuestion.answers[2];
            colorCode3.gameObject.SetActive(true);
            description4.text = "";
            feedback4.text = "";
            colorCode4.gameObject.SetActive(false);
            count4.text = "";
        }
        else if (questionSize == 4)
        {
            currentAnswer1 = currentQuestion.answers[0];
            currentAnswer2 = currentQuestion.answers[1];
            currentAnswer3 = currentQuestion.answers[2];
            currentAnswer4 = currentQuestion.answers[3];
            colorCode3.gameObject.SetActive(true);
            colorCode4.gameObject.SetActive(true);
        }
        resultsQuestion.text = currentQuestion.question;
        description1.text = currentAnswer1.answer;
        feedback1.text = currentAnswer1.feedback;
        description2.text = currentAnswer2.answer;
        feedback2.text = currentAnswer2.feedback;
        if (currentAnswer2.correctIndex == 1)
        {
            colorCode2.color = COLOR_YELLOW;
        }
        else if (currentAnswer2.correctIndex == 2)
        {
            colorCode2.color = COLOR_RED;
        }
        count2.text = QuizResults.numAnswer2List[resultsIndex].ToString();
        if (currentAnswer3 != null)
        {
            description3.text = currentAnswer3.answer;
            feedback3.text = currentAnswer3.feedback;
            if (currentAnswer3.correctIndex == 1)
            {
                colorCode3.color = COLOR_YELLOW;
            }
            else if (currentAnswer3.correctIndex == 2)
            {
                colorCode3.color = COLOR_RED;
            }
            count3.text = QuizResults.numAnswer3List[resultsIndex].ToString();
        }
        if (currentAnswer4 != null)
        {
            description4.text = currentAnswer4.answer;
            feedback4.text = currentAnswer4.feedback;
            if (currentAnswer4.correctIndex == 1)
            {
                colorCode4.color = COLOR_YELLOW;
            }
            else if (currentAnswer4.correctIndex == 2)
            {
                colorCode4.color = COLOR_RED;
            }
            count4.text = QuizResults.numAnswer4List[resultsIndex].ToString();
        }
    }

    public void AdvanceResults()
    {
        resultsIndex += 1;
        if (resultsIndex == 7)
        {
            resultsIndex += 1;
            DisplayNextResult();
        }
        else if (resultsIndex == questions.Count)
        {
            SceneManager.LoadScene("End");
        }
        else
        {
            DisplayNextResult();
        }
    }

}
