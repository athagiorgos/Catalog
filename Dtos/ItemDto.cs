using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Dtos
{
    public class ItemDto
    {
        public Guid Id { get; init; } // init after the creation you can no longer modify this

        public string Name { get; set; }

        public decimal Price { get; init; }

        public DateTimeOffset CreatedDate { get; init; }
    }
}