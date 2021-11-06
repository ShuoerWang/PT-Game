using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizAnswer
{

    public string answer;
    public string feedback;
    public int correctIndex;

    public QuizAnswer(string answer, string feedback, int correctIndex)
    {
        this.answer = answer;
        this.feedback = feedback;
        this.correctIndex = correctIndex;
    }

}
