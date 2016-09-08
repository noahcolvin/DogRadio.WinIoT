using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.System.Threading;
using Devkoes.Restup.WebServer.File;
using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Rest;
using DogRadio.WinIoT.Library.Controllers;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace DogRadio.WinIoT
{
    public sealed class StartupTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;
        private ThreadPoolTimer _timer;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var restRouteHandler = new RestRouteHandler();
            restRouteHandler.RegisterController<RadioController>();
            restRouteHandler.RegisterController<PowerController>();

            _deferral = taskInstance.GetDeferral();
            _timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick, TimeSpan.FromMilliseconds(500));

            var httpServer = new HttpServer(8800);
            httpServer.RegisterRoute("api", restRouteHandler);
            httpServer.RegisterRoute(new StaticFileRouteHandler(@"Web"));
            await httpServer.StartServerAsync();
        }

        private void Timer_Tick(ThreadPoolTimer timer)
        {
            Debug.WriteLine("tick" + Environment.TickCount);
        }
    }
}
