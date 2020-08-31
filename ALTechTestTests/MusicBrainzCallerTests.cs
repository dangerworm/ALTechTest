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
        private const string _emptyArtistsRequestUri = "http://musicbrainz.org/ws/2/artist?fmt=json&query=IAmNotAnArtist";
        private const string _emptyArtistsGetResponse = "{\"created\":\"2020-08-31T12:00:00.000Z\",\"count\":0,\"offset\":0,\"artists\":[]}";

        private const string _validArtistsRequestUri = "http://musicbrainz.org/ws/2/artist?fmt=json&query=Ed+Sheeran";
        private const string _validArtistsGetResponse = "{\"created\":\"2020-08-31T19:07:27.973Z\",\"count\":1459,\"offset\":0,\"artists\":[{\"id\":\"b8a7c51f-362c-4dcb-a259-bc6e0095f0a6\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":100,\"name\":\"Ed Sheeran\",\"sort-name\":\"Sheeran, Ed\",\"gender\":\"male\",\"country\":\"GB\",\"area\":{\"id\":\"8a754a16-0027-3a29-b6d7-2b40ea0481ed\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"United Kingdom\",\"sort-name\":\"United Kingdom\",\"life-span\":{\"ended\":null}},\"begin-area\":{\"id\":\"918efe5e-4f94-4554-8ce7-6aac7a5ac09f\",\"type\":\"City\",\"type-id\":\"6fd8f29a-3d0a-32fc-980d-ea697b69da78\",\"name\":\"Halifax\",\"sort-name\":\"Halifax\",\"life-span\":{\"ended\":null}},\"disambiguation\":\"UK singer-songwriter\",\"ipis\":[\"00600664284\"],\"isnis\":[\"0000000364434564\"],\"life-span\":{\"begin\":\"1991-02-17\",\"ended\":null},\"aliases\":[{\"sort-name\":\"Sheeran, Edward Christopher\",\"type-id\":\"d4dcd0c0-b341-3612-a332-c0ce797b25cf\",\"name\":\"Edward Christopher Sheeran\",\"locale\":null,\"type\":\"Legal name\",\"primary\":null,\"begin-date\":null,\"end-date\":null}],\"tags\":[{\"count\":3,\"name\":\"pop\"},{\"count\":1,\"name\":\"guitarist\"},{\"count\":1,\"name\":\"folk\"},{\"count\":1,\"name\":\"uk\"},{\"count\":1,\"name\":\"singer\"},{\"count\":1,\"name\":\"dance-pop\"},{\"count\":0,\"name\":\"pop rap\"},{\"count\":1,\"name\":\"folk pop\"},{\"count\":0,\"name\":\"rapper\"},{\"count\":0,\"name\":\"pop soul\"},{\"count\":1,\"name\":\"singer/songwriter\"},{\"count\":0,\"name\":\"ed sheeran does not make sillyname tracks ಠ_ಠ\"},{\"count\":0,\"name\":\"gradually watermelon\"}]},{\"id\":\"3bca1168-e7c6-4beb-9af9-db9dc674b1fa\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":95,\"name\":\"Ed Sheeran\",\"sort-name\":\"Sheeran, Ed\",\"gender\":\"male\",\"country\":\"GB\",\"area\":{\"id\":\"8a754a16-0027-3a29-b6d7-2b40ea0481ed\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"United Kingdom\",\"sort-name\":\"United Kingdom\",\"life-span\":{\"ended\":null}},\"begin-area\":{\"id\":\"c249c30e-88ab-4b2f-a745-96a25bd7afee\",\"type\":\"City\",\"type-id\":\"6fd8f29a-3d0a-32fc-980d-ea697b69da78\",\"name\":\"Liverpool\",\"sort-name\":\"Liverpool\",\"life-span\":{\"ended\":null}},\"disambiguation\":\"unknown singer-songwriter, \\\"Eddie Sheeran\\\"\",\"life-span\":{\"ended\":null},\"tags\":[{\"count\":1,\"name\":\"wrong-one\"}]},{\"id\":\"3ed09909-21f1-4a73-a725-59e0a87f5872\",\"score\":36,\"name\":\"Phil Sheeran\",\"sort-name\":\"Sheeran, Phil\",\"life-span\":{\"ended\":null},\"tags\":[{\"count\":1,\"name\":\"jazz\"},{\"count\":1,\"name\":\"production music\"}]},{\"id\":\"458383b7-0293-4e19-a86a-e6a0a4a7fae2\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":36,\"name\":\"Laura Sheeran\",\"sort-name\":\"Sheeran, Laura\",\"gender\":\"female\",\"country\":\"IE\",\"area\":{\"id\":\"390b05d4-11ec-3bce-a343-703a366b34a5\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"Ireland\",\"sort-name\":\"Ireland\",\"life-span\":{\"ended\":null}},\"disambiguation\":\"alternative artist from Dublin\",\"life-span\":{\"begin\":\"1987-04-19\",\"ended\":null}},{\"id\":\"cbb720ad-612b-4885-b6b1-fbe8181c8fb8\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":35,\"name\":\"Laura Sheeran\",\"sort-name\":\"Sheeran, Laura\",\"gender\":\"female\",\"disambiguation\":\"Caterpillar Love Hugs\",\"life-span\":{\"ended\":null}},{\"id\":\"c0a486cc-b699-4610-a45d-9f7c7d65a0eb\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":35,\"name\":\"Kate Sheeran\",\"sort-name\":\"Sheeran, Kate\",\"gender\":\"female\",\"life-span\":{\"ended\":null}},{\"id\":\"983b72eb-61df-4efe-88c2-af368ad08408\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":35,\"name\":\"John Sheeran\",\"sort-name\":\"Sheeran, John\",\"gender\":\"male\",\"life-span\":{\"ended\":null}},{\"id\":\"0ad66386-c44c-4fe4-a88a-153bea036acf\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":35,\"name\":\"Patmo Sheeran\",\"sort-name\":\"Sheeran, Patmo\",\"ipis\":[\"00040570608\"],\"life-span\":{\"ended\":null}},{\"id\":\"eb41ebf1-ef2a-46bf-af65-b6b5b7bc348c\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":35,\"name\":\"Matt Sheeran\",\"sort-name\":\"Sheeran, Matt\",\"gender\":\"male\",\"area\":{\"id\":\"40d758a4-b7c2-40f3-b439-5efbd2a3b038\",\"type\":\"City\",\"type-id\":\"6fd8f29a-3d0a-32fc-980d-ea697b69da78\",\"name\":\"Bristol\",\"sort-name\":\"Bristol\",\"life-span\":{\"ended\":null}},\"life-span\":{\"ended\":null}},{\"id\":\"c309ab91-fdcd-4a67-a37b-2c1ef1b8b02f\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":35,\"name\":\"Alistair Sheeran\",\"sort-name\":\"Sheeran, Alistair\",\"gender\":\"male\",\"life-span\":{\"ended\":null}},{\"id\":\"7ca5acb8-0b7d-49ef-9f47-0165eb3d5689\",\"score\":31,\"name\":\"Ed Ed\",\"sort-name\":\"Ed Ed\",\"life-span\":{\"ended\":null}},{\"id\":\"fc8b6db8-695c-4faf-a9f6-3d9051eab587\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":31,\"name\":\"Ed Starink\",\"sort-name\":\"Starink, Ed\",\"gender\":\"male\",\"country\":\"NL\",\"area\":{\"id\":\"ef1b7cc0-cd26-36f4-8ea0-04d9623786c7\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"Netherlands\",\"sort-name\":\"Netherlands\",\"life-span\":{\"ended\":null}},\"begin-area\":{\"id\":\"c185924d-df30-48d3-af06-7dd8c8592a5a\",\"type\":\"City\",\"type-id\":\"6fd8f29a-3d0a-32fc-980d-ea697b69da78\",\"name\":\"Apeldoorn\",\"sort-name\":\"Apeldoorn\",\"life-span\":{\"ended\":null}},\"disambiguation\":\"keyboardist, songwriter & producer\",\"ipis\":[\"00068375642\"],\"life-span\":{\"begin\":\"1952-12-17\",\"ended\":null},\"aliases\":[{\"sort-name\":\"SYNTHETISEUR\",\"type-id\":\"1937e404-b981-3cb7-8151-4c86ebfc8d8e\",\"name\":\"SYNTHETISEUR\",\"locale\":null,\"type\":\"Search hint\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Star Inc.\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Star Inc.\",\"locale\":null,\"type\":\"Artist name\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Synthétiseur Orchestra\",\"name\":\"Synthétiseur Orchestra\",\"locale\":null,\"type\":null,\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Hollywood Studio Orchestra\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Hollywood Studio Orchestra\",\"locale\":null,\"type\":\"Artist name\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"The London Studio Symphony Orchestra\",\"name\":\"The London Studio Symphony Orchestra\",\"locale\":null,\"type\":null,\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"London Starlight Orchestra\",\"name\":\"London Starlight Orchestra\",\"locale\":null,\"type\":null,\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Edgar Starink\",\"name\":\"Edgar Starink\",\"locale\":null,\"type\":null,\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Starink\",\"type-id\":\"1937e404-b981-3cb7-8151-4c86ebfc8d8e\",\"name\":\"Starink\",\"locale\":null,\"type\":\"Search hint\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Starr Inc.\",\"type-id\":\"1937e404-b981-3cb7-8151-4c86ebfc8d8e\",\"name\":\"Starr Inc.\",\"locale\":null,\"type\":\"Search hint\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Andromeda Project\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Andromeda Project\",\"locale\":null,\"type\":\"Artist name\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Space 2000\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Space 2000\",\"locale\":null,\"type\":\"Artist name\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"New Star Orchestra, The\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"The New Star Orchestra\",\"locale\":null,\"type\":\"Artist name\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"E.Starink\",\"type-id\":\"1937e404-b981-3cb7-8151-4c86ebfc8d8e\",\"name\":\"E.Starink\",\"locale\":null,\"type\":\"Search hint\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Starink, Eduward A.J.\",\"type-id\":\"d4dcd0c0-b341-3612-a332-c0ce797b25cf\",\"name\":\"Eduward A.J. Starink\",\"locale\":null,\"type\":\"Legal name\",\"primary\":null,\"begin-date\":null,\"end-date\":null}],\"tags\":[{\"count\":1,\"name\":\"electronic\"},{\"count\":1,\"name\":\"synth-pop\"}]},{\"id\":\"8b304b6b-7146-4b8e-b1f4-7ae1d879751e\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":30,\"name\":\"Edgar Schlepper\",\"sort-name\":\"Schlepper, Edgar\",\"gender\":\"male\",\"country\":\"DE\",\"area\":{\"id\":\"85752fda-13c4-31a3-bee5-0e5cb1f51dad\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"Germany\",\"sort-name\":\"Germany\",\"life-span\":{\"ended\":null}},\"begin-area\":{\"id\":\"564f6766-f295-4c97-901f-1bf68249903e\",\"type\":\"City\",\"type-id\":\"6fd8f29a-3d0a-32fc-980d-ea697b69da78\",\"name\":\"Oldenburg\",\"sort-name\":\"Oldenburg\",\"life-span\":{\"ended\":null}},\"end-area\":{\"id\":\"7be2e8c7-2264-43e4-ac57-0bdce42022c1\",\"type\":\"City\",\"type-id\":\"6fd8f29a-3d0a-32fc-980d-ea697b69da78\",\"name\":\"Lübeck\",\"sort-name\":\"Lübeck\",\"life-span\":{\"ended\":null}},\"life-span\":{\"begin\":\"1949-03-01\",\"end\":\"2015-07-21\",\"ended\":true},\"aliases\":[{\"sort-name\":\"Vanguard, Ed\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Ed Vanguard\",\"locale\":\"de\",\"type\":\"Artist name\",\"primary\":true,\"begin-date\":null,\"end-date\":null}]},{\"id\":\"b39cc25c-56d2-49d3-8ef1-69e14b74c3ef\",\"type\":\"Group\",\"type-id\":\"e431f5f6-b5d2-343d-8b36-72607fffb74b\",\"score\":29,\"name\":\"ED\",\"sort-name\":\"ED\",\"country\":\"IT\",\"area\":{\"id\":\"c6500277-9a3d-349b-bf30-41afdbf42add\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"Italy\",\"sort-name\":\"Italy\",\"life-span\":{\"ended\":null}},\"disambiguation\":\"Italian skate punk\",\"life-span\":{\"ended\":null}},{\"id\":\"915e3e7e-1245-4046-b189-98ad854a2332\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":29,\"name\":\"Ed Asner\",\"sort-name\":\"Asner, Ed\",\"gender\":\"male\",\"begin-area\":{\"id\":\"c1fc21db-40e9-47f5-af93-f3ebe6581113\",\"type\":\"City\",\"type-id\":\"6fd8f29a-3d0a-32fc-980d-ea697b69da78\",\"name\":\"Kansas City\",\"sort-name\":\"Kansas City\",\"life-span\":{\"ended\":null}},\"isnis\":[\"0000000081333616\"],\"life-span\":{\"begin\":\"1929-11-15\",\"ended\":null},\"aliases\":[{\"sort-name\":\"Yitzhak Edward Asner\",\"type-id\":\"d4dcd0c0-b341-3612-a332-c0ce797b25cf\",\"name\":\"Yitzhak Edward Asner\",\"locale\":\"en\",\"type\":\"Legal name\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Asner, Ed\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Ed Asner\",\"locale\":\"en\",\"type\":\"Artist name\",\"primary\":true,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Asner, Edward\",\"name\":\"Edward Asner\",\"locale\":null,\"type\":null,\"primary\":null,\"begin-date\":null,\"end-date\":null}]},{\"id\":\"2789ab73-04bd-4d6f-96ce-d2d2237b6bb2\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":29,\"name\":\"Ed Labunski\",\"sort-name\":\"Labunski, Ed\",\"gender\":\"male\",\"end-area\":{\"id\":\"6e24b85b-f261-4c8f-8dc3-ccb86465aec7\",\"type\":\"City\",\"type-id\":\"6fd8f29a-3d0a-32fc-980d-ea697b69da78\",\"name\":\"Milford\",\"sort-name\":\"Milford\",\"life-span\":{\"ended\":null}},\"life-span\":{\"begin\":\"1937-05-14\",\"end\":\"1980\",\"ended\":true},\"aliases\":[{\"sort-name\":\"Labunski, Ed\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Ed Labunski\",\"locale\":\"en\",\"type\":\"Artist name\",\"primary\":true,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Labunski, Edward H.\",\"type-id\":\"d4dcd0c0-b341-3612-a332-c0ce797b25cf\",\"name\":\"Edward H. Labunski\",\"locale\":\"en\",\"type\":\"Legal name\",\"primary\":null,\"begin-date\":null,\"end-date\":null}]},{\"id\":\"137d7a9e-21c5-45fb-9070-3f49b9bb5bf2\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":29,\"name\":\"Ed Silvers\",\"sort-name\":\"Silvers, Ed\",\"gender\":\"male\",\"country\":\"US\",\"area\":{\"id\":\"489ce91b-6658-3307-9877-795b68554c98\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"United States\",\"sort-name\":\"United States\",\"life-span\":{\"ended\":null}},\"ipis\":[\"00028737464\"],\"life-span\":{\"ended\":null},\"aliases\":[{\"sort-name\":\"Silvers, Ed\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Ed Silvers\",\"locale\":\"en\",\"type\":\"Artist name\",\"primary\":true,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Silvers, Eddie\",\"type-id\":\"894afba6-2816-3c24-8072-eadb66bd04bc\",\"name\":\"Eddie Silvers\",\"locale\":\"en\",\"type\":\"Artist name\",\"primary\":null,\"begin-date\":null,\"end-date\":null},{\"sort-name\":\"Silvers, Edward James, Jr.\",\"type-id\":\"d4dcd0c0-b341-3612-a332-c0ce797b25cf\",\"name\":\"Edward James Silvers, Jr.\",\"locale\":\"en\",\"type\":\"Legal name\",\"primary\":null,\"begin-date\":null,\"end-date\":null}]},{\"id\":\"86edbe02-325a-446c-9b90-beb70237b8fa\",\"score\":29,\"name\":\"ED.\",\"sort-name\":\"ED\",\"disambiguation\":\"Electric Dreams Future funk artist\",\"life-span\":{\"ended\":null}},{\"id\":\"a02d8fb6-cea8-4304-a809-e733ecdda445\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":29,\"name\":\"-ED\",\"sort-name\":\"-ED\",\"gender\":\"male\",\"country\":\"BY\",\"area\":{\"id\":\"660e3c48-b301-3c8c-9708-0f71d5d094d6\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"Belarus\",\"sort-name\":\"Belarus\",\"life-span\":{\"ended\":null}},\"disambiguation\":\"Belarusian ambient producer\",\"life-span\":{\"ended\":null}},{\"id\":\"95380f98-9cb2-4090-a47e-f09a5a970f9d\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":29,\"name\":\"Ed\",\"sort-name\":\"Ed\",\"gender\":\"male\",\"country\":\"NL\",\"area\":{\"id\":\"ef1b7cc0-cd26-36f4-8ea0-04d9623786c7\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"Netherlands\",\"sort-name\":\"Netherlands\",\"life-span\":{\"ended\":null}},\"disambiguation\":\"A dutch singer/songwriter\",\"life-span\":{\"ended\":null}},{\"id\":\"062cb7e9-154a-431f-ac7f-8252073a9059\",\"score\":29,\"name\":\"Ed\",\"sort-name\":\"Ed\",\"disambiguation\":\"micromusic/chiptune artist\",\"life-span\":{\"ended\":null}},{\"id\":\"921e23a4-d24d-4d98-8cd6-8b99128aec95\",\"type\":\"Person\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\",\"score\":29,\"name\":\"Ed\",\"sort-name\":\"Ed\",\"gender\":\"male\",\"disambiguation\":\"Israeli artist\",\"life-span\":{\"ended\":null}},{\"id\":\"2f26a9fa-be12-45a9-8cf2-71e1fd908178\",\"score\":29,\"name\":\"E.D.\",\"sort-name\":\"E.D.\",\"disambiguation\":\"US Vocalist\",\"life-span\":{\"ended\":null}},{\"id\":\"dedd61c1-1c72-49d1-8fff-761b890e3f45\",\"score\":29,\"name\":\"Ed\",\"sort-name\":\"Ed\",\"disambiguation\":\"The Chief\",\"life-span\":{\"ended\":null}},{\"id\":\"68a97384-146f-4f87-982f-4d773046eb0d\",\"type\":\"Group\",\"type-id\":\"e431f5f6-b5d2-343d-8b36-72607fffb74b\",\"score\":29,\"name\":\"Ed\",\"sort-name\":\"Ed\",\"country\":\"IT\",\"area\":{\"id\":\"c6500277-9a3d-349b-bf30-41afdbf42add\",\"type\":\"Country\",\"type-id\":\"06dd0ae4-8c74-30bb-b43d-95dcedf961de\",\"name\":\"Italy\",\"sort-name\":\"Italy\",\"life-span\":{\"ended\":null}},\"disambiguation\":\"Italian indie-pop band\",\"life-span\":{\"ended\":null}}]}";

        private const string _emptyArtistRequestUri = "http://musicbrainz.org/ws/2/artist/00000000-0000-0000-0000-000000000000?fmt=json";
        private const string _emptyArtistGetResponse = "{\"help\":\"For usage, please see: https://musicbrainz.org/development/mmd\",\"error\":\"Invalid mbid.\"}";

        private const string _validArtistRequestUri = "http://musicbrainz.org/ws/2/artist/b8a7c51f-362c-4dcb-a259-bc6e0095f0a6?fmt=json";
        private const string _validArtistGetResponse = "{\"sort-name\":\"Sheeran, Ed\",\"life-span\":{\"ended\":false,\"begin\":\"1991-02-17\",\"end\":null},\"gender\":\"Male\",\"name\":\"Ed Sheeran\",\"isnis\":[\"0000000364434564\"],\"country\":\"GB\",\"ipis\":[\"00600664284\"],\"disambiguation\":\"UK singer-songwriter\",\"begin_area\":{\"type-id\":null,\"name\":\"Halifax\",\"id\":\"918efe5e-4f94-4554-8ce7-6aac7a5ac09f\",\"sort-name\":\"Halifax\",\"disambiguation\":\"\",\"type\":null},\"type\":\"Person\",\"id\":\"b8a7c51f-362c-4dcb-a259-bc6e0095f0a6\",\"begin-area\":{\"type-id\":null,\"name\":\"Halifax\",\"id\":\"918efe5e-4f94-4554-8ce7-6aac7a5ac09f\",\"sort-name\":\"Halifax\",\"disambiguation\":\"\",\"type\":null},\"area\":{\"iso-3166-1-codes\":[\"GB\"],\"sort-name\":\"United Kingdom\",\"type\":null,\"disambiguation\":\"\",\"name\":\"United Kingdom\",\"type-id\":null,\"id\":\"8a754a16-0027-3a29-b6d7-2b40ea0481ed\"},\"end-area\":null,\"end_area\":null,\"gender-id\":\"36d3d30a-839d-3eda-8cb3-29be4384e4a9\",\"type-id\":\"b6e035f4-3ce9-331c-97df-83397230b0df\"}";

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

            MockServiceCallerForGetArtists.Setup(x => x.GetApiResponseString(_emptyArtistsRequestUri)).Returns(Task.FromResult(_emptyArtistsGetResponse));
            MockServiceCallerForGetArtists.Setup(x => x.GetApiResponseString(_validArtistsRequestUri)).Returns(Task.FromResult(_validArtistsGetResponse));

            MockServiceCallerForGetArtistById.Setup(x => x.GetApiResponseString(_emptyArtistRequestUri)).Returns(Task.FromResult(_emptyArtistGetResponse));
            MockServiceCallerForGetArtistById.Setup(x => x.GetApiResponseString(_validArtistRequestUri)).Returns(Task.FromResult(_validArtistGetResponse));
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
                    break;
                case CallType.GetRecordingsByArtistId:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(callType), callType, null);
            }

            return null;
        }

        [Test]
        public async Task GetArtists_WhenNoArtistsFound_ReturnEmptyList()
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
        public async Task GetArtists_WhenArtistsFound_ReturnListOfArtistDto()
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
            Assert.That(Equals(result, Enumerable.Empty<ArtistDto>()));
        }

        [Test]
        public async Task GetArtistById_WhenArtistFound_ReturnSingleArtistDto()
        {
            // Arrange
            var caller = GetMusicBrainzCaller(CallType.GetArtistById);
            var id = new Guid("b8a7c51f-362c-4dcb-a259-bc6e0095f0a6");

            // Act
            var result = await caller.GetArtistById(id);

            // Assert
            Assert.That(result.GetType() == typeof(ArtistDto));
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
