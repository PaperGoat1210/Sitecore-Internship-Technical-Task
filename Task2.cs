using System;

class PalindromeChecker
{
    static void Main()
    {
        string inputString1 = "a@b!!b$a";
        string trashSymbolString1 = "!@$";

        string inputString2 = "?Aa#c";
        string trashSymbolString2 = "#?";

        Console.WriteLine($"InputString: {inputString1}");
        Console.WriteLine($"TrashSymbolsString: {trashSymbolString1}");
        if (IsPalindrome(inputString1, trashSymbolString1))
        {
            Console.WriteLine("Result should be: True");
        }
        else
        {
            Console.WriteLine("Result should be: False");
        }

        Console.WriteLine();
        Console.WriteLine($"InputString: {inputString2}");
        Console.WriteLine($"TrashSymbolsString: {trashSymbolString2}");
        if (IsPalindrome(inputString2, trashSymbolString2))
        {
            Console.WriteLine("Result should be: True");
        }
        else
        {
            Console.WriteLine("Result should be: False");
        }
    }

    public static bool IsPalindrome(string InputString, string TrashSymbolString)
    {
        // Convert input and trash symbols to lowercase
        InputString = InputString.ToLower();
        TrashSymbolString = TrashSymbolString.ToLower();

        int i = 0;
        int j = InputString.Length - 1;

        while (i < j)
        {
            // Skip trash symbols at the beginning of the string
            while (i < InputString.Length && TrashSymbolString.Contains(InputString[i]))
            {
                i++;
            }

            // Skip trash symbols at the end of the string
            while (j >= 0 && TrashSymbolString.Contains(InputString[j]))
            {
                j--;
            }

            // Compare characters ignoring trash symbols
            if (i < j && InputString[i] != InputString[j])
            {
                return false;
            }

            i++;
            j--;
        }

        return true;
    }

}


