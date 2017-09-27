﻿using Chinook.Data.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Data.DTOs
{
    public class AlbumDTO
    {
        public string title { get; set; }
        public int releaseyear { get; set; }
        public int numberoftracks { get; set; }
        public IEnumerable<TrackPOCO> tracks { get; set; }
    }
}
