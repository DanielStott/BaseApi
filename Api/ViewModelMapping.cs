using AutoMapper;

namespace BaseApi
{
    public static class ViewModelMapping
    {
        public static IMapper Get()
        {
            return new MapperConfiguration(
                cfg => cfg.AddMaps(typeof(Profile)))
                .CreateMapper(); 
        }
    }
}