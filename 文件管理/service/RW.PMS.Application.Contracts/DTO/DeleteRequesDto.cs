using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO
{
    public class DeleteRequesDto
    {
        public object Ids { get; set; }

        public int[] GetIds()
        {
            if (Ids == null) return new int[0];
            var ids = Ids.ToString();
            return ids.Split(',').Select(x => Convert.ToInt32(x)).ToArray();
        }
    }
}
