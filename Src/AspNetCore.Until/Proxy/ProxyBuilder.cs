using System.Collections.Generic;

namespace AspNetCore.Until.Proxy
{
    public class ProxyBuilder
    {
        public List<ProxyItem> Items { get; set; }

        public ProxyBuilder()
        {
            Items = new List<ProxyItem>();
        }
    }
}