using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommon.Web.Abstract.EntityframeworkCore.Interfaces
{
    public interface IPageable
    {
        [Range(1, int.MaxValue)]
        int PageNumber { get; set; }

        [Range(1, int.MaxValue)]
        int PageSize { get; set; }
    }
}
