using System.Collections.Generic;
using ALTechTest.DataTransferObjects;

namespace ALTechTest.Helpers
{
    public class ArtistComparer : IEqualityComparer<ArtistDto>
    {
        public bool Equals(ArtistDto leftArtist, ArtistDto rightArtist)
        {
            Verify.NotNull(leftArtist, nameof(leftArtist));
            Verify.NotNull(rightArtist, nameof(rightArtist));

            return leftArtist.Id.Equals(rightArtist.Id);
        }

        public int GetHashCode(ArtistDto value)
        {
            return value.GetHashCode();
        }
    }
}