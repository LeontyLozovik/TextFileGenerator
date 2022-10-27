using System.Text;

namespace TextFileGenerator
{
    public class FilesMerger
    {
        public const string directoryPath = "C:\\B1_Test_Task\\TextFileGenerator\\GeneratedFiles";
        public const string commonFilePath = "C:\\B1_Test_Task\\TextFileGenerator\\GeneratedFiles\\CommonFile.txt";
        public void MergeFiles(string? pattern) //Функция объединения файлов в общий
        {
            if(File.Exists(commonFilePath))
                File.Delete(commonFilePath);

            var files = Directory.GetFiles(directoryPath); //Получение всех файлов
            foreach (var file in files)
            {
                var content = ReadContent(file, pattern); //Чтение содержимого
                WriteContent(content);                    //Запись в общий файл
            }
        }
        public static List<string> ReadContent(string filename, string? pattern) 
        {
            List<string> content = new List<string>();
            int deleted = 0;
            using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (TextReader textReader = new StreamReader(fileStream, Encoding.Default))
                {
                    string oneString;
                    while ((oneString = textReader.ReadLine()!) != null)
                    {
                        if (string.IsNullOrEmpty(pattern) || string.IsNullOrWhiteSpace(pattern))
                            content.Add(oneString);
                        else if (!oneString.Contains(pattern)) //Если строка существует и в неё не входит патерн поиска строка добавляется в результирующий список
                        {
                            content.Add(oneString);
                        }
                        else
                        {
                            deleted++; //Подсчёт удалённых строк
                        }
                    }         
                }
            }

            if (deleted != 0)
                Console.WriteLine($"{deleted} strings were deleted from file {filename}");

            return content;
        }
        private void WriteContent(List<string> content)
        {
            using (FileStream fileStream = new FileStream(commonFilePath, FileMode.Append))
            {
                using (TextWriter textWriter = new StreamWriter(fileStream, Encoding.Default))
                {
                    foreach (var strings in content)
                    {
                        textWriter.WriteLine(strings); //Запись данных в общий файл
                    }
                }
            }
        }
    }
}