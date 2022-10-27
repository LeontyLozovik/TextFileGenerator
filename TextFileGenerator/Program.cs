using TextFileGenerator;
using TextFileGenerator.DBContext;
using TextFileGenerator.Models;

const int NUMBER_OF_FILES = 2;
const int NUMBER_OF_STRINGS = 100_000;

List<StringModel> content = new List<StringModel>();

for (int i = 0; i < NUMBER_OF_FILES; i++)       //Процесс генерации файлов
{
    for (int j = 0; j < NUMBER_OF_STRINGS; j++)
    {
        StringModel stringModel = new StringModel();
        content.Add(stringModel);
    }
    FileGenerator generator = new FileGenerator(i + 1, content);
    generator.GenerateFile();       //Непосредственный вызов генератора файлов
    content.Clear();
}

var options = ImportToDb.CreateDbOptions();         //Подключение базы данных
ApplicationDBContext applicationDB = new ApplicationDBContext(options);

Console.WriteLine($"{NUMBER_OF_FILES} files with {NUMBER_OF_STRINGS} strings where generated\n");

do          //Меню
{
    Console.WriteLine("1 - Merge all files in one\n2 - save files in database\n3 - requests from database\nother - exit");
    int choice = 0;
    bool notExit = Int32.TryParse(Console.ReadLine(), out choice);
    if (notExit)
    {
        switch (choice)
        {
            case 1:
                {
                    Console.Write("Enter a pattern whith which to delete lines. If there is no pattern tap 'Enter' : ");
                    string? pattern = Console.ReadLine();

                    FilesMerger filesMerger = new FilesMerger();
                    try 
                    {
                        filesMerger.MergeFiles(pattern);        //Объединение файлов
                    }
                    catch
                    {
                        Console.WriteLine("Error during mergining files");
                    }
                    break;
                }
            case 2:
                {
                    try
                    {
                        ImportToDb.Import(applicationDB);       //Импорт в базу данных
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error during imopring files to database");
                    }
                    break;
                }
            case 3:
                {
                    try
                    {
                        SqlRequests.BothRequests(applicationDB);    //Выполнени SQL запросов
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Something went wrong! Maby you forgot to write records to the database");
                    }
                    break;
                }
            default:
                {
                    return;
                }
        }           
    }
    else
        break;
}
while (true);