using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Components
{
    public class WordieGame
    {
        private static ArrayList possible_answers = new ArrayList() { "anime", "month", "class" };
        private static String[] possible_guesses = new string[] { "anime", "month", "class", "aisle", "wreck", "dodge", "never" };

        public int wordsToGuess;

        // Todo: uncomment if expanding wordie to more options
        // private ArrayList answers = new ArrayList();

        public string answer { get; set; }

        public int attempts = 6;

        public bool validGuess = false;
        public string result { get; set; }

        public bool isPlaying = true;
        public bool isCorrect = false;

        public WordieGame(int numOfWords)
        {
            // create wordle game based on number of words to guess
            wordsToGuess = numOfWords;
            SetupGame();
        }

        public void SetupGame()
        {
            Random random = new Random();

            // create game board based on number of words to guess
            for (int i = 0; i < wordsToGuess; i++)
            {
                int randomIndex = random.Next(possible_answers.Count + 1);

                // todo: uncomment later if expanding wordie to more words
                //answers.Add((string) possible_answers[randomIndex]);

                answer = (string) possible_answers[randomIndex];
                Console.WriteLine("Generated word: " + answer);
            }

            // add more attempts if more words
            attempts += (wordsToGuess - 1);

        }

        public void makeGuess(string input)
        {
            result = "";

            validGuess = possible_guesses.Contains(input);

            if (validGuess) {
                    for (int i = 0; i < input.Length; i++)
                    {
                        result += compareGuessLetterByLetter(input.Substring(i, 1), i);

                        Console.WriteLine(result);
                    }
            } else
            {
                Console.WriteLine("invalid guess.");
            }

            attempts--;

            // result = ccccc => all correct guesses => game won
            if (result == "ccccc")
            {
                isCorrect = true;
                isPlaying = false;
            } else
            {
                Console.WriteLine("Guess Again. result: " + result);
            }

            checkGameState();
        }

        public string compareGuessLetterByLetter(string letter, int index)
        {
            if (letter == answer.Substring(index, 1))
            {
                // correct letter guess & position
                // c = correct
                return "c";
            }

            if (answer.Contains(letter))
            {
                // letter is present but not in correct position
                // p = yellow
                return "p";
            } 

            // letter is incorrect
            // f = grey
            return "f";

        }


        private void checkGameState()
        {
            if (attempts == 0)
            {
                isPlaying = false;
            }

            if (isCorrect)
            {
                // victory screen
                Console.WriteLine("You won!");
            }

            if (!isPlaying && !isCorrect)
            {
                // defeat screen
                Console.WriteLine("You lose!");
                Console.WriteLine("The Wordie was " + answer + "!!!");
            }
        }


    }
}
