using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class CarReview
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public int? CustomerId { get; set; }
        public User? Customer { get; set; }

        public int? OrderId { get; set; }
        public Order? Order { get; set; }

        public int? Rating { get; set; }
        public string? ReviewText { get; set; }

        public DateTime? AddedAt { get; set; }
    }
}
