using System.Collections.Generic;
using System.Linq;
using ALTechTest.DataTransferObjects;
using ALTechTest.Helpers;

namespace ALTechTest.ViewModels
{
    public class ArtistsViewModel
    {
        public ArtistsViewModel(IEnumerable<ArtistDto> artists)
        {
            Artists = artists.Distinct(new ArtistComparer());
        }

        public IEnumerable<ArtistDto> Artists { get; set; }
    }
}