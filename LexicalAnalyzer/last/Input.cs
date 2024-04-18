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
    public static class Input
    {
        public static void Recognized(string data)
        {
            InputData.lexems.Add((InputData.Current.ToString(), data));
            InputData.index++;
        }
        public static void Analyse(string inputString)
        {
            InputData.Data = inputString;
            while (InputData.index < InputData.Data.Length)
            {
                string current = InputData.RecognizeChar();
                switch (current)
                {
                    case "<ц>":
                        Numbers.Analyse();
                        break;

                    case "<б>":
                        Words.Analyse();
                        break;

                    case "< >":
                        InputData.index++;
                        break;

                    case "<,>":
                        Recognized("разделительный символ");
                        break;

                    case "<;>":
                        Recognized("концевой символ");
                        break;

                    case "<скобки>":
                        Recognized($"спецсимвол {InputData.Current}");
                        break;

                    case "<ЗО>":
                        Recognized($"спецсимвол {InputData.Current}");
                        break;

                    case ">,<,!":
                        Comparison.Analyse();
                        break;

                    case "<=>":
                        Recognized("оператор присваивания");
                        break;
                    default:
                        throw new Exception($"Недопустимый символ. {current}");
                }
            }
        }
    }
}