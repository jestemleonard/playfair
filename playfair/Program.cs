using System;
using System.Collections.Generic;
using System.Linq;

namespace playfair
{
    class KeyTable
    {
        public char[,] CharArray = new char[7,5];

        string alphabet = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";

        public KeyTable(string keyraw)
        {

            keyraw = keyraw.ToUpper();
            string key = string.Empty;

            // Sprawdza które znaki w kluczu są prawidłowymi literami i usuwa pozostałe
            foreach (char c in keyraw)
            {
                if (alphabet.Contains(c))
                    key += c;
            }
            // Dodaje słowo klucz na początek alfabetu
            string alphabetKey = key + alphabet;

            // Zastosowanie HashSet usuwa powtarzające się znaki
            var unique = new HashSet<char>(alphabetKey);

            int i = 0;
            int j = 0;
            foreach (char c in unique)
            {
                if (j < 5)
                {
                    CharArray[i, j] = c;
                    j++;
                }
                else
                {
                    j = 0;
                    i++;
                    CharArray[i, j] = c;
                    j++;
                }
            }

        }

        /// <summary>
        /// Returns the key table
        /// </summary>
        /// <returns></returns>
        public string DisplayArray()
        {
            string output = string.Empty;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                    output += CharArray[i, j];
                output += Environment.NewLine;
            }

            return output;
        }

        /// <summary>
        /// Returns position of a char in the key table
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int[] GetIndex(char c)
        {
            int[] output = {0, 0};
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (CharArray[i, j] == c)
                    {
                        output[0] = i;
                        output[1] = j;
                        return output;
                    }
                }
            }

            return output;
        }

    }

    class Digram
    {
        public char FirstChar;
        public char SecondChar;

        public Digram(char first, char second)
        {
            FirstChar = first;
            SecondChar = second;
        }
    }

    class Program
    {
        /// <summary>
        /// Returns string of encrypted input
        /// </summary>
        /// <returns></returns>
        public static string Encrypt(string input, KeyTable keyTable)
        {
            Digram[] inputArray = CreateDigramsArray(input);
            //// Szyfrowanie

            foreach (Digram currentPair in inputArray)
            {
                int[] firstCharPos = keyTable.GetIndex(currentPair.FirstChar);
                int[] secondCharPos = keyTable.GetIndex(currentPair.SecondChar);

                if (firstCharPos[0] == secondCharPos[0])        // Para tworzy poziomą linię
                {
                    currentPair.FirstChar = keyTable.CharArray[firstCharPos[0], (firstCharPos[1] + 1) % 5];
                    currentPair.SecondChar = keyTable.CharArray[secondCharPos[0], (secondCharPos[1] + 1) % 5];
                }
                else if (firstCharPos[1] == secondCharPos[1]) // Para tworzy pionową linię
                {
                    currentPair.FirstChar = keyTable.CharArray[(firstCharPos[0] + 1) % 7, firstCharPos[1]];
                    currentPair.SecondChar = keyTable.CharArray[(secondCharPos[0] + 1) % 7, secondCharPos[1]];
                }
                else // Para tworzy prostokąt
                {
                    int tempPosition1 = firstCharPos[1];
                    int tempPosition2 = secondCharPos[1];
                    currentPair.FirstChar = keyTable.CharArray[firstCharPos[0], tempPosition2];
                    currentPair.SecondChar = keyTable.CharArray[secondCharPos[0], tempPosition1];
                }
            }

            // Zwraca zaszyfrowaną wiadomość
            string output = string.Empty;
            foreach (Digram currentDigram in inputArray)
            {
                output += currentDigram.FirstChar.ToString() + currentDigram.SecondChar.ToString() + " ";   //jak nie ma ToString() to zwraca liczbę
            }

            return output;
        }

        public static Digram[] CreateDigramsArray(string inputraw)
        {

            //// Tworzenie Digramów

            inputraw = inputraw.ToUpper();
            string input = string.Empty;

            // Sprawdzanie czy input jest prawidłowy
            string alphabet = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPQRSŚTUVWXYZŹŻ";
            foreach (char c in inputraw)
            {
                if (alphabet.Contains(c))
                    input += c;
            }

            // Dodawanie X pomiedzy powtorzenia
            string inputnew = "0";  // Nie może być pusty, żeby można było porównać
            foreach (char c in input)
            {
                if (c != inputnew.Last())
                    inputnew += c;
                else if (inputnew.Last() != 'X')
                    inputnew += $"X{c}";
                else
                    inputnew += $"Y{c}";
            }
            inputnew = inputnew.Substring(1);

            // Dodawanie X jeśli input nie jest parzysty
            int stringLength = inputnew.Length;
            if ((inputnew.Length % 2) != 0)
            {
                inputnew += 'X';
                stringLength++;
            }

            // Stworzenie tablicy
            char[] arr = inputnew.ToCharArray(0, stringLength);
            Digram[] inputArray = new Digram[stringLength / 2];

            // Wypełnienie tablicy
            int counter = 0;
            for (int i = 0; counter < stringLength; i++)
            {
                inputArray[i] = new Digram(arr[counter], arr[counter + 1]);
                counter += 2;
            }

            return inputArray;
        }

        public static string DisplayDigram(Digram[] inputArray)
        {
            string output = string.Empty;
            foreach (Digram currentDigram in inputArray)
            {
                output += currentDigram.FirstChar.ToString() + currentDigram.SecondChar.ToString() + " ";   //jak nie ma ToString() to zwraca liczbę
            }

            return output;
        }
    }
}
