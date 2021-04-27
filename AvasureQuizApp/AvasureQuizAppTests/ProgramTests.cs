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
        public void DisplayScoreTest_OneCorrect()
        {
            string expectedScore = "You got 1 out of 2 questions correct!";
            string actualScore = Program.DisplayScore(1, 2);

            Assert.AreEqual(expectedScore, actualScore);
        }

        [TestMethod()]
        public void PresentQuestionsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ReadQuestionsFileTest_ValidFile()
        {
            List<Question> questions = new List<Question>() { 
                new Question { 
                    QuestionNumber = 1, 
                    QuestionText = "The National Basketball Association is made up of how many teams?", 
                    Answers = new List<Answer> { 
                        new Answer { AnswerNumber = 1, AnswerText = "35", IsCorrectAnswer = false },
                        new Answer { AnswerNumber = 2, AnswerText = "30", IsCorrectAnswer = true },
                        new Answer { AnswerNumber = 3, AnswerText = "20", IsCorrectAnswer = false },
                        new Answer { AnswerNumber = 4, AnswerText = "32", IsCorrectAnswer = false },
                    } 
                },
                new Question {
                    QuestionNumber = 2,
                    QuestionText = "What score is considered a \"Perfect Game\" in the sport of Bowling?",
                    Answers = new List<Answer> {
                        new Answer { AnswerNumber = 1, AnswerText = "400", IsCorrectAnswer = false },
                        new Answer { AnswerNumber = 2, AnswerText = "100", IsCorrectAnswer = false },
                        new Answer { AnswerNumber = 3, AnswerText = "300", IsCorrectAnswer = true },
                        new Answer { AnswerNumber = 4, AnswerText = "200", IsCorrectAnswer = false },
                    }
                }
            };

            List<Question> expectedQuestions = Program.ReadQuestionsFile(4, "QuizQuestions");
            Assert.AreEqual(questions.Count, expectedQuestions.Count);
        }

        [TestMethod()]
        public void ReadQuestionsFileTest_InvalidFile()
        {
            List<Question> questions = new List<Question>() { };
            List<Question> expectedQuestions = Program.ReadQuestionsFile(4, "InvalidQuizQuestions");

            Assert.AreEqual(expectedQuestions.Count, questions.Count);
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