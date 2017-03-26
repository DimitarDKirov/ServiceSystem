using ServiceSystem.Infrastructure.Mapping.Contracts;

namespace ServiceSystem.Infrastructure.Mapping
{
    public class MappingService : IMappingService
    {
        public T Map<T>(object source)
        {
            return AutoMapperConfig.Configuration.CreateMapper().Map<T>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoMapperConfig.Configuration.CreateMapper().Map(source, destination);
        }
    }
}
