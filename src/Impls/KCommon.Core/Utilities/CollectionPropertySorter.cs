using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using KCommon.Core.Validations;

namespace KCommon.Core.Utilities
{
    public class CollectionPropertySorter<T>
    {
        // ReSharper disable StaticFieldInGenericType
        private static readonly ConcurrentDictionary<string, LambdaExpression> Cache =
            new ConcurrentDictionary<string, LambdaExpression>();

        /// <summary>
        /// 按指定的属性名称对<see cref="IEnumerable{T}"/>序列进行排序
        /// </summary>
        /// <param name="source"><see cref="IEnumerable{T}"/>序列</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="sortDirection">排序方向</param>
        public static IOrderedEnumerable<T> OrderBy(IEnumerable<T> source, string propertyName,
            ListSortDirection sortDirection)
        {
            if (!new NotNullOrEmpty().IsValid(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            dynamic expression = GetKeySelector(propertyName);
            var keySelector = expression.Compile();
            return sortDirection == ListSortDirection.Ascending
                ? Enumerable.OrderBy(source, keySelector)
                : Enumerable.OrderByDescending(source, keySelector);
        }

        /// <summary>
        /// 按指定的属性名称对<see cref="IOrderedEnumerable{T}"/>进行继续排序
        /// </summary>
        /// <param name="source"><see cref="IOrderedEnumerable{T}"/>序列</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="sortDirection">排序方向</param>
        public static IOrderedEnumerable<T> ThenBy(IOrderedEnumerable<T> source, string propertyName,
            ListSortDirection sortDirection)
        {
            if (!new NotNullOrEmpty().IsValid(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            dynamic expression = GetKeySelector(propertyName);
            var keySelector = expression.Compile();
            return sortDirection == ListSortDirection.Ascending
                ? Enumerable.ThenBy(source, keySelector)
                : Enumerable.ThenByDescending(source, keySelector);
        }

        /// <summary>
        /// 按指定的属性名称对<see cref="IQueryable{T}"/>序列进行排序
        /// </summary>
        /// <param name="source">IQueryable{T}序列</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy(IQueryable<T> source, string propertyName,
            ListSortDirection sortDirection)
        {
            if (!new NotNullOrEmpty().IsValid(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            dynamic keySelector = GetKeySelector(propertyName);
            return sortDirection == ListSortDirection.Ascending
                ? Queryable.OrderBy(source, keySelector)
                : Queryable.OrderByDescending(source, keySelector);
        }

        /// <summary>
        /// 按指定的属性名称对<see cref="IOrderedQueryable{T}"/>序列进行排序
        /// </summary>
        /// <param name="source">IOrderedQueryable{T}序列</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> source, string propertyName,
            ListSortDirection sortDirection)
        {
            if (!new NotNullOrEmpty().IsValid(propertyName)) throw new ArgumentNullException(nameof(propertyName));

            dynamic keySelector = GetKeySelector(propertyName);
            return sortDirection == ListSortDirection.Ascending
                ? Queryable.ThenBy(source, keySelector)
                : Queryable.ThenByDescending(source, keySelector);
        }

        private static LambdaExpression GetKeySelector(string keyName)
        {
            var type = typeof(T);
            var key = type.FullName + "." + keyName;
            if (Cache.ContainsKey(key)) return Cache[key];
            var param = Expression.Parameter(type);
            var propertyNames = keyName.Split('.');
            Expression propertyAccess = param;
            foreach (var propertyName in propertyNames)
            {
                var property = type.GetProperty(propertyName);
                if (property == null) throw new Exception($"Property name not exists in {property.Name}");
                type = property.PropertyType;
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            }

            var keySelector = Expression.Lambda(propertyAccess, param);
            Cache[key] = keySelector;
            return keySelector;
        }
    }
}