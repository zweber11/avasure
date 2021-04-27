using AvasureQuizApp.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AvasureQuizApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            StartQuiz();
        }

        public static void StartQuiz()
        {
            //Read in the .txt file, build out question & answer pairs.
            List<Question> questions = ReadQuestionsFile(4, "QuizQuestions");

            //Loop through the questions, record responses.
            int correctAnswerCount = PresentQuestions(questions);

            //Present an overall score when the last question has been answered.
            string score = DisplayScore(correctAnswerCount, questions.Count);
            Console.ReadLine();
        }

        public static string DisplayScore(int correctAnswerCount, int questionCount)
        {
            string score = $"You got {correctAnswerCount.ToString()} out of {questionCount.ToString()} questions correct!";
            Console.WriteLine(score);

            return score;
        }

        public static int PresentQuestions(List<Question> questions)
        {
            int correctAnswerCount = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"({questions[i].QuestionNumber.ToString()}) {questions[i].QuestionText}");

                for (int j = 0; j < questions[i].Answers.Count; j++)
                {
                    Console.WriteLine($"{questions[i].Answers[j].AnswerNumber}. {questions[i].Answers[j].AnswerText}");
                }

                //Handle user response. Check for valid response. If not, let the user retry.
                string answer;
                int parsedAnswer;

                do
                {
                    answer = Console.ReadLine();
                    int.TryParse(answer, out parsedAnswer);

                    //Failed parse, or a response greater than the possible answers.
                    if (parsedAnswer == 0 || parsedAnswer > questions[0].Answers.Count)
                    {
                        Console.WriteLine($"Sorry - invalid response. Please try again.");
                    }
                    else
                    {
                        //Valid response - check if the user is correct.
                        Answer userAnswer = questions[i].Answers.Where(x => x.AnswerNumber == parsedAnswer).FirstOrDefault();
                        Answer correctAnswer = questions[i].Answers.Where(x => x.IsCorrectAnswer).FirstOrDefault();

                        if (userAnswer.IsCorrectAnswer)
                        {
                            //Correct answer.
                            Console.WriteLine($"Correct! The answer was: {correctAnswer.AnswerNumber}. {correctAnswer.AnswerText}");
                            correctAnswerCount++;
                        }
                        else
                        {
                            //Incorrect answer.
                            Console.WriteLine($"Sorry, the correct answer was: {correctAnswer.AnswerNumber}. {correctAnswer.AnswerText}");
                        }

                        break;
                    }
                } while (true);
            }

            return correctAnswerCount;
        }

        public static List<Question> ReadQuestionsFile(int possibleAnswers, string fileName)
        {
            List<Question> questions = new List<Question>();
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\" + fileName + ".txt");

            using (StreamReader sr = new StreamReader(path))
            {
                String line;

                while ((line = sr.ReadLine()) != null)
                {
                    //Check for the Question text
                    if (line.StartsWith('('))
                    {
                        Question newFoundQuestion = new Question() { Answers = new List<Answer>() };
                        string[] splitLine;

                        //Split on the closed parenthesis character.
                        splitLine = line.Split(')');
                        int questionNumber;

                        //Remove the open parenthesis character.
                        splitLine[0] = splitLine[0].Remove(0, 1);
                        int.TryParse(splitLine[0], out questionNumber);
                        newFoundQuestion.QuestionNumber = questionNumber;

                        //Set the question text.
                        newFoundQuestion.QuestionText = splitLine[1].TrimStart();

                        //Read the next (possibleAnswers) lines - 4 responses & correct answer for this exercise, but can support other values.
                        for (int i = 0; i < possibleAnswers; i++)
                        {
                            line = sr.ReadLine();
                            Answer newAnswer = BuildAnswerObject(line);

                            newFoundQuestion.Answers.Add(newAnswer);
                        }

                        //Correct answer line
                        line = sr.ReadLine();
                        Answer correctAnswerObj = GetCorrectAnswer(line, newFoundQuestion.Answers);

                        //Update the item in the List.
                        int index = newFoundQuestion.Answers.FindIndex(x => x == correctAnswerObj);
                        newFoundQuestion.Answers[index] = correctAnswerObj;

                        questions.Add(newFoundQuestion);
                    }
                }
            }

            return questions;
        }

        public static Answer BuildAnswerObject(string line)
        {
            string[] splitLine;
            splitLine = line.Split('.');

            Answer newAnswer = new Answer();

            int responseNumber;
            int.TryParse(splitLine[0], out responseNumber);
            newAnswer.AnswerNumber = responseNumber;
            newAnswer.AnswerText = splitLine[1].TrimStart();

            return newAnswer;
        }

        public static Answer GetCorrectAnswer(string lineData, List<Answer> answers)
        {
            int correctAnswer;
            int.TryParse(lineData, out correctAnswer);

            Answer correctAnswerObj = answers.Where(x => x.AnswerNumber == correctAnswer).FirstOrDefault();
            correctAnswerObj.IsCorrectAnswer = true;

            return correctAnswerObj;
        }
    }
}
