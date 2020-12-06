using System;
using System.Collections.Generic;

namespace KCommon.Web.Models.Message
{
    public class MessageCollection<T> : MessageModel
    {
        [Obsolete("This method is only for framework", true)]
        public MessageCollection()
        {
        }

        public MessageCollection(List<T> items)
        {
            Items = items;
        }

        public List<T> Items { get; set; }
    }
}