using System;

namespace WebAPI.API.Common
{
    public static class ObjectExstentions
    {
        public static TSource Apply<TSource>(this TSource source, Func<TSource, TSource> functor)
          => functor(source);
        public static TOut Bind<TSource, TOut>(this TSource _, TOut returnValue)
           => returnValue;
    }
}
