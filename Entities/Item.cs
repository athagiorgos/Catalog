using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Entities
{
    public record Item
    {
        public Guid Id { get; init; } // init after the creation you can no longer modify this

        public string Name { get; set; } 

        public decimal Price { get; set; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}