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
        private static readonly List<string> _reservedWords = new List<string>() { "while", "int", "string", "bool", "print", "input", "if", "else", "or", "not", "and" };
        private static string _name = "";
        private static void END()
        {
            InputData.Pointer++;
            END_minus();
        }

        private static void END_minus()
        {
            if (_reservedWords.Contains(_name))
                InputData.lexems.Add(("зарезервированное_слово", _name));
            //Console.WriteLine($"Распознано зарезервированное слово: {_name};");
            else
                InputData.lexems.Add(("идентификатор", _name));
            //Console.WriteLine($"Распознан идентификатор: {_name};");

            _name = "";
        }

        public static void Analyse()
        {
            _name += InputData.Current;
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