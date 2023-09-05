using AutoMapper;

namespace Meetings4IT.Shared.Implementations.AutoMapper;

public static class Mapping
{
    public static TTarget Map<TSource, TTarget>(this TSource value)
        where TSource : class
        where TTarget : class
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<TSource, TTarget>());

        var mapper = new Mapper(config);
        var result = mapper.Map<TSource, TTarget>(value);

        return result;
    }

    public static List<TTarget> MapList<TSource, TTarget>(this List<TSource> value)
        where TSource : class
        where TTarget : class
    {
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<TSource, TTarget>());

        var mapper = new Mapper(config);
        var result = mapper.Map<List<TSource>, List<TTarget>>(value);

        return result;
    }
}