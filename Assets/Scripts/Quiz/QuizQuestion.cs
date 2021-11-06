using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizQuestion
{

    public string question;
    public List<QuizAnswer> answers;

    public QuizQuestion(string question, List<QuizAnswer> answers)
    {
        this.question = question;
        this.answers = answers;
    }

}
