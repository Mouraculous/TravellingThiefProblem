using System;
using System.Collections.Generic;
using System.Text;

namespace TravellingThief.Utilities
{
    public class ObjectValidator
    {
        public static void IfNullThrowException(object o, string objectName)
        {
            if (o is null)
            {
                throw new NullArgumentException($"Argument for {objectName} you provided is null");
            }
        }
    }

    public class NullArgumentException : Exception
    {
        public NullArgumentException(string msg) : base(msg)
        {
        }
    }
}
