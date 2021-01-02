using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mh.Service.Dto.EnumsOp
{
    public class FicheTypeFakeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Enums.FicheTypes FicheTypeId { get; set; }
    }
}