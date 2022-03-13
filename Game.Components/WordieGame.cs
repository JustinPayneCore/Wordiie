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

        private int wordsToGuess;

        // Todo: uncomment if expanding wordie to more options
        // private ArrayList answers = new ArrayList();

        public string answer;

        private int attempts = 6;

        private bool isPlaying = true;
        private bool isCorrect = false;

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
                int randomIndex = random.Next(possible_answers.Count);

                // todo: uncomment later if expanding wordie to more words
                //answers.Add((string) possible_answers[randomIndex]);

                answer = (string) possible_answers[randomIndex];
            }

            // add more attempts if more words
            attempts += (wordsToGuess - 1);

        }

        public void makeGuess(string input)
        {
            int result = 0;

            if (possible_guesses.Contains(input)) {
                    for (int i = 0; i < input.Length; i++)
                    {
                        result += compareGuessLetterByLetter(input.Substring(i, 1), i);
                        result = result * 10;

                        Console.WriteLine(result);
                    }
            }

            attempts--;

            // result = 11111 => all correct guesses => game won
            if (result == 11111)
            {
                isCorrect = true;
                isPlaying = false;
            }

            checkGameState();
        }

        public int compareGuessLetterByLetter(string letter, int index)
        {
            if (letter == answer.Substring(index, 1))
            {
                // correct letter guess & position
                // 1 = green
                return 1;
            }

            if (answer.Contains(letter))
            {
                // letter is present but not in correct position
                // 2 = yellow
                return 2;
            } 

            // letter is incorrect
            // 0 = grey
            return 0;

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
