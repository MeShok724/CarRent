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
    public class UserStatus
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<User>? Users { get; set; }
    }
}
