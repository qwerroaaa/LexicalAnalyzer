using LexicalAnalyzer.types;

namespace LexicalAnalyzer
{
    public static class Program
    {
        static void Main()
        {
            string? inputString = Console.ReadLine();

            if (inputString == null) 
            {
                Console.WriteLine("Ошибка в входной строке");
            } else
            {
                Input.Analyse(inputString);
            }

            foreach (var lexems in InputData.lexems)
            {
                Console.WriteLine($"Распознана лексема: {lexems.Item1} значение: {lexems.Item2}");
            }
        }
    }
}