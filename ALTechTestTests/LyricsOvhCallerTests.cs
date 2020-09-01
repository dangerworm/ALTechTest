using System.Threading.Tasks;
using ALTechTest.DataTransferObjects;
using ALTechTest.Interfaces;
using ALTechTest.ServiceCallers;
using Moq;
using NUnit.Framework;

namespace ALTechTestTests
{
    public class LyricsOvhCallerTests
    {
        private Mock<IServiceCaller> MockServiceCaller { get; set; }

        [Test]
        public async Task GetLyrics_ArtistAndTitleProvided_ReturnLyricsDto()
        {
            // Arrange
            var lyricsOvCaller = GetLyricsOvhCaller();
            var artist = new ArtistDto {Name = TestResources.ExampleArtistName};
            var work = new WorkDto {Title = TestResources.ExampleSongTitle};

            // Act
            var result = await lyricsOvCaller.GetLyrics(artist.Name, work.Title);

            // Assert
            Assert.That(result != null && result.Lyrics.Length > 0);
        }

        [Test]
        public async Task GetLyrics_WhenNoArtistProvided_ReturnNull()
        {
            // Arrange
            var caller = GetLyricsOvhCaller();
            var artist = new ArtistDto {Name = string.Empty};
            var work = new WorkDto {Title = TestResources.ExampleSongTitle};

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
            var artist = new ArtistDto {Name = TestResources.ExampleArtistName};
            var work = new WorkDto {Title = string.Empty};

            // Act
            var result = await caller.GetLyrics(artist.Name, work.Title);

            // Assert
            Assert.That(result == null);
        }

        private LyricsOvhCaller GetLyricsOvhCaller()
        {
            return new LyricsOvhCaller(MockServiceCaller.Object);
        }

        [SetUp]
        public void Setup()
        {
            MockServiceCaller = new Mock<IServiceCaller>();

            MockServiceCaller
                .Setup(x => x.GetApiResponseString(TestResources.InvalidLyricsRequestUriArtistNameOnly))
                .Returns(Task.FromResult(TestResources.InvalidLyricsGetResponse));

            MockServiceCaller
                .Setup(x => x.GetApiResponseString(TestResources.InvalidLyricsRequestUriSongTitleOnly))
                .Returns(Task.FromResult(TestResources.InvalidLyricsGetResponse));

            MockServiceCaller
                .Setup(x => x.GetApiResponseString(TestResources.ValidLyricsRequestUriArtistAndSongTitle))
                .Returns(Task.FromResult(TestResources.ValidLyricsGetResponse));
        }
    }
}