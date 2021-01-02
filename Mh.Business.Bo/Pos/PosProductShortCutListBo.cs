using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Business.Bo.Pos
{
    public class PosProductShortCutListBo
    {
        public int Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}