using System;
using System.Collections.Generic;
using System.Text;

namespace AvasureQuizApp.DataModels
{
    public class Answer
    {
        public int AnswerNumber { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrectAnswer { get; set; }
    }
}
