using System;
using Microsoft.Extensions.Logging;

namespace Parser
{
    public class Parse
    {
        private const int AskiiValueForNumber = 48;

        public static ILogger Logger { private get; set; }
       
        private static int CharToInt(char number)
        {
            Logger.LogDebug($"Try to convert symbol {number} to number");
            if (number - AskiiValueForNumber < 0 || number - AskiiValueForNumber > 9)
            {
                throw new FormatException("Input argument is not correct");
            }
            
            return number - AskiiValueForNumber;
        }

        public static int ToInt(string number)
        {
            _ = number ?? throw new ArgumentNullException(nameof(number), "Argument was null, it is not possible");
            if (number == "")
            {
                throw new FormatException("Input argument is not correct");
            }

            var result = 0;
            var length = number.Length - 1;
            
            Logger.LogDebug($"Start convert argument ({nameof(number)}:{number}) to int");
            for (int i = 0; i < number.Length; i++)
            {
                try
                {
                    result += CharToInt(number[i]) * (int)Math.Pow(10, length--);
                }
                catch (FormatException)
                {
                    Logger.LogError($"{number[i]} impossible convert to number. Input string was {number}");
                    throw;
                }
            }

            return result;
        }
    }
}