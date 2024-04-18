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
    public static class Numbers
    {
        private static int number = 0;
        private static void Recognized()
        {
            InputData.index++;
            InputData.lexems.Add(("Число", number.ToString()));
            number = 0;
        }
        private static void next()
        {
            InputData.lexems.Add(("Число", number.ToString()));
            number = 0;
        }
        public static void Analyse()
        {
            number = number * 10 + (InputData.Current - '0');
            InputData.index++;

            if (InputData.index >= InputData.Data.Length) throw new Exception($"Ошибка в Numbers.cs");


            switch (InputData.RecognizeChar())
            {
                case "<ц>": Analyse(); break;
                case "<б>": throw new Exception("Недопустимый символ");
                case "< >": Recognized(); break;
                case "<скобки>":
                case "<;>":
                case "<ЗО>":
                case ">,<,!":
                case "<=>": next(); break;
                default: throw new Exception("Недопустимый символ");
            }
        }
    }
}