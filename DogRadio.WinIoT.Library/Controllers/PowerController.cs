using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Windows.Devices.Gpio;

namespace DogRadio.WinIoT.Library.Controllers
{
    [RestController(InstanceCreationType.Singleton)]
    public class PowerController
    {
        private const int LIGHT_PIN = 3;
        private const int SPEAKER_PIN = 2;
        private GpioPin[] _pins = new GpioPin[2];
        private GpioPin _light;
        private GpioPin _speaker;

        public PowerController()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
                return;

            _pins[0] = gpio.OpenPin(LIGHT_PIN);
            _pins[1] = gpio.OpenPin(SPEAKER_PIN);

            // Show an error if the pin wasn't initialized properly
            if (_pins.Any(p => p == null))
                return;

            _pins.ToList().ForEach(x =>
            {
                x.Write(GpioPinValue.High);
                x.SetDriveMode(GpioPinDriveMode.Output);
            });
        }

        [UriFormat("/power/{pin}")]
        public GetResponse Get(int pin)
        {
            return new GetResponse(
                GetResponse.ResponseStatus.OK,
               _pins[pin].Read());
        }

        [UriFormat("/power/{pin}/{state}")]
        public PutResponse Put(int pin, int state)
        {
            _pins[pin].Write((GpioPinValue)Math.Abs(state - 1));

            return new PutResponse(
                PutResponse.ResponseStatus.OK,
               _pins[pin].Read());
        }
    }
}