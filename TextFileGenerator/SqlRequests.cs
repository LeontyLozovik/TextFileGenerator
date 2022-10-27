using System.Linq;
using TextFileGenerator.DBContext;

namespace TextFileGenerator
{
    public static class SqlRequests
    {
        public static void BothRequests(ApplicationDBContext applicationDB)     //Последовательный вызов обоих запросов
        {
            Console.WriteLine($"All integer numbers sum: {GetSumOfInt(applicationDB)}");
            Console.WriteLine($"All real numbers average: {GetAverageReal(applicationDB)}");
            Console.WriteLine();
        }
        private static long GetSumOfInt(ApplicationDBContext applicationDB)      //Получение суммы целых чисел
        {
            long averageInt = (from items in applicationDB.Record select (long)(items.Integer)).Sum();
            return averageInt;
        }
        private static double GetAverageReal(ApplicationDBContext applicationDB)        //Получение среднего значеня дробных чисел
        {
            var averageReal = applicationDB.Record.Average(r => r.Real);
            return averageReal;
        }
    }
}
