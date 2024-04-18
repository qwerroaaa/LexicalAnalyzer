using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer
{
    public static class InputData
    {
            public static string Data { get; set; } = "int a = 52";
            public static int Pointer { get; set; } = 0;

            public static char Current { get { return Data[Pointer]; } }

            public static List<(string, string)> lexems = new List<(string, string)>();

            public static string RecognizeChar()
            {
                if (Pointer >= Data.Length) throw new Exception("Внезапное окончание файла");
                else if (Current >= '0' && Current <= '9')
                    return "<ц>";
                else if (Current >= 'a' && Current <= 'z' || Current >= 'A' && Current <= 'Z')
                    return "<б>";
                else if (Current == ' ') return "< >";
                else if (Current == ',') return "<,>";
                else if (Current == ';') return "<;>";
                else if (Current == '+' || Current == '-' || Current == '*' || Current == '/')
                    return "<ЗО>";
                else if (Current == '<' || Current == '>') return ">,<";
                else if (Current == '=') return "<=>";
                else if (Current == '(' ||
                        Current == ')' ||
                        Current == '[' ||
                        Current == ']' ||
                        Current == '{' ||
                        Current == '}')
                    return "<cкобки>";
                else if (Current == '!') return "<!>";
                else throw new ArgumentOutOfRangeException("символ \"" + Current + "\" недопустим в грамматике");
        }
    }
}
