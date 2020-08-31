using System;
using System.Linq;
using ALTechTest.DataTransferObjects;
using ALTechTest.Interfaces;
using ALTechTest.ServiceCallers;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ALTechTestTests
{
    public class MusicBrainzCallerTests
    {
        private Mock<IServiceCaller> MockServiceCallerForGetArtists { get; set; }
        private Mock<IServiceCaller> MockServiceCallerForGetArtistById { get; set; }
        private Mock<IServiceCaller> MockServiceCallerForGetWorksByArtistId { get; set; }
        private Mock<IServiceCaller> MockServiceCallerForGetRecordingsByArtistId { get; set; }

        [SetUp]
        public void Setup()
        {
            MockServiceCallerForGetArtists = new Mock<IServiceCaller>();
            MockServiceCallerForGetArtistById = new Mock<IServiceCaller>();
            MockServiceCallerForGetWorksByArtistId = new Mock<IServiceCaller>();
            MockServiceCallerForGetRecordingsByArtistId = new Mock<IServiceCaller>();

            MockServiceCallerForGetArtists
                .Setup(x => x.GetApiResponseString(TestResources.EmptyArtistsRequestUri))
                .Returns(Task.FromResult(TestResources.EmptyArtistsGetResponse));
            MockServiceCallerForGetArtists
                .Setup(x => x.GetApiResponseString(TestResources.ValidArtistsRequestUri))
                .Returns(Task.FromResult(TestResources.ValidArtistsGetResponse));

            MockServiceCallerForGetArtistById
                .Setup(x => x.GetApiResponseString(TestResources.EmptyArtistRequestUri))
                .Returns(Task.FromResult(TestResources.EmptyArtistGetResponse));
            MockServiceCallerForGetArtistById
                .Setup(x => x.GetApiResponseString(TestResources.ValidArtistRequestUri))
                .Returns(Task.FromResult(TestResources.ValidArtistGetResponse));

            MockServiceCallerForGetWorksByArtistId
                .Setup(x => x.GetApiResponseString(TestResources.EmptyWorksRequestUri))
                .Returns(Task.FromResult(TestResources.EmptyWorksGetResponse));
            MockServiceCallerForGetWorksByArtistId
                .Setup(x => x.GetApiResponseString(TestResources.ValidWorksRequestUri))
                .Returns(Task.FromResult(TestResources.ValidWorksGetResponse));

            MockServiceCallerForGetRecordingsByArtistId
                .Setup(x => x.GetApiResponseString(TestResources.EmptyRecordingsRequestUri))
                .Returns(Task.FromResult(TestResources.EmptyRecordingsGetResponse));
            MockServiceCallerForGetRecordingsByArtistId
                .Setup(x => x.GetApiResponseString(TestResources.ValidRecordingsRequestUri))
                .Returns(Task.FromResult(TestResources.ValidRecordingsGetResponse));
        }

        private MusicBrainzCaller GetMusicBrainzCaller(CallType callType)
        {
            switch (callType)
            {
                case CallType.GetArtists:
                    return new MusicBrainzCaller(MockServiceCallerForGetArtists.Object);
                case CallType.GetArtistById:
                    return new MusicBrainzCaller(MockServiceCallerForGetArtistById.Object);
                case CallType.GetWorksByArtistId:
                    return new MusicBrainzCaller(MockServiceCallerForGetWorksByArtistId.Object);
                case CallType.GetRecordingsByArtistId:
                    return new MusicBrainzCaller(MockServiceCallerForGetRecordingsByArtistId.Object);
                default:
                    throw new ArgumentOutOfRangeException(nameof(callType), callType, null);
            }
        }

        [Test]
        public async Task GetArtists_WhenNotFound_ReturnEmptyEnumerableOfArtistDto()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetArtists);
            var query = "IAmNotAnArtist";

            // Act
            var result = await caller.GetArtists(query);

            // Assert
            Assert.That(Equals(result, Enumerable.Empty<ArtistDto>()));
        }

        [Test]
        public async Task GetArtists_WhenFound_ReturnEnumerableOfArtistDto()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetArtists);
            var query = "Ed Sheeran";

            // Act
            var result = await caller.GetArtists(query);

            // Assert
            Assert.That(result.All(x => x.GetType() == typeof(ArtistDto)));
        }

        [Test]
        public async Task GetArtistById_WhenNotFound_ReturnNull()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetArtistById);
            var id = Guid.Empty;

            // Act
            var result = await caller.GetArtistById(id);

            // Assert
            Assert.That(result == null);
        }

        [Test]
        public async Task GetArtistById_WhenFound_ReturnSingleArtistDto()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetArtistById);
            var id = new Guid(TestResources.EdSheeranArtistId);

            // Act
            var result = await caller.GetArtistById(id);

            // Assert
            Assert.That(result.GetType() == typeof(ArtistDto));
        }

        [Test]
        public async Task GetRecordingsByArtistId_WhenNotFound_ReturnEmptyEnumerableOfRecordingDto()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetRecordingsByArtistId);
            var id = Guid.Empty;

            // Act
            var result = await caller.GetRecordingsByArtistId(id);

            // Assert
            Assert.That(Equals(result, Enumerable.Empty<RecordingDto>()));
        }

        [Test]
        public async Task GetRecordingsByArtistId_WhenFound_ReturnEnumerableOfRecordingDto()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetRecordingsByArtistId);
            var id = new Guid(TestResources.EdSheeranArtistId);

            // Act
            var result = await caller.GetRecordingsByArtistId(id);

            // Assert
            Assert.That(result.All(x => x.GetType() == typeof(RecordingDto)));
        }

        [Test]
        public async Task GetWorksByArtistId_WhenNotFound_ReturnEmptyEnumerableOfWorkDto()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetWorksByArtistId);
            var id = Guid.Empty;

            // Act
            var result = await caller.GetWorksByArtistId(id);

            // Assert
            Assert.That(Equals(result, Enumerable.Empty<WorkDto>()));
        }

        [Test]
        public async Task GetWorksByArtistId_WhenFound_ReturnEnumerableOfWorkDto()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetWorksByArtistId);
            var id = new Guid(TestResources.EdSheeranArtistId);

            // Act
            var result = await caller.GetWorksByArtistId(id);

            // Assert
            Assert.That(result.All(x => x.GetType() == typeof(WorkDto)));
        }
    }

    internal enum CallType
    {
        GetArtists,
        GetArtistById,
        GetWorksByArtistId,
        GetRecordingsByArtistId
    }
}
