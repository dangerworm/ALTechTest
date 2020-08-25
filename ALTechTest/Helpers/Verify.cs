using System;
using System.Diagnostics;

namespace ALTechTest.Helpers
{
    public static class Verify
    {
        public static void NotNull(object parameter, string parameterName, string message = null)
        {
            if (parameter != null) return;

            if (!string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(parameterName, message);

            var sourceMethodName = new StackTrace().GetFrame(1).GetMethod().Name;
            message = $"Parameter {parameterName} provided to {sourceMethodName} had a null value.";

            throw new ArgumentNullException(parameterName, message);
        }
    }
}