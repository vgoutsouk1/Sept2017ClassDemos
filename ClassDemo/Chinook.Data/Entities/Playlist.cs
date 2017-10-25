
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Data.Entities
{
    [Table("Playlists")]
    public class Playlist
    {
        [Key]

        public int PlaylistId { get; set; }

        [Required]
        [StringLength(120)]
        public string Name { get; set; }

        [StringLength(120)]
        public string UserName { get; set; }

    }
}
