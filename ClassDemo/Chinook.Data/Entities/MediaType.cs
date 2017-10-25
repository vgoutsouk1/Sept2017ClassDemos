
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chinook.Data.Entities
{
    [Table("MediaTypes")]
    public class MediaType
    {
        [Key]

        public int MediaTypeId { get; set; }

        [StringLength(120)]
        public string Name { get; set; }


    }
}
