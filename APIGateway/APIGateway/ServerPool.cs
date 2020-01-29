using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway
{
    public class ServerPool
    {
        private List<string> servers = new List<string>();
        int index = 0;
        public void Add(string item)
        {
            servers.Add(item);
        } 
        public void Remove(string item)
        {
            servers.Remove(item);
        }
        public string Next()
        {

            if (IsEmpty())
                return null;
            if (index < servers.Count)
                return servers[index++];
            else
            {
                index = 0;
                return servers[index++];
                
            }
                
        }
        public bool IsEmpty()
        {
            return servers.Count() == 0;
        }
    }
}
