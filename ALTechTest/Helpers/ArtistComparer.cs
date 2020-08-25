using System.Collections.Generic;
using ALTechTest.Models;

namespace ALTechTest.Helpers
{
    public class ArtistComparer : IEqualityComparer<Artist>
    {
        public bool Equals(Artist leftArtist, Artist rightArtist)
        {
            Verify.NotNull(leftArtist, nameof(leftArtist));
            Verify.NotNull(rightArtist, nameof(rightArtist));

            return leftArtist.Id.Equals(rightArtist.Id);
        }

        public int GetHashCode(Artist value)
        {
            return value.GetHashCode();
        }
    }
}