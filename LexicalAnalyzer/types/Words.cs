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
        private static void END()
        {
            InputData.Pointer++;
            END_minus();
        }

        private static void END_minus()
        {
            if (ListOfFunctionWords.Contains(variable))
                InputData.lexems.Add(("зарезервированное_слово", variable));
            else
                InputData.lexems.Add(("идентификатор", variable));
            variable = "";
        }

        public static void Analyse()
        {
            variable += InputData.Current;
            InputData.Pointer++;

            if (InputData.Pointer >= InputData.Data.Length) throw new Exception($"неожиданное окончание файла при чтении name");


            switch (InputData.RecognizeChar())
            {
                case "<ц>": Analyse(); break;
                case "<б>": Analyse(); break;
                case "< >": END(); break;
                case "<'>": throw new Exception("Недопустимый символ");
                case "<,>": END_minus(); break;
                case "<;>": END_minus(); break;
                case "<с>": END_minus(); break;
                case ">,<": END_minus(); break;
                case "<=>": END_minus(); break;
                default: throw new Exception("Недопустимый символ");
            }
        }
    }
}