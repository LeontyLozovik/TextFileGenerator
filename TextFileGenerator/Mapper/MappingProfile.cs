using AutoMapper;
using TextFileGenerator.Models;

namespace TextFileGenerator.Mapper
{
    public class MappingProfile : Profile    //Класс профиля для маппинга
    {
        public MappingProfile()
        {
            CreateMap<DBModel, StringModel>().ReverseMap();    //Правило маппинга
        }
    }
}
