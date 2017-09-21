
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Data.Entities
{
    [Table("PlaylistTracks")]
    public class PlaylistTrack
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlaylistId { get; set; }

        [Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrackId { get; set; }

        public int TrackNumber { get; set; }


    }
}
