using ALTechTest.DataTransferObjects;
using ALTechTest.Interfaces;
using ALTechTest.ServiceCallers;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ALTechTestTests
{
    public class LyricsOvhCallerTests
    {
        private const string _invalidGetResponse =
            "<!DOCTYPE html>\n<html lang=\"en\">\n<head>\n<meta charset=\"utf-8\">\n<title>Error</title>\n</head>\n<body>\n<pre>Cannot GET /v1/Invalid/Query</pre>\n</body>\n</html>\n";

        private const string _validGetResponse =
            "{\"lyrics\":\"I'm gonna pick up the pieces,\\nand build a lego house\\nif things go wrong we can knock it down\\n\\nMy three words have two meanings,\\nthere's one thing on my mind\\n\\nIt's all for you\\n\\n\\n\\nAnd it's dark in a cold December, \\n\\nbut I've got ya to keep me warm\\n\\nand if you're broken I will mend ya and keep you sheltered\\n\\nfrom the storm that's raging on now\\n\\n\\n\\nI'm out of touch, I'm out of love\\n\\nI'll pick you up when you're getting down\\n\\nand out of all these things I've done \\n\\nI think I love you better now\\n\\n\\n\\nI'm out of sight, I'm out of mind\\n\\nI'll do it all for you in time\\n\\nAnd out of all these things I've done \\n\\nI think I love you better now, now\\n\\n\\n\\nI'm gonna paint you by numbers\\n\\nand colour you in\\n\\nif things go right we can frame it, and put you on a wall\\n\\n\\n\\nAnd it's so hard to say it but I've been here before\\n\\nand I will surrender up my heart\\n\\nand swap it for yours\\n\\n\\n\\nI'm out of touch, I'm out of love\\n\\nI'll pick you up when you're getting down\\n\\nand out of all these things I've done \\n\\nI think I love you better now\\n\\n\\n\\nI'm out of sight, I'm out of mind\\n\\nI'll do it all for you in time\\n\\nAnd out of all these things I've done \\n\\nI think I love you better now\\n\\n\\n\\nDon't hold me down\\n\\nI think my braces are breaking \\n\\nand it's more than I can take\\n\\n\\n\\nAnd if it's dark in a cold December, \\n\\nI've got ya to keep me warm\\n\\nand if you're broken then I will mend ya \\n\\nand keep you sheltered from the storm that's raging on, now\\n\\n\\n\\nI'm out of touch, I'm out of love\\n\\nI'll pick you up when you're getting down\\n\\nand out of all these things I've done \\n\\nI think I love you better now\\n\\n\\n\\nI'm out of sight, I'm out of mind\\n\\nI'll do it all for you in time\\n\\nAnd out of all these things I've done \\n\\nI think I love you better now\\n\\n\\n\\nI'm out of touch, I'm out of love\\n\\nI'll pick you up when you're getting down\\n\\nand out of all these things I've done \\n\\nI will love you better now\"}";

        private Mock<IServiceCaller> MockServiceCaller { get; set; }

        [SetUp]
        public void Setup()
        {
            MockServiceCaller = new Mock<IServiceCaller>();

            MockServiceCaller
                .Setup(x => x.GetApiResponseString("https://api.lyrics.ovh/v1//Lego House"))
                .Returns(Task.FromResult(_invalidGetResponse));

            MockServiceCaller
                .Setup(x => x.GetApiResponseString("https://api.lyrics.ovh/v1/Ed Sheeran/"))
                .Returns(Task.FromResult(_invalidGetResponse));

            MockServiceCaller
                .Setup(x => x.GetApiResponseString("https://api.lyrics.ovh/v1/Ed Sheeran/Lego House"))
                .Returns(Task.FromResult(_validGetResponse));
        }

        private LyricsOvhCaller GetLyricsOvhCaller()
        {
            return new LyricsOvhCaller(MockServiceCaller.Object);
        }
        
        [Test]
        public async Task GetLyrics_WhenNoArtistProvided_ReturnNull()
        {
            // Arrange
            var caller = GetLyricsOvhCaller();
            var artist = new ArtistDto {Name = ""};
            var work = new WorkDto {Title = "Lego House"};

            // Act
            var result = await caller.GetLyrics(artist.Name, work.Title);

            // Assert
            Assert.That(result == null);
        }

        [Test]
        public async Task GetLyrics_WhenNoTitleProvided_ReturnNull()
        {
            // Arrange
            var caller = GetLyricsOvhCaller();
            var artist = new ArtistDto { Name = "Ed Sheeran" };
            var work = new WorkDto { Title = "" };

            // Act
            var result = await caller.GetLyrics(artist.Name, work.Title);

            // Assert
            Assert.That(result == null);
        }

        [Test]
        public async Task GetLyrics_ArtistAndTitleProvided_ReturnLyricsDto()
        {
            // Arrange
            var lyricsOvCaller = GetLyricsOvhCaller();
            var artist = new ArtistDto { Name = "Ed Sheeran" };
            var work = new WorkDto {Title = "Lego House"};

            // Act
            var result = await lyricsOvCaller.GetLyrics(artist.Name, work.Title);

            // Assert
            Assert.That(result != null && result.Lyrics.Length > 0);
        }
    }
}