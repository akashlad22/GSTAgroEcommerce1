using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroEcommerceLibrary.Admin
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string AlternateMobileNo { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int PinCode { get; set; }
        public int StatusId { get; set; }
    }
}
