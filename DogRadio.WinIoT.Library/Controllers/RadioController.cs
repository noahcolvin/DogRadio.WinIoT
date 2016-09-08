using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;

namespace DogRadio.WinIoT.Library.Controllers
{
    [RestController(InstanceCreationType.Singleton)]
    public sealed class RadioController
    {
        [UriFormat("/radio/play/{id}")]
        public PutResponse PutPlay(int id)
        {
            return new PutResponse(
                PutResponse.ResponseStatus.OK,
                "Okay!!!");
        }
    }
}