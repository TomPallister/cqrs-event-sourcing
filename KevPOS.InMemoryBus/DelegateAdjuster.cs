using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KevPOS.InMemoryBus
{
    public class DelegateAdjuster
    {
        public static Func<TBaseT, Task> CastArgument<TBaseT, TDerivedT>(Expression<Func<TDerivedT, Task>> source)
            where TDerivedT : TBaseT
        {
            if (typeof (TDerivedT) == typeof (TBaseT))
            {
                return (Func<TBaseT, Task>) ((Delegate) source.Compile());
            }
            ParameterExpression sourceParameter = Expression.Parameter(typeof (TBaseT), "source");
            Expression<Func<TBaseT, Task>> result = Expression.Lambda<Func<TBaseT, Task>>(
                Expression.Invoke(
                    source,
                    Expression.Convert(sourceParameter, typeof (TDerivedT))),
                sourceParameter);
            return result.Compile();
        }
    }
}