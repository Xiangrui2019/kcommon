using System.ComponentModel.DataAnnotations;

namespace KCommon.Core.Abstract.Models
{
    public interface IPageable
    {
        [Range(1, int.MaxValue)]
        int PageNumber { get; set; }

        [Range(1, int.MaxValue)]
        int PageSize { get; set; }
    }
}