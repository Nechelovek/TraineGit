using System;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Parser;

namespace M05_Logging
{
    class Program
    {
        private static ILogger logger = new NLogLoggerFactory().CreateLogger("Logger");
  
        static void Main(string[] args)
        {
            logger.LogDebug("Start program");
            Parse.Logger = new NLogLoggerProvider().CreateLogger("Logger");
            
            var number = "123";
            Console.WriteLine($"You entered: {number}");
            
            try
            {
                logger.LogDebug($"Try to convert entered string {nameof(number)} to int");
                
                Console.WriteLine($"Your number was converted to int result: {Parse.ToInt(number)}");
            }
            catch (FormatException fe)
            {
                logger.LogError(fe, fe.Message);
                
                Console.WriteLine($"You entered invalid data {number}, please check and fix it!");
                
                logger.LogDebug("The application failed, please see Errors.log");
                return;
            }
            catch (ArgumentNullException ane)
            {
                logger.LogError(ane, ane.Message);
                
                Console.WriteLine($"You entered invalid data {number}, please check and fix it!");
                
                logger.LogDebug("The application failed, please see Errors.log");
                return;
            } 
            
            logger.LogDebug("Program is completed");
        }
    }
}