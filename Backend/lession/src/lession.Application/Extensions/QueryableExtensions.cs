using lession.Application.DTOs.Common;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;


namespace lession.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
            this IQueryable<T> source,
            int pageNumber,
            int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(items, count, pageNumber, pageSize);
        }

        public static IQueryable<T> ApplyOrdering<T>(
            this IQueryable<T> source,
            string? orderBy,
            bool isDescending,
            Dictionary<string, Expression<Func<T, object>>> orderMappings)
        {
            if (string.IsNullOrWhiteSpace(orderBy) || !orderMappings.ContainsKey(orderBy.ToLower()))
                return source;

            var expression = orderMappings[orderBy.ToLower()];
            return isDescending
                ? source.OrderByDescending(expression)
                : source.OrderBy(expression);
        }
    }
}
