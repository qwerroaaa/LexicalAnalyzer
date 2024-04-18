using System;
using System.Collections.Generic;

namespace LexicalAnalyzer
{
    public class Lexeme
    {
        public string Type { get; }
        public string Value { get; }

        public Lexeme(string type, string value)
        {
            Type = type;
            Value = value;
        }
    }

    public class LexicalAnalyzer
    {
        private readonly string _inputData;
        private int _currentIndex;

        private readonly HashSet<string> _reservedKeywords = new HashSet<string>
        {
            "if", "else", "while", "print", "int", "write", "and", "or", "not" 
        };

        public LexicalAnalyzer(string inputData)
        {
            _inputData = inputData;
            _currentIndex = 0;
        }

        private char CurrentChar => _inputData[_currentIndex];

        private void MoveNext()
        {
            _currentIndex++;
        }

        public List<Lexeme> Analyze()
        {
            var lexemes = new List<Lexeme>();

            while (_currentIndex < _inputData.Length)
            {
                var currentChar = CurrentChar;

                if (char.IsDigit(currentChar))
                {
                    lexemes.Add(AnalyzeNumber());
                }
                else if (char.IsLetter(currentChar))
                {
                    lexemes.Add(AnalyzeWord());
                }
                else if (char.IsWhiteSpace(currentChar))
                {
                    MoveNext();
                }
                else if (currentChar == '=')
                {
                    MoveNext();
                    if (CurrentChar == '=')
                    {
                        lexemes.Add(new Lexeme("Оператор сравнения", "=="));
                        MoveNext();
                    }
                    else
                    {
                        lexemes.Add(new Lexeme("Присваивание", "="));
                    }
                }
                else if (IsBracket(currentChar))
                {
                    lexemes.Add(AnalyzeBracket());
                }
                else if (IsComparisonOperator(currentChar))
                {
                    lexemes.Add(AnalyzeComparisonOperator());
                }
                else if (currentChar == ';')
                {
                    MoveNext();
                    lexemes.Add(new Lexeme("Символ конца строки", ";"));
                }
                else if (currentChar == ',')
                {
                    MoveNext();
                    lexemes.Add(new Lexeme("Разделительный символ", ","));
                }
                else if (IsOperator(currentChar))
                {
                    lexemes.Add(AnalyzeOperation());
                }
                else
                {
                    throw new Exception($"Неизвестный символ: {currentChar}");
                }
            }

            return lexemes;
        }

        private bool IsBracket(char c)
        {
            return c == '(' || c == ')' || c == '{' || c == '}' || c == '[' || c == ']';
        }

        private bool IsComparisonOperator(char c)
        {
            return c == '>' || c == '<' || c == '=' || c == '!';
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private bool IsEnglishLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        private Lexeme AnalyzeNumber()
        {
            string number = "";

            while (_currentIndex < _inputData.Length && char.IsDigit(CurrentChar))
            {
                number += CurrentChar;
                MoveNext();
            }

            return new Lexeme("Число", number);
        }

        private Lexeme AnalyzeWord()
        {
            string word = "";

            while (_currentIndex < _inputData.Length && (IsEnglishLetter(CurrentChar) || char.IsDigit(CurrentChar) || CurrentChar == '_'))
            {
                word += CurrentChar;
                MoveNext();
            }

            if (_reservedKeywords.Contains(word))
            {
                return new Lexeme("Зарезервированное слово", word);
            }

            return new Lexeme("Идентификатор", word);
        }

        private Lexeme AnalyzeBracket()
        {
            var bracket = CurrentChar.ToString();
            MoveNext();
            return new Lexeme("Скобки", bracket);
        }

        private Lexeme AnalyzeComparisonOperator()
        {
            string op = CurrentChar.ToString();
            MoveNext();

            if (_currentIndex < _inputData.Length && op == "!")
            {
                if (CurrentChar != '=')
                {
                    throw new Exception($"Неожиданный символ '!' без '='");
                }
                else
                {
                    op += CurrentChar;
                    MoveNext();
                }
            }
            else if (_currentIndex < _inputData.Length && (op == ">" || op == "<" || op == "="))
            {
                if (CurrentChar == '=')
                {
                    op += CurrentChar;
                    MoveNext();
                }
            } 

            return new Lexeme("Оператор сравнения", op);
        }




        private Lexeme AnalyzeOperation()
        {
            string operation = CurrentChar.ToString();
            MoveNext();
            if (_currentIndex < _inputData.Length && IsOperator(CurrentChar))
            {
                operation += CurrentChar;
                MoveNext();
            }
            return new Lexeme("Знак операции", operation);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите строку для анализа:");
            string inputString = Console.ReadLine();

            LexicalAnalyzer analyzer = new LexicalAnalyzer(inputString);
            List<Lexeme> lexemes = analyzer.Analyze();

            Console.WriteLine("Результат анализа:");
            foreach (var lexeme in lexemes)
            {
                Console.WriteLine($"Тип: {lexeme.Type}, Значение: {lexeme.Value}");
            }
        }
    }
}
