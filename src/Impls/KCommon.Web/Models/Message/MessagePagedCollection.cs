using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KCommon.Web.Models.Message
{
    public class MessagePagedCollection<T> : MessageCollection<T>
    {
        [Obsolete("This method is only for framework", true)]
        public MessagePagedCollection()
        {
        }

        public MessagePagedCollection(List<T> items) : base(items)
        {
        }

        public int TotalCount { get; set; }

        [Range(1, int.MaxValue)] public int CurrentPage { get; set; }

        [Range(1, int.MaxValue)] public int CurrentPageSize { get; set; }
    }
}