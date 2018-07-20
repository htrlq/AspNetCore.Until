using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication3.Controllers
{
    public class Informattor
    {
        public string PcName { get; set; }
        public string UserName { get; set; }
        public string Version { get; set; }
    }

    public class InformattorStamp
    {
        public string PcName { get; set; }
        public string UserName { get; set; }
        public string Version { get; set; }
        public long Stamp { get; set; }
    }
}
