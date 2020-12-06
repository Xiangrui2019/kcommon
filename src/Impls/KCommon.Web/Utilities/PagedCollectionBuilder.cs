using KCommon.Core.Abstract.Pager;
using KCommon.Core.Extensions;
using KCommon.Web.ErrorCode;
using KCommon.Web.Models.Message;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCommon.Web.Utilities
{
    public static class PagedCollectionBuilder
    {
        public static async Task<MessagePagedCollection<T>> BuildPagedMessageAsync<T>(
            IOrderedQueryable<T> query,
            IPageable pager,
            ErrorType code,
            string message)
        {
            var items = await BuildPagedAsync(query, pager);
            return new MessagePagedCollection<T>(items)
            {
                TotalCount = await query.CountAsync(),
                CurrentPage = pager.PageNumber,
                CurrentPageSize = pager.PageSize,
                Code = code,
                Message = message
            };
        }

        public static async Task<List<T>> BuildPagedAsync<T>(
            IOrderedQueryable<T> query,
            IPageable pager)
        {
            var items = await query.Page(pager).ToListAsync();
            return items;
        }
    }
}