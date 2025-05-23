﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class CarImage
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        [JsonIgnore]
        public Car? Car { get; set; }
        public string? ImageUrl { get; set; }
    }
}
