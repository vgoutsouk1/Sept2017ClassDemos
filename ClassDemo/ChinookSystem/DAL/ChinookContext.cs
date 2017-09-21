using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region Additional Namespaces
using System.Data.Entity;
using Chinook.Data.Entities;
#endregion

namespace ChinookSystem.DAL
{
    internal class ChinookContext:DbContext
    {
        public ChinookContext(): base("name=ChinookDB")
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<MediaType> MediaTypes { get; set; }
        public virtual DbSet<Playlist> Playlists { get; set; }
        public virtual DbSet<PlaylistTrack> PlaylistTracks { get; set; }
        public virtual DbSet<Track> Tracks { get; set; }
    }
}
