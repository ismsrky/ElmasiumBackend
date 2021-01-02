using System;

namespace Mh.Business.Bo.Sys
{
    public class SysVersionBo
    {
        public int Id { get; set; }
        public Enums.ApplicationTypes ApplicationTypeId { get; set; }
        public int Version { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseNote { get; set; } // not null. Lenght is max.
    }
}