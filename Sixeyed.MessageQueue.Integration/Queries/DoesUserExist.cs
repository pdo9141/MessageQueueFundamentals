using System;

namespace Sixeyed.MessageQueue.Integration.Queries
{
    public class DoesUserExist
    {
        public static bool Execute(string emailAddress)
        {
            // Fake the funk DB call here
            Random randomGenerator = new Random();
            return randomGenerator.Next(2) == 1 ? true : false;
        }
    }
}
