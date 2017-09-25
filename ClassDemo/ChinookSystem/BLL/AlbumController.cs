
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region additinal namesapces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using Chinook.Data.POCOs;
using System.ComponentModel;
#endregion
namespace ChinookSystem.BLL
{
    [DataObject]
    public class AlbumController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ArtistAlbumsByReleaseYear> Albums_ByArtist(int artistid) // this list class must be the same as on the bottom
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums // dont forget to add context.
                              where x.ArtistId.Equals(artistid)
                              select new ArtistAlbumsByReleaseYear // after new add the Poco class that contains the properies below
                                                                   // with a get;set;
                              {
                                  Title = x.Title,
                                  Released = x.ReleaseYear
                              };
                return results.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Albums_FindByYearRange(int minyear, int maxyear)
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              where x.ReleaseYear >= minyear && x.ReleaseYear <= maxyear
                              orderby x.ReleaseYear, x.Title
                              select x;
                return results.ToList();
            }
        }
    }
}

