using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.BLL
{
   [DataObject]
    public class GenreController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<GenreDTO> Genre_GenreAlbumTracks()
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Genres
                              select new GenreDTO
                              {
                                  genre = x.Name,
                                  albums = from y in x.Tracks
                                           group y by y.Album into groupresults
                                           select new AlbumDTO
                                           {
                                               title = groupresults.Key.Title,
                                               releaseyear = groupresults.Key.ReleaseYear,
                                               numberoftracks = groupresults.Count(),
                                               tracks = from z in groupresults
                                                        select new TrackPOCO
                                                        {
                                                            song = z.Name,
                                                            milliseconds = z.Milliseconds // receiving fields on the select become the fields in the classes below
                                                        }

                                           }
                              };
                return results.ToList();
            }
        }
    }
}
