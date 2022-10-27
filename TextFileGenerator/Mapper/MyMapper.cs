using AutoMapper;

namespace TextFileGenerator.Mapper
{
    public static class MyMapper     //Создание маппера для приведения DBModels к StringModel и обратно
    {
        public static IMapper CreateMapper()
        {
            MapperConfiguration configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            configuration.AssertConfigurationIsValid();
            return configuration.CreateMapper();
        }
    }
}
