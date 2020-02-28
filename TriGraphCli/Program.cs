using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//This is a tool to simplify the process of generating a Tri-graph. If you are wondering wha Tri-graph (AKA Diana Cryptosystem) is please go to https://en.wikipedia.org/wiki/One-time_pad 
//This Method has been used by the U.S. Forces since Vietnam War.
//It involves an One-Time pad thus it is very hard to break.

namespace TriGraphCli
{
    //If we assign value from 1 - 26 for letters A - Z, in TriGraph, the three numbers add together will either be 54 or 28.  
    //Using Dictionary to initiate the 26 letters with numbers.
    class Program
    {
        public static string OTP = ""; //One Time Phrase
        static Dictionary<string, int> letterNum = new Dictionary<string, int>(); //This is will be the base format for each letter <'A',1>, <'B',2>.......<'Z',26>

        static void programInit() // Add key and value to the dictionary.
        {
            for(int i=65; i<=90; i++) //in ASCII 'A' is 65 and 'Z' is 90
            {
                string letter = char.ConvertFromUtf32(i);
                letterNum.Add(letter, i - 64);
            }
            Console.WriteLine("System Initiation Completed.");
        }

        static void oneTimePhrase() //Ask fo4 OTP, convert too Upper Cases. Check to make sure it only contains letters and no repeated letters.
        {
            string phrase = (Console.ReadLine()).ToUpper();            //Convert to all Caps             
            bool correctInput = Regex.IsMatch(phrase, @"^[A-Z]+$");    //Check for letters only
            bool repeatedInput = Regex.IsMatch(phrase, @"(.)\1+");     //Check for any repeating letters
            bool loop = true;
            do{
                Console.WriteLine("Please Enter a One time Phrase: ");

                if (correctInput)
                {
                    if (repeatedInput)
                    {
                        Console.WriteLine("One Time Phrase CANNOT contain repeated letters (non-case sensitive)!");
                    }
                    else
                    {
                        OTP = phrase;
                        loop = false;
                    }
                }
                else
                {
                    Console.WriteLine("One Time Phrase can only contain letters!");
                }
            } while (loop);
        }

        static void encrypt() //message encryption
        {
            Console.WriteLine("What is the message?");
            int count = OTP.Length;
            bool EOM = false;
            int i = 0;
            while(!EOM)
            {
                for()
            }
        }

        static void decrypt() //message decryption
        {

        }

        static void Main(string[] args)
        {
            bool keepGoing = true;
            programInit();
            do {
                Console.WriteLine("Welcome to TriGraphCli. For Encryption enter 1; For Decryption Enter 2; To exit press any other key.");
                string mainOption = Console.ReadLine();
                switch (mainOption)
                {
                    case "1":
                        oneTimePhrase();
                        
                        break;

                    case "2":
                        oneTimePhrase();

                        break;

                    default:
                        Console.WriteLine("TriGraphCli exiting");
                        keepGoing = false;
                        break;
    
                }         

            } while (keepGoing);

        }
    }
}
