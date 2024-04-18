using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LexicalAnalyzer.last
{
    public static class InputData
    {
        public static string Data { get; set; } = "int number = 102;";
        public static int index { get; set; } = 0;

        public static char Current { get { return Data[index]; } }

        public static List<(string, string)> lexems = new List<(string, string)>();

        public static string RecognizeChar()
        {
            if (index >= Data.Length) throw new Exception("Ошибка в InputData.cs");
            else if (Current >= '0' && Current <= '9')
                return "<ц>";
            else if (Current >= 'a' && Current <= 'z' || Current >= 'A' && Current <= 'Z')
                return "<б>";
            else if (Current == ' ') return "< >";
            else if (Current == ',') return "<,>";
            else if (Current == ';') return "<;>";
            else if (Current == '+' || Current == '-' || Current == '*' || Current == '/')
                return "<ЗО>";
            else if (Current == '<' || Current == '>' || Current == '!') return ">,<,!";
            else if (Current == '=') return "<=>";
            else if (Current == '(' ||
                    Current == ')' ||
                    Current == '[' ||
                    Current == ']' ||
                    Current == '{' ||
                    Current == '}')
                return "<скобки>";
            else throw new ArgumentOutOfRangeException("символ \"" + Current + "\" недопустим в грамматике");
        }
    }
}
