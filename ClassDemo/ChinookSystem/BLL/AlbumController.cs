
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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Albums_ListByTitle(string title)
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.Albums
                              where x.Title.Contains(title)
                              orderby x.Title, x.ReleaseYear
                              select x;
                return results.ToList();
            }
        }//eom

        [DataObjectMethod(DataObjectMethodType.Insert,false)]
        public int Albums_Add(Album item)
        {
            using (var context = new ChinookContext())
            {
                item = context.Albums.Add(item); // the staging command
                context.SaveChanges(); //puts the item instance on the database
                return item.AlbumId; //  is returned because the system generates a pkey for the item
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public int Albums_Update(Album item)
        {
            using (var context = new ChinookContext())
            {
                context.Entry(item).State =
                    System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        public int Albums_Delete(int albumid)
        {
            using (var context = new ChinookContext())
            {
                var existingItem = context.Albums.Find(albumid);
                context.Albums.Remove(existingItem);
                return context.SaveChanges();

            }
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)] // extracting what we need and passing into the above delete
        public void Albums_Delete(Album item)
        {
            Albums_Delete(item.AlbumId);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Album> Albums_List()
        {
            using (var context = new ChinookContext())
            {
             
                return context.Albums.ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Album Albums_Get(int albumid)
        {
            using (var context = new ChinookContext())
            {

                return context.Albums.Find(albumid);
            }
        }

    }
}

