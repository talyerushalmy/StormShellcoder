using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace StormShellcoder
{
    public static class DataManipulation
    {
        public static string[] splitEveryNChars(string input, int n)
        {
            List<string> list = Regex.Replace(input, "(.{" + n + "})", "$1" + '-').Split('-').ToList();
            list = list.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            return list.ToArray();
        }

        public static string manipulateOutput(string original)
        {
            string output = "";

            if (!Settings.getUseCapitalLetters())
            {
                original = original.ToLower();
            }

            if (Settings.getAddJoinSequenceToBeginning() && original != "")
            {
                output += Settings.getJoinSeq();
            }

            output += String.Join(Settings.getJoinSeq(), original.Split('-'));

            if (Settings.getAddJoinSequenceToEnd() && original != "")
            {
                output += Settings.getJoinSeq();
            }

            output = Settings.getPrefix() + output + Settings.getSuffix();

            return output;
        }
    }
}