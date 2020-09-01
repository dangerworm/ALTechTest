using System;
using System.Linq;
using ALTechTest.DataTransferObjects;
using ALTechTest.ViewModels;
using NUnit.Framework;

namespace ALTechTestTests
{
    public class WorkViewModelTests
    {
        private RecordingDto[] _recordingDtos;
        private WorkDto _workDto;

        [Test]
        [TestCase(120)]
        [TestCase(180)]
        [TestCase(360)]
        public void ProcessLyricsAndRecordingDataCalculatesCorrectValues(int numberOfWords)
        {
            // Arrange
            var workViewModel = new WorkViewModel(_workDto)
            {
                Lyrics = string.Concat(Enumerable.Repeat("lyric ", numberOfWords))
            };

            _recordingDtos = new[]
            {
                new RecordingDto {Length = 175000},
                new RecordingDto {Length = 180000},
                new RecordingDto {Length = 185000}
            };

            var averageDuration = _recordingDtos.Average(x => x.Length) ?? 0;

            // Act
            workViewModel.ProcessLyricsAndRecordingData(_recordingDtos);

            // Assert
            Assert.That(workViewModel.AverageDuration.Equals(TimeSpan.FromMilliseconds(averageDuration)));
            Assert.That(workViewModel.WordsPerSecond.Equals(numberOfWords / averageDuration * 1000));
        }

        [SetUp]
        public void Setup()
        {
            _workDto = new WorkDto
            {
                Id = new Guid("184b7f06-a18c-42ef-b476-b42cfc7019b5"),
                Title = "Lego House",
                Type = "Song",
                Disambiguation = ""
            };

            // If you ever want *real* data, there's some here:
            //_recordingDtos = JsonConvert.DeserializeObject<IEnumerable<RecordingDto>>(TestResources.LegoHouseRecordings).ToArray();

            _recordingDtos = new[]
            {
                new RecordingDto {Length = 175000},
                new RecordingDto {Length = 180000},
                new RecordingDto {Length = 185000}
            };
        }
    }
}