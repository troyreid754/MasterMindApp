using System;
using System.Reflection.Metadata.Ecma335;

class Program
{
    static void Main()
    {
        // Random function generates a number from 1-6
        Random random = new Random();
        string answer = GenerateAnswer(random); //string of 4 numbers to make iterable.
        Console.WriteLine(answer);

        Console.WriteLine("Welcome to The Mastermind!");
        Console.WriteLine("In 10 attempts or less guess the 4 digit number. Each Digit is between 1 and 6. \n+ indicates the number was guessed correctly, - indicates the digit is in the sequence but in the wrong postition.\n ");


        int attempts = 10;
        while (attempts > 0)
        {
            Console.Write("Enter your guess here: ");
            string guess = Console.ReadLine();

            if (guessChecker(guess))
            {
                string clues = generateClues(answer, guess);
                Console.WriteLine(clues);

                if (guess == answer)
                {
                    Console.WriteLine("YOU WIN! BRAVO!");
                    return;
                }

                attempts--;
                Console.WriteLine($"{attempts} Attempts Remaining");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a 4-digit number using digits 1-6 only.");
            }
        }

        Console.WriteLine($"OOPS, No attempts remaining! The correct number was: {answer}.\nBetter Luck Next Time");
    }

    static string GenerateAnswer(Random random)
    {
        char[] number = new char[4];
        for (int i = 0; i < 4; i++)
        {
            number[i] = (char)('1' + random.Next(0, 6)); // Generates a digit between '1' and '6'
        }
        return new string(number);
    }

//function used to check that the input guess is valid.
//TryParse checks if the string can be evaluated as integer.
//.Contains checks if numbers outside of the range 1-6 are included
    static bool guessChecker(string guess)
    {
        return guess.Length == 4 && int.TryParse(guess, out _) &&
               !guess.Contains('7') && !guess.Contains('8') && !guess.Contains('9') && !guess.Contains('0');

    }

    static string generateClues(string answer, string guess)
    {
        int plusCount = 0;
        int minusCount = 0;
        bool[] answerIter = new bool[4];
        bool[] guessIter = new bool[4];

        // Check the + first which indicates the number of correct answers
        for (int i = 0; i < 4; i++)
        {
            if (guess[i] == answer[i])
            {
                plusCount++;
                answerIter[i] = true;
                guessIter[i] = true;
            }
        }

        // Check the - next to get number of correct digits in wrong position
        
        for (int j = 0; j < 4; j++)
        {
            if (!guessIter[j]) // Only consider unevaluated guess.
            {
                for (int k = 0; k < 4; k++)
                {
                    if (!answerIter[k] && guess[j] == answer[k])
                    {
                        minusCount++;
                        answerIter[k] = true; // Mark this answer digit as evaluated
                        break; // Exits loop
                    }
                }
            }
        }

        // create return value that indicates the clues

        return new string('+', plusCount) + new string('-', minusCount);
    }
}
