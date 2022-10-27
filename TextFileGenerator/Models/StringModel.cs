using System.Text;

namespace TextFileGenerator.Models
{
    public class StringModel    //Модель представления строки в файле
    {
        private Random rand = new Random();
        private const int lenghthOfString = 10;
        public DateTime Date { get; set; }
        public string Latine { get; set; }
        public string Cirilic { get; set; }
        public int Integer { get; set; }
        public double Real { get; set; }

        public StringModel()
        {
            Date = RandomDate();
            Latine = RandomString("latine");
            Cirilic = RandomString("cirilic");
            Integer = RandomInt();
            Real = RandomReal();
        }
        public override string ToString()        //Приведения класса StringModel к строке нужного формата
        {
            return Date.ToString("dd.MM.yyyy") + "||" + Latine + "||" + Cirilic + "||" + Integer.ToString() + "||" + Real.ToString() + "||";
        }
        public static StringModel FromStringToModel(string str)        //Приведение строки к классу StringModel
        {
            string[] values = str.Split("||");
            string[] stringDate = values[0].Split('.');

            StringModel resultStringModel = new StringModel
            {
                Date = new DateTime(Int32.Parse(stringDate[2]), Int32.Parse(stringDate[1]), Int32.Parse(stringDate[0])),
                Latine = values[1],
                Cirilic = values[2],
                Integer = Int32.Parse(values[3]),
                Real = Double.Parse(values[4])
            };

            return resultStringModel;
        }
        private DateTime RandomDate()        //Получение случайной даты
        {
            var startDate = new DateTime(2018, 1, 1);      //Задаваемый диопазон дат
            var endDate = DateTime.Now;

            var randomYear = rand.Next(startDate.Year, endDate.Year);
            var randomMonth = rand.Next(1, 12);
            var randomDay = rand.Next(1, DateTime.DaysInMonth(randomYear, randomMonth));

            //Проверка нижней гранцы
            //В данном случае оказалось не нужным

            //if (randomYear == startDate.Year)
            //{
            //    randomMonth = rand.Next(startDate.Month, 12);

            //    if (randomMonth == startDate.Month)
            //        randomDay = rand.Next(startDate.Day, DateTime.DaysInMonth(randomYear, randomMonth));
            //}

            if (randomYear == endDate.Year)            //Проверка верхней границы
            {
                randomMonth = rand.Next(1, endDate.Month);

                if (randomMonth == endDate.Month)
                    randomDay = rand.Next(1, endDate.Day);
            }

            DateTime randomDate = new DateTime(randomYear, randomMonth, randomDay);
            return randomDate;
        }
        private string RandomString(string alphabeth)        //Получение случайной строки
        {
            StringBuilder resultString = new StringBuilder();
            if (alphabeth.Equals("latine"))
            {
                for (int i = 0; i < lenghthOfString; i++)
                {
                    int caseRand = rand.Next(0, 10);
                    if (caseRand >= 5)
                    {
                        resultString.Append((char)rand.Next(0x0041, 0x005A));       //Диопазон заглавных латинский букв
                    }
                    else
                    {
                        resultString.Append((char)rand.Next(0x0061, 0x007A));       //Диопазон строчных латиских символов
                    }
                }
            }
            else if (alphabeth.Equals("cirilic"))
            {
                for (int i = 0; i < lenghthOfString; i++)
                {
                    resultString.Append((char)rand.Next(0x0410, 0x044F));          //Диопазон русского алфавита
                }
            }

            return resultString.ToString();
        }
        private int RandomInt()        //Получение случайного целого числа
        {
            return rand.Next(1, 100_000_000);
        }
        private double RandomReal()        //Получение случайного дробного числа
        {
            return Math.Round(rand.NextDouble() + rand.Next(1, 20), 8);
        }
    }
}
