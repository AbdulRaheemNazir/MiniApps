using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        char choice;

        do
        {
            Console.WriteLine("P4CS Mini Applications");
            Console.WriteLine("----------------------");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1) Keep Counting");
            Console.WriteLine("2) Square Root Calculator");
            Console.WriteLine("3) Encrypt Text (Caesar Cipher)");
            Console.WriteLine("4) Decrypt Text (Caesar Cipher)");
            Console.WriteLine("5) New Square Root Calculator");
            Console.WriteLine("9) Quit");

            choice = Console.ReadKey().KeyChar;
            if (choice == '9')
                break;

        } while (choice < '1' | choice > '8' & choice != 'q');

        Console.WriteLine("\n");

        try
        {
            switch (choice)
            {
                case '1':
                    Optionone();
                    break;
                case '2':
                    Optiontwo();
                    break;
                case '3':
                    Optionthree();
                    break;
                case '4':
                    Optionfour();
                    break;
                case '5':
                    OptionFive();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.ReadKey();
        }
    }

    static void Optionone()
    {
        Console.WriteLine("Keep Counting");
        Console.WriteLine("--------------");
        Console.WriteLine("You will be presented with 10 arithmetic questions.");
        Console.WriteLine("After the first question, the left-hand operand is the result of the previous addition.");
        Console.WriteLine(" ");

        //         bool onlyOnce = false;          i might need it for when wrong

        var rnd = new Random();
        int numberx = rnd.Next(1, 10);
        int counter = 0;

        for (int b = 1; b < 11; b++)
        {
            int numbery = rnd.Next(1, 10);
            //       Random symb = new Random();
            //     int symbol = symb.Next(-1, +1);

            Console.WriteLine($"Question {b}:   {numberx} + {numbery}");

            int sameanswer = Convert.ToInt32(Console.ReadLine());
            int donkey = numberx + numbery;

            if (sameanswer != donkey)
            {
                Console.WriteLine("Incorrect");
                Console.WriteLine(" ");
                Console.WriteLine(numberx + numbery);
                numberx = donkey;
            }
            else
            {
                numberx = donkey;
                Console.WriteLine("Correct");
                counter++;
            }
        }

        Console.WriteLine("You got " + counter + " out of 10 questions correct");
        Console.ReadLine();
    }

    static void Optiontwo()
    {
        Console.WriteLine("Enter a whole, positive number:");
        int number = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enters the number of decimal places for the precision square root result (Range 1-->6): ");
        int shift2 = Convert.ToInt32(Console.ReadLine());

        double root = 1;
        int i = 0;
        //The Babylonian Method for Computing Square Roots
        while (true)
        {
            i++;
            root = (number / root + root) / 2;
            if (i == number + 1) { break; }
        }

        //Output
        double shift3 = Math.Round(root, shift2, MidpointRounding.ToEven);

        Console.WriteLine($"Square root: {shift3}");
        Console.ReadLine();
    }

    static void Optionthree()
    {
        Console.WriteLine("Type a string to encrypt:");

        var UserString = Console.ReadLine();
        Regex r = new("^[a-zA-Z ]+$");

        if (r.IsMatch(UserString))
        {
            Console.WriteLine("Enter your Key:");
            int key = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Encrypted Data:");
            string cipherText = Encipher(UserString, key);
            Console.WriteLine(cipherText);
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Invalid input.");
            Optionthree();
        }
    }

    static void Optionfour()
    {
        Console.WriteLine("Type a string to decrypt:");

        var UserString = Console.ReadLine();
        Regex r = new("^[a-zA-Z ]+$");

        if (r.IsMatch(UserString))
        {
            Console.WriteLine("Enter your Key:");
            int key = Convert.ToInt32(Console.ReadLine());

            string cipherText = Decipher2(UserString, key);
            string t = Decipher3(cipherText, key);
            Console.WriteLine("Decrypted Data:");
            Console.WriteLine(cipherText);
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Invalid input.");
            Optionfour();
        }
    }

    static void OptionFive()
    {
        Console.WriteLine("Square Root Calculator: ");
        Console.WriteLine("----------------------");
        Console.WriteLine("Please enter a positive number: ");
        int number = int.Parse(Console.ReadLine());

        bool validinteger;

        do
        {
            Console.WriteLine("How many decimal places do you want the solution calculated to: ");
            int numberofdecimalplaces = int.Parse(Console.ReadLine());
            double upperbound = 0d;
            double startnumber = 1d; // number to start counting p at

            if (numberofdecimalplaces > 0 & numberofdecimalplaces < 7)
            {
                validinteger = true;

                while (startnumber < number) // checks if startnumber is greater than the number being square rooted
                {
                    upperbound = upperbound + 1d; // adds 1 to the original number
                    startnumber = upperbound * upperbound; // squares original number

                }

                double lowerbound = upperbound - 1d; // finds lowerbound
                double avrgbound = (lowerbound + upperbound) / 2d; // finds average
                while (upperbound - lowerbound >= 0.000001)
                { // checks what the difference between the bounds is to six decimal places as that is the most precise you can go
                    if (avrgbound * avrgbound > number)
                    {
                        upperbound = upperbound - (upperbound - lowerbound) / 2; // decreasing the number
                        avrgbound = (lowerbound + upperbound) / 2;


                    }
                    else
                    {
                        lowerbound = lowerbound + (upperbound - lowerbound) / 2; // increasing the number
                        avrgbound = (lowerbound + upperbound) / 2;

                    }
                }
                decimal aver = (decimal)((upperbound + lowerbound) / 2d);
                decimal roundednumber = decimal.Round(aver, numberofdecimalplaces); // puts number to the correct number of decimal places

                Console.WriteLine(roundednumber); // prints the squarerooted nuber
            }
            else
            {
                validinteger = false;
                Console.WriteLine("Please enter a valid decimal");
            }
        } while (!validinteger);
    }


    /*Used for the Decipher*/
    static char cipher2(char ch, int key)
    {
        if (!char.IsLetter(ch))
        {
            return ch;
        }

        char d = char.IsUpper(ch) ? 'A' : 'a';
        return (char)((ch - key - d) % 26 + d);
    }

    static string Decipher2(string input, int key)
    {
        string output = string.Empty;

        foreach (char ch in input)
            output += cipher2(ch, key);

        return output;
    }

    static string Decipher3(string input, int key)
    {
        return Encipher(input, 26 - key);
    }

    /*Used for the Encipher*/
    static char cipher(char ch, int key)
    {
        if (!char.IsLetter(ch))
        {

            return ch;
        }

        char d = char.IsUpper(ch) ? 'A' : 'a';
        return (char)((ch + key - d) % 26 + d);
    }

    static string Encipher(string input, int key)
    {
        string output = string.Empty;

        foreach (char ch in input)
            output += cipher(ch, key);

        return output;
    }

    static string Decipher(string input, int key)
    {
        return Encipher(input, 26 - key);
    }
}