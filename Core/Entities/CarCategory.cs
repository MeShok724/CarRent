﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeGen.Core.TypeAnnotations;

namespace Core.Entities
{
    [ExportTsClass]
    public class CarCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
