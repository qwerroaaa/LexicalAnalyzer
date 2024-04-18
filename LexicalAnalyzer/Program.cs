using LexicalAnalyzer.types;

namespace LexicalAnalyzer
{
    public static class Program
    {
        static void main()
        {
            string? inputString = Console.ReadLine();

            if (inputString == null) 
            {
                Console.WriteLine("Ошибка в входной строке");
            } else
            {
                Input.Analyse(inputString);
            }

            foreach (var ls in InputData.lexems)
            {
                Console.WriteLine($"Распознана лексема: {ls.Item1} значение: {ls.Item2}");
            }
        }
    }
}