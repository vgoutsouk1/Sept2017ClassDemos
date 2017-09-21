<Query Kind="Expression">
  <Connection>
    <ID>00ea54ca-480e-49d3-9306-7c745651fafe</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//simple group
from genre in Genres
group genre by genre.Name

//grouping by multiple attributes
from track in Tracks
group track by new {track.Genre.Name, track.AlbumId} into gresult
select new
{
		id = gresult.Key.Name,
		song = gresult.Select(a => a.Name)
}

//saving your grouping to a temporary collection
//from parent to child
from genre in Genres
group genre by genre.Name into gresult
select new
{
	genreName = gresult.Key,
	tracks = from x in gresult.ToList()
			select new 
			{
				songs = from y in x.Tracks
						select y.Name
			}
}

//child using parent attribute
from track in Tracks
group track by track.Genre.Name into gresult
select new
{
	keyvalue = gresult.Key,
	songs = from x in gresult
			select new
			{
				Song = x.Name,
				albumtitle = x.Album.Title
			}
}

//grouping by the entire parent record
from track in Tracks
group track by track.Genre into gresult
select new
{
	keyvalue = gresult.Key.GenreId,
	songs = from x in gresult
			select new
			{
				Song = x.Name,
				albumtitle = x.Album.Title
			}
}

var results = from x in Albums
			select new
			{
				title = x.Title,
				decade = x.ReleaseYear > 1969 && x.ReleaseYear < 1980?"70s":
						x.ReleaseYear > 1979 && x.ReleaseYear < 1990?"80s":
						x.ReleaseYear > 1989 && x.ReleaseYear < 2000?"90s":
						"Modern"
			};

var resultavg = (from x in Tracks
				select x.Milliseconds).Average();
				
//resultavg.Dump();
var trackbalance = from x in Tracks
					select new
					{
					  song = x.Name,
					  length = x.Milliseconds > resultavg?"Long":
					           x.Milliseconds < resultavg?"Short":
							   "Average"
					};
trackbalance.Dump();

if (condition)
{
		value
}
else if (condition)
{
       value
}
else
{
		default value
}