using AvasureQuizApp.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AvasureQuizApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            StartQuiz();
        }

        private static void StartQuiz()
        {
            //Read in the .txt file, build out question & answer pairs.
            List<Question> questions = ReadQuestionsFile(4);

            //Loop through the questions, record responses.
            PresentQuestion(questions);

            ////Present an overall score when the last question has been answered.
            //DisplayScore();
        }

        //private static void DisplayScore()
        //{
        //    throw new NotImplementedException();
        //}

        private static void PresentQuestion(List<Question> questions)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"({questions[i].QuestionNumber.ToString()}) {questions[i].QuestionText}");

                for (int j = 0; j < questions[i].Answers.Count; j++)
                {
                    Console.WriteLine($"{questions[i].Answers[j].AnswerNumber}. {questions[i].Answers[j].AnswerText}");
                }

                //TODO: Handle user response here. Check for valid response. If not, let the user retry.
                string answer = Console.ReadLine();
            }
        }

        //TODO: Error handling
        //TODO: Unit tests

        public static List<Question> ReadQuestionsFile(int possibleAnswers)
        {
            List<Question> questions = new List<Question>();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\QuizQuestions.txt");

            using (StreamReader sr = new StreamReader(path))
            {
                String line;

                while ((line = sr.ReadLine()) != null)
                {
                    //Check for the Question text
                    if (line.StartsWith('('))
                    {
                        Question newFoundQuestion = new Question();
                        newFoundQuestion.Answers = new List<Answer>();
                        string[] splitLine;

                        //Split on the closed parthesis character.
                        splitLine = line.Split(')');

                        //Set the question number.
                        int questionNumber;

                        //Remove the open parthesis character.
                        splitLine[0] = splitLine[0].Remove(0, 1);
                        int.TryParse(splitLine[0], out questionNumber);
                        newFoundQuestion.QuestionNumber = questionNumber;

                        //Set the question text.
                        newFoundQuestion.QuestionText = splitLine[1].TrimStart();

                        //Read the next (possibleAnswers) lines - 4 responses & correct answer for this exercise, but can support other values.
                        for (int i = 0; i < possibleAnswers; i++)
                        {
                            //Possible response line
                            line = sr.ReadLine();
                            splitLine = line.Split('.');

                            Answer newAnswer = new Answer();

                            int responseNumber;
                            int.TryParse(splitLine[0], out responseNumber);
                            newAnswer.AnswerNumber = responseNumber;
                            newAnswer.AnswerText = splitLine[1].TrimStart();

                            newFoundQuestion.Answers.Add(newAnswer);
                        }

                        //TODO: Refactor into seperate method?
                        //Correct answer line
                        line = sr.ReadLine();
                        int correctAnswer;
                        int.TryParse(line, out correctAnswer);

                        //Set the correctAnswer flag on the correct answer object.
                        Answer correctAnswerObj = newFoundQuestion.Answers.Where(x => x.AnswerNumber == correctAnswer).FirstOrDefault();
                        correctAnswerObj.IsCorrectAnswer = true;

                        //TODO: Update the item in the List.
                        int index = newFoundQuestion.Answers.FindIndex(x => x == correctAnswerObj);
                        newFoundQuestion.Answers[index] = correctAnswerObj;

                        questions.Add(newFoundQuestion);
                    }
                }
            }

            return questions;
        }
    }
}
