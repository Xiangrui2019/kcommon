using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using KCommon.Core.Abstract.Models;
using KCommon.Core.Utilities;
using KCommon.Core.Validations;

namespace KCommon.Core.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 根据第三方条件是否为真来决定是否执行指定条件的查询
        /// </summary>
        /// <param name="source"> 要查询的源 </param>
        /// <param name="predicate"> 查询条件 </param>
        /// <param name="condition"> 第三方条件 </param>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <returns> 查询的结果 </returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate,
            bool condition)
        {
            if (!new NotNull().IsValid(source)) throw new ArgumentNullException(nameof(source));

            if (!new NotNull().IsValid(predicate)) throw new ArgumentNullException(nameof(predicate));

            return condition ? source.Where(predicate) : source;
        }

        /// <summary>
        /// 把<see cref="IQueryable{T}"/>集合按指定字段与排序方式进行排序
        /// </summary>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <typeparam name="T">动态类型</typeparam>
        /// <returns>排序后的数据集</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            if (!new NotNull().IsValid(source)) throw new ArgumentNullException(nameof(source));

            if (!new NotNull().IsValid(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            return CollectionPropertySorter<T>.OrderBy(source, propertyName, sortDirection);
        }


        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合继续按指定字段排序方式进行排序
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="source">要排序的数据集</param>
        /// <param name="propertyName">排序属性名</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source,
            string propertyName,
            ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            if (!new NotNullOrEmpty().IsValid(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            if (!new NotNull().IsValid(source)) throw new ArgumentNullException(nameof(source));

            return CollectionPropertySorter<T>.ThenBy(source, propertyName, sortDirection);
        }

        /// <summary>
        /// 把<see cref="IOrderedQueryable{T}"/>集合分页
        /// </summary>
        /// <typeparam name="T">动态类型</typeparam>
        /// <param name="pager">输入</param>
        /// <returns></returns>
        public static IQueryable<T> Page<T>(this IOrderedQueryable<T> query, IPageable pager)
        {
            return query
                .Skip((pager.PageNumber - 1) * pager.PageSize)
                .Take(pager.PageSize);
        }
    }
}