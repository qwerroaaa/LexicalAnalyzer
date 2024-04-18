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

        private static void END()
        {
            if (InputData.Current == ' ') InputData.lexems.Add(("оператор сравнения", prev.ToString())); //Console.WriteLine($"Распознан оператор сравнения: {prev} ");
            else if (InputData.Current == '=') InputData.lexems.Add(("оператор сравнения", prev.ToString() + '='));
            else throw new Exception("Недопустимый символ");

            InputData.Pointer++;
        }
        private static void END_minus()
        {
            Console.WriteLine($"Распознан оператор сравнения: {prev} ");
        }

        public static void Analyse()
        {
            prev = InputData.Current;
            InputData.Pointer++;

            if (InputData.Pointer >= InputData.Data.Length) throw new Exception($"неожиданное окончание файла при сравнении");


            switch (InputData.RecognizeChar())
            {
                case "<ц>": END_minus(); break;
                case "<б>": END_minus(); break;
                case "< >": END(); break;
                case "<'>": END_minus(); break;
                case "<,>": throw new Exception("Недопустимый символ");
                case "<;>": throw new Exception("Недопустимый символ");
                case "<с>": END_minus(); break;
                case ">,<": END(); break;
                case "<=>": END(); break;
                default: throw new Exception("Недопустимый символ");
            }
        }
    }
}