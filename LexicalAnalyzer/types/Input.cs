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
        public static void END(string data)
        {
            InputData.lexems.Add((InputData.Current.ToString(), data));
            InputData.Pointer++;
        }
        public static void Analyse(string inputString)
        {
            InputData.Data = inputString;
            while (InputData.Pointer < InputData.Data.Length)
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
                        InputData.Pointer++;
                        break;

                    case "<,>":
                        END("разделительный символ");
                        break;

                    case "<;>":
                        END("концевой символ");
                        break;

                    case "<c>":
                        END($"спецсимвол {InputData.Current}");
                        break;

                    case ">,<":
                        COMP.Analyse();
                        break;

                    case "<=>":
                        END("оператор присваивания");
                        break;
                    default:
                        throw new Exception($"Недопустимый символ. {current}");
                }
            }
        }
    }
}