using System;
using System.Collections.Generic;
using System.Text;

namespace AvasureQuizApp.DataModels
{
    public class Question
    {
        public int QuestionNumber { get; set; }

        public string QuestionText { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
