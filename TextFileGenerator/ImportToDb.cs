using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TextFileGenerator.DBContext;
using TextFileGenerator.Mapper;
using TextFileGenerator.Models;

namespace TextFileGenerator
{
    public class ImportToDb
    {
        public static void Import(ApplicationDBContext applicationDB)
        {
            var mapper = MyMapper.CreateMapper();
            var files = Directory.GetFiles(FilesMerger.directoryPath);      //Получение всех файлов
            foreach (var file in files)
            {
                if (file.Equals(FilesMerger.commonFilePath))
                    continue;

                List<string> listOfStrings = FilesMerger.ReadContent(file, null);       //Получение контента из файлов
                int counter = 0;
                DateTime timeStart = DateTime.Now;

                foreach (var str in listOfStrings)
                {
                    var stringModel = StringModel.FromStringToModel(str);
                    var dbModel = mapper.Map<DBModel>(stringModel);
                    applicationDB.Record.Add(dbModel);                          //Запись строки в базу данны
                    counter++;

                    if ((DateTime.Now.Ticks - timeStart.Ticks) > 500_000)       //Вывод процесса загрузки 
                    {
                        Console.Clear();
                        Console.WriteLine($"From {file.Remove(0, FilesMerger.directoryPath.Length)} Downloaded: {counter}\tLeft: {listOfStrings.Count - counter}");
                        timeStart = DateTime.Now;
                    }
                }
                Console.Clear();
                Console.WriteLine($"From {file.Remove(0, FilesMerger.directoryPath.Length)} Downloaded: {counter}\tLeft: {listOfStrings.Count - counter}");
                Console.Clear();
                applicationDB.SaveChanges();
            }
        }

        public static DbContextOptions<ApplicationDBContext> CreateDbOptions()      //Подключение базы данных
        {
            var builder = new ConfigurationBuilder();
            var connectionString = builder.SetBasePath(Directory.GetCurrentDirectory())
                                          .AddJsonFile("appconfig.json")
                                          .Build()
                                          .GetConnectionString("SQLConnection");

            var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseSqlServer(connectionString).Options;
            return options;
        }
    }
}
