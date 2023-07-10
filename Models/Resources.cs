using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finops.Models
{
    public class Resources
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public int Id { get; set; }
        // Other properties...

        public ICollection<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}