using LexicalAnalyzer.last;
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
    public static class Comparison
    {
        private static char prev = ' ';

        private static void Recognized()
        {
            if (InputData.Current == ' ') InputData.lexems.Add(("оператор сравнения", prev.ToString()));
            else if (InputData.Current == '=') InputData.lexems.Add(("оператор сравнения", prev.ToString() + '='));
            else throw new Exception("Недопустимый символ");

            InputData.index++;
        }
        private static void next()
        {
            Console.WriteLine($"Распознан оператор сравнения: {prev} ");
        }

        public static void Analyse()
        {
            prev = InputData.Current;
            InputData.index++;

            if (InputData.index >= InputData.Data.Length) throw new Exception($"Ошибка в файле Comparison.cs");


            switch (InputData.RecognizeChar())
            {
                case "<ц>": next(); break;
                case "<б>": next(); break;
                case "< >": Recognized(); break;
                case "<,>": throw new Exception("Недопустимый символ");
                case "<;>": throw new Exception("Недопустимый символ");
                case "<скобки>": next(); break;
                case "<ЗО>": next(); break;
                case ">,<,!": Recognized(); break;
                case "<=>": Recognized(); break;
                default: throw new Exception("Недопустимый символ");
            }
        }
    }
}