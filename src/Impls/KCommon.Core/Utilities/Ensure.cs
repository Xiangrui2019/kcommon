using System;
using System.Diagnostics.CodeAnalysis;

namespace KCommon.Core.Utilities
{
    public static class Ensure
    {
        public static void Equal(int expected, int actual, string argumentName)
        {
            if (expected != actual)
                throw new ArgumentException($"{argumentName} expected value: {expected}, actual value: {actual}");
        }

        public static void Equal(long expected, long actual, string argumentName)
        {
            if (expected != actual)
                throw new ArgumentException($"{argumentName} expected value: {expected}, actual value: {actual}");
        }

        public static void Equal(bool expected, bool actual, string argumentName)
        {
            if (expected != actual)
                throw new ArgumentException($"{argumentName} expected value: {expected}, actual value: {actual}");
        }
    }
}