using Microsoft.VisualStudio.TestTools.UnitTesting;
using AvasureQuizApp;
using System;
using System.Collections.Generic;
using System.Text;
using AvasureQuizApp.DataModels;

namespace AvasureQuizApp.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void StartQuizTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DisplayScoreTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PresentQuestionsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadQuestionsFileTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BuildAnswerObjectTest_ValidAnswerNumber()
        {
            string lineData = "1. 300";
            Answer newAnswer = Program.BuildAnswerObject(lineData);
            Answer expectedAnswer = new Answer()
            {
                AnswerNumber = 1,
                AnswerText = "300",
            };

            Assert.AreEqual(newAnswer.AnswerNumber, expectedAnswer.AnswerNumber);
        }

        [TestMethod()]
        public void BuildAnswerObjectTest_ValidAnswerText()
        {
            string lineData = "1. 300";
            Answer newAnswer = Program.BuildAnswerObject(lineData);
            Answer expectedAnswer = new Answer()
            {
                AnswerNumber = 1,
                AnswerText = "300",
            };

            Assert.AreEqual(newAnswer.AnswerText, expectedAnswer.AnswerText);
        }

        [TestMethod()]
        public void BuildAnswerObjectTest_ValidCorrectAnswer()
        {
            string lineData = "1. 300";
            Answer newAnswer = Program.BuildAnswerObject(lineData);
            Answer expectedAnswer = new Answer()
            {
                AnswerNumber = 1,
                AnswerText = "300",
            };

            Assert.AreEqual(newAnswer.IsCorrectAnswer, expectedAnswer.IsCorrectAnswer);
        }

        [TestMethod()]
        public void GetCorrectAnswerTest_ValidAnswerObject()
        {
            string lineData = "3";
            List<Answer> answers = new List<Answer>() { };
            answers.Add(new Answer { AnswerNumber = 1, AnswerText = "300", IsCorrectAnswer = false });
            answers.Add(new Answer { AnswerNumber = 2, AnswerText = "200", IsCorrectAnswer = false });
            answers.Add(new Answer { AnswerNumber = 3, AnswerText = "400", IsCorrectAnswer = false });
            answers.Add(new Answer { AnswerNumber = 4, AnswerText = "100", IsCorrectAnswer = false });

            Answer expectedAnswer = Program.GetCorrectAnswer(lineData, answers);
            Assert.AreEqual(expectedAnswer.IsCorrectAnswer, true);
        }
    }
}