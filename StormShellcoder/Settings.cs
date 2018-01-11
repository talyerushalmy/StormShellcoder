﻿using System.Security.Policy;

namespace StormShellcoder
{
    public static class Settings
    {
        private static string joinSeq;
        private static bool addJoinSequenceToBeginning;
        private static bool useCapitalLetters;

        public static string getJoinSeq()
        {
            return Settings.joinSeq;
        }

        public static void setJoinSeq(string joinSeq)
        {
            Settings.joinSeq = joinSeq;
        }

        public static bool getAddJoinSequenceToBeginning()
        {
            return Settings.addJoinSequenceToBeginning;
        }

        public static void setAddJoinSequenceToBeginning(bool addJoinSequenceToBeginning)
        {
            Settings.addJoinSequenceToBeginning = addJoinSequenceToBeginning;
        }

        public static bool getUseCapitalLetters()
        {
            return Settings.useCapitalLetters;
        }

        public static void setUseCapitalLetters(bool useCapitalLetters)
        {
            Settings.useCapitalLetters = useCapitalLetters;
        }
    }
}