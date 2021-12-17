using System;
using System.Collections.Generic;
using System.Text;


namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                running = GameMenu();
            }

        }
        static bool GameMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the hangman game!");
            Console.WriteLine("\n1) Play the game!");
            Console.WriteLine("0) Exit the application...");
            Console.WriteLine("\nMake your choice and press enter:");

            switch (Console.ReadLine())
            {
                case "1":
                    HangmanGame();
                    return true;
                case "0":
                    return false;
                default:
                    return true;
            }
        }

        static void HangmanGame()

        {
            string[] wordList = { "gold", "space", "car", "snowman", "alien",
                "ship", "jacket", "book", "winter", "watch", "kitchen",
                "rainbow", "letter", "umbrella" };

            Random wordNumber = new Random();
            int randomWord = wordNumber.Next(wordList.Length);

            string wordX = wordList[randomWord];
            string wordXUpper = wordX.ToUpper(); //Changing the word to upper case letters to avoid confusion
            int numOfChar = wordX.Length;

            Console.Clear();
            Console.WriteLine("\nYou have 10 attempts to find the right word...");
            Console.WriteLine("\nThis time we are looking for a word with " + numOfChar + " letters. Good luck!" +
                "\n");

            char[] charsInWord = new char[numOfChar];   //An array that stores the correct guesses later on

            for (int i = 0; i < numOfChar; i++)
            {
                charsInWord[i] = '_';                   //An _ is added för each character the word contains
                Console.Write(charsInWord[i]);
            }

            List<char> guessList = new List<char>(); //A list that keeps track of old guesses
            StringBuilder wrongGuesses = new StringBuilder(); //A stringbuilder containing the wrong character guesses later on

            int guessesLeft = 10;
            int charFound = 0;

            string guess;
            char input;

            bool gameWon = false;
            while (true)

            {
                Console.Write("\n>");
                guess = Console.ReadLine().ToUpper();
                input = guess[0];

                if (guess == wordXUpper)
                {
                    guessesLeft--;
                    gameWon = true;
                    break;
                }

                if (guess.Length > 1)  //Any guess with more than one character is considered a word guess
                {
                    guessesLeft--;
                    Console.WriteLine("\n" + guess + " is not the correct word.");
                    Console.WriteLine("You have " + (guessesLeft) + " guesses remaining.");
                    Console.WriteLine("* Incorrect guesses: *" + wrongGuesses.ToString());

                }

                if (guessesLeft == 0)
                {
                    noGuesses(wordXUpper);
                    break;
                }

                else if (guessesLeft > 0 && guess.Length == 1)
                {

                    if (guessList.Contains(input)) //If the user already guessed the letter

                    {
                        Console.WriteLine("You have already tried '{0}'. Try another one!", input);
                        Console.WriteLine("You still have " + (guessesLeft) + " guesses remaining...");
                        Console.WriteLine("* Incorrect guesses: *" + wrongGuesses.ToString());
                        continue;
                    }


                    if (wordXUpper.Contains(input))

                    {
                        guessList.Add(input);
                        guessesLeft--;
                        Console.WriteLine("Great! '{0}' is part of the word", input);
                        Console.WriteLine("You have " + (guessesLeft) + " guesses remaining...");
                        Console.WriteLine("* Incorrect guesses: *" + wrongGuesses.ToString());

                        for (int i = 0; i < wordX.Length; i++)

                            if (wordXUpper[i] == input)
                            {
                                charsInWord[i] = wordX[i];
                                charFound++;                //Counting each found character in the word
                            }

                        if (charFound == numOfChar)

                        {
                            gameWon = true;           //A win condition if user guess the word character by character
                            break;
                        }
                    }

                    else //If the unknown word doesn't contain the input

                    {
                        wrongGuesses.Append(input + "*");
                        guessList.Add(input);
                        guessesLeft--;

                        if (guessesLeft == 0)
                        {
                            noGuesses(wordXUpper);
                            break;
                        }

                        Console.WriteLine("\nToo bad, '{0}' is not part of the word", input);
                        Console.WriteLine("You have " + (guessesLeft) + " guesses remaining...");
                        Console.WriteLine("* Incorrect guesses: *" + wrongGuesses.ToString());

                    }

                    for (int j = 0; j < numOfChar; j++)
                    {
                        if (input == wordXUpper[j])
                            charsInWord[j] = input;         //The _ in the array is replaced with each correctly guessed character
                    }
                    Console.WriteLine(charsInWord);
                }

            }

            if (gameWon)

            {
                Console.Clear();
                Console.WriteLine("*********************************************************************************************************");
                Console.WriteLine("\nFantastic! You managed to find the word " + wordXUpper + " with " + (guessesLeft) + " guesses remaining.");
                Console.WriteLine("Press enter to return to the main menu");
                Console.ReadLine();
            }

            static void noGuesses(string wordXUpper)
            {
                Console.Clear();
                Console.WriteLine("*************************************************");
                Console.WriteLine("Too bad, you have used all of your ten guesses...");
                Console.WriteLine("The word we were looking for was " + wordXUpper);

                Console.WriteLine("\nPress enter to return to the game menu");
                Console.ReadLine();
            }
        }
    }
}

