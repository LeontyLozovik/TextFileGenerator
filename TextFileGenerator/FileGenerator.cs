using System.Text;
using TextFileGenerator.Models;

namespace TextFileGenerator
{
    public class FileGenerator
    {
        public string FilePath { get; set; }
        public int FileNumber { get; set; }
        public List<StringModel> Content { get; set; }

        public FileGenerator(int fileNumber, List<StringModel> content)
        {
            FileNumber = fileNumber;
            Content = content;
            FilePath = $"C:\\B1_Test_Task\\TextFileGenerator\\GeneratedFiles\\file{FileNumber}.txt";
        }
        public void GenerateFile()        //Функция генерация одного файла
        {
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
            {
                using (TextWriter textWriter = new StreamWriter(fileStream, Encoding.Default))
                {
                    foreach (var strings in Content)
                    { 
                        textWriter.WriteLine(strings);
                    }
                }
            }
        }
    }
}
