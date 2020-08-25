using System.Collections.Generic;
using System.Linq;
using ALTechTest.Helpers;

namespace ALTechTest.ViewModels.Artist
{
    public class ArtistsViewModel
    {
        public ArtistsViewModel(IEnumerable<Classes.MusicBrainz.Artist> artists)
        {
            Artists = artists.Select(x => new Models.Artist(x)).Distinct(new ArtistComparer());
        }

        public IEnumerable<Models.Artist> Artists { get; set; }
    }
}