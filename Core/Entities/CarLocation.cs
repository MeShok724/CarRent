﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class CarLocation
    {
        public int Id { get; set; }

        public int? CarId { get; set; }
        public Car? Car { get; set; }

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
