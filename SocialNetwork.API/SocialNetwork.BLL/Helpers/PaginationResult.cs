using System.Collections.Generic;

namespace SocialNetwork.BLL.Helpers
{
    public class PaginationResult<T>
    {
        public PaginationInfo Information { get; set; }
        public IList<T> Result { get; set; }
    }
}
