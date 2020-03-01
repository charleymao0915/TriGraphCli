using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//This is a tool to simplify the process of generating a Tri-graph. If you are wondering wha Tri-graph (Similiar Diana Cryptosystem) is please go to https://en.wikipedia.org/wiki/One-time_pad 
//This Method has been used by the U.S. Forces since Vietnam War.
//It involves an One-Time pad thus it is very hard to break.

namespace TriGraphCli
{
    //If we assign value from 1 - 26 for letters A - Z, in TriGraph, the three numbers add together will either be 54 or 28.  
    //Using Dictionary to initiate the 26 letters with numbers.
    class Program
    {
        public static string OTP = ""; //One Time Phrase
        static Dictionary<int, string> letterNum = new Dictionary<int, string>(); //This is will be the base format for each letter <'A',1>, <'B',2>.......<'Z',26>

        static void programInit() // Add key and value to the dictionary.
        {
            for(int i=65; i<=90; i++) //in ASCII 'A' is 65 and 'Z' is 90
            {
                string letter = char.ConvertFromUtf32(i);
                letterNum.Add(i - 64, letter);
            }
            Console.WriteLine("System Initiation Completed.");
            
            //Printing a Trigraph still need to fix
            int verticalCounter = 0;
            for(int i = 0; i< 78; i++)
            {
                if(i%3==0)
                {
                    Console.WriteLine((char)(verticalCounter+65) + "   " + "A B C D E F G H I J K L M N O P Q R S T U V W X Y Z");
                    verticalCounter++;
                }else if(i%3==1)
                {   
                    Console.Write("    ");
                    for(int j =53; j>27; j--)
                    {
                       
                        Console.Write("{0} ",(char)(((j-verticalCounter-1)%26)+65));
                        
                    }
                    Console.WriteLine();
                }else
                {
                    Console.WriteLine();
                }
            }   
        }

        static void oneTimePhrase() //Ask for OTP, convert too Upper Cases. Check to make sure it only contains letters and no repeated letters.
        {        

            bool loop = true;
            do{
                Console.WriteLine("Please Enter a One time Phrase: ");
                string phrase = (Console.ReadLine()).ToUpper();            //Convert to all Caps     
                bool correctInput = Regex.IsMatch(phrase, @"^[A-Z]+$");    //Check for letters only
                bool repeatedInput = Regex.IsMatch(phrase, @"(.)\1+");     //Check for any repeating letters
                if (correctInput)
                {
                    if (repeatedInput)
                    {
                        Console.WriteLine("For better seucurity, One Time Phrase CANNOT contain repeated letters (non-case sensitive)!");
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
            string PT = ""; //plain text
            string CT = ""; //cipher text
            bool loop = true;
            do //using loop to ensure correct input, only letters and spaces allowed
            {
                Console.WriteLine("What is the message? No punctuations! Spell out the numbers.");
                PT = Console.ReadLine().Replace(" ","XX").ToUpper();                     //Convert to all Caps and replace spaces with XX   
                bool correctInput = Regex.IsMatch(PT, @"^[A-Z\s]+$");    //Check for letters and spaces only
                if (!correctInput)
                {
                    Console.WriteLine("Your message can only contain letters and spaces!");
                }
                else
                {
                    loop = false;
                }                
            } while (loop);
            
            
            for(int j =0;j<PT.Length;j++)
            {
                int numSum = PT[j] - 64 + OTP[j % (OTP.Length)] - 64;
                if (numSum < 28)
                {
                    CT += letterNum[28 - numSum];
                }
                else
                {
                    CT += letterNum[54 - numSum];
                }
                
            }
            Console.WriteLine("Your Encrypted Message is: " + CT);
        }

        static void decrypt() //message decryption
        {

            string CT = "";
            string PT = "";
            bool loop = true;
            do //using loop to ensure correct input, only letters allowed
            {
                Console.WriteLine("What is the encrypted message?");
                CT = (Console.ReadLine()).ToUpper();                    //Convert to all Caps     
                bool correctInput = Regex.IsMatch(CT, @"^[A-Z]+$");    //Check for letters only
                if (!correctInput)
                {
                    Console.WriteLine("Encrypted messages can only contain letters!");
                }
                else
                {
                    loop = false;
                }                
            } while (loop);
            for (int i =0; i<CT.Length; i++)
            {
                int numSum = CT[i] - 64 + OTP[i % (OTP.Length)] - 64;
                if (numSum < 28)
                {
                    PT += letterNum[28 - numSum];
                }
                else
                {
                    PT += letterNum[54 - numSum];
                }
            }
            Console.WriteLine("Your Clear Text Message is: " + PT);
        }

        static void Main(string[] args)
        {
            bool keepGoing = true;
            programInit();
            do {
                Console.WriteLine("Welcome to TriGraphCli. Please enter your option: \n1. Encryption; \n2. Decryption; \nTo exit press any other key.");
                string mainOption = Console.ReadLine();
                switch (mainOption)
                {
                    case "1":
                        oneTimePhrase();
                        encrypt();
                        break;

                    case "2":
                        oneTimePhrase();
                        decrypt();
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
