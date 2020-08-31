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
        private Mock<IServiceCaller> MockServiceCaller { get; set; }

        [SetUp]
        public void Setup()
        {
            MockServiceCaller = new Mock<IServiceCaller>();

            MockServiceCaller
                .Setup(x => x.GetApiResponseString("https://api.lyrics.ovh/v1//Lego House"))
                .Returns(Task.FromResult(TestResources.InvalidLyricsGetResponse));

            MockServiceCaller
                .Setup(x => x.GetApiResponseString("https://api.lyrics.ovh/v1/Ed Sheeran/"))
                .Returns(Task.FromResult(TestResources.InvalidLyricsGetResponse));

            MockServiceCaller
                .Setup(x => x.GetApiResponseString("https://api.lyrics.ovh/v1/Ed Sheeran/Lego House"))
                .Returns(Task.FromResult(TestResources.ValidLyricsGetResponse));
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