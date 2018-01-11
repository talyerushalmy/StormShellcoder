using System;

namespace StormShellcoder
{
    public static class DataManipulation
    {
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

            return output;
        }
    }
}