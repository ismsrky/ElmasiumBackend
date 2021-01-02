namespace Mh.Service.Dto.Person.Shop
{
    public class ShopWorkingHoursDto
    {
        public long PersonId { get; set; }
        /*
      * -1       : shop not open.
      * 00000000 : shop never closes.
      * 09301945 : opens at 09:30 a.m., closes at 19:45(or 07:45 p.m.)
      */
        public string MonStartEnd { get; set; }
        public string TuesStartEnd { get; set; }
        public string WedStartEnd { get; set; }
        public string ThursStartEnd { get; set; }
        public string FriStartEnd { get; set; }
        public string SatStartEnd { get; set; }
        public string SunStartEnd { get; set; }

        public bool TakesOrderOutTime { get; set; }
    }
}