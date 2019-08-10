using System;
using System.Collections.Generic;

namespace Assets.Parsing
{
    public static class Flags
    {
        private static Dictionary<string, string> _flags = new Dictionary<string, string>();

        public static string Get(string flagName)
        {
            if (_flags.ContainsKey(flagName))
                return _flags[flagName];

            return null;
        }

        public static void Set(string flagName, string flagValue)
        {
            if (_flags.ContainsKey(flagName))
                _flags[flagName] = flagValue;
            else
                _flags.Add(flagName, flagValue);
        }
    }
}
