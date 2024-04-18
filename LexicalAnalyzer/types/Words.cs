using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LexicalAnalyzer.types
{
    public static class Words
    {
        private static readonly List<string> ListOfFunctionWords = new List<string>() { "while", "int", "print", "write", "if", "else", "or", "not", "and" };
        private static string variable = "";
        private static void Recognized()
        {
            InputData.index++;
            next();
        }

        private static void next()
        {
            if (ListOfFunctionWords.Contains(variable))
                InputData.lexems.Add(("зарезервированное слово", variable));
            else
                InputData.lexems.Add(("идентификатор", variable));
            variable = "";
        }

        public static void Analyse()
        {
            variable += InputData.Current;
            InputData.index++;

            if (InputData.index >= InputData.Data.Length) throw new Exception($"variable / ошибка в Words.cs");


            switch (InputData.RecognizeChar())
            {
                case "<ц>": Analyse(); break;
                case "<б>": Analyse(); break;
                case "< >": Recognized(); break;
                case "<,>": next(); break;
                case "<;>": next(); break;
                case "<скобки>": next(); break;
                case "<ЗО>": next(); break;
                case "<=>": next(); break;
                default: throw new Exception("Недопустимый символ");
            }
        }
    }
}