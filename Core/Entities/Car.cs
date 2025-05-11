using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class Car
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }
        public string? Vin { get; set; }
        public string? LicensePlate { get; set; }
        public string? Color { get; set; }
        public int? Mileage { get; set; }

        public int? CategoryId { get; set; }
        public CarCategory? Category { get; set; }

        public int? StatusId { get; set; }
        public CarStatus? Status { get; set; }

        public int? BranchId { get; set; }
        public Branch? Branch { get; set; }

        public decimal? RentalPricePerDay { get; set; }
        public decimal? EngineVolume { get; set; }
        public int? Seats { get; set; }
        public DateTime? AddedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Order>? Orders { get; set; }
        public ICollection<CarImage>? CarImages { get; set; }
    }
}
