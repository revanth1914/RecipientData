using System.Collections.Generic;

namespace TestProject
{
    public class Recipient
    {
        public List<string> tags { get; set; }
        public string name { get; set; }
        public int id { get; set; }
    }

    public class JsonMapper
    {
        public List<Recipient> recipients { get; set; }
    }
}