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
        private static void END()
        {
            InputData.Pointer++;
            InputData.lexems.Add(("Число", number.ToString()));
            number = 0;
        }
        private static void END_minus()
        {
            InputData.lexems.Add(("Число", number.ToString()));
            number = 0;
        }
        public static void Analyse()
        {
            number = number * 10 + (InputData.Current - '0');
            InputData.Pointer++;

            if (InputData.Pointer >= InputData.Data.Length) throw new Exception($"неожиданное окончание файла при чтении числа");


            switch (InputData.RecognizeChar())
            {
                case "<ц>": Analyse(); break;
                case "<б>": throw new Exception("Недопустимый символ");
                case "< >": END(); break;
                case "<'>": throw new Exception("Недопустимый символ");
                case "<,>":
                case "<;>":
                case "<с>":
                case ">,<":
                case "<=>": END_minus(); break;
                default: throw new Exception("Недопустимый символ");
            }
        }
    }
}