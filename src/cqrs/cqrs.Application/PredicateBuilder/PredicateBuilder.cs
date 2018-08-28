using System;
using System.Linq.Expressions;

namespace cqrs.Application.PredicateBuilder
{
    public static class PredicateBuilder
    {
        private enum BinaryOperators
        {
            And,
            Or
        }

        public static Expression<Func<T, bool>> True<T>() { return x => true; }

        public static Expression<Func<T, bool>> False<T>() { return x => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return expr1 == null
                ? expr2
                : Build(expr1, expr2, BinaryOperators.Or);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            return expr1 == null
                ? expr2
                : Build(expr1, expr2, BinaryOperators.And);
        }

        private static Expression<Func<T, bool>> Build<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2, BinaryOperators binaryOperator)
        {
            var body = binaryOperator == BinaryOperators.And
                ? Expression.AndAlso(expr1.Body, expr2.Body)
                : Expression.OrElse(expr1.Body, expr2.Body);

            var parameterExpression = Expression.Parameter(typeof(T));
            body = (BinaryExpression)new ParameterReplacer(parameterExpression).Visit(body);
            return Expression.Lambda<Func<T, bool>>(body, parameterExpression);
        }
    }
}
