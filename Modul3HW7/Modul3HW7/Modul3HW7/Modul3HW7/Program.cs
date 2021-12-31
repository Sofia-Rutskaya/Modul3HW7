using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Newtonsoft.Json;
using Modul3HW7.Configs;
using Modul3HW7.Services;
using Modul3HW7.Services.Additional;

using Microsoft.Extensions.DependencyInjection;

namespace Modul3HW7
{
    public class Program
    {
        private static ILogger _logger;

        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILogger, Logger>()
                .AddSingleton<IFileService, FileService>()
                .AddTransient<Starter>()
                .BuildServiceProvider();

            var start = serviceProvider.GetService<Starter>();
            _logger = Logger.Instance;
            _logger.Invoke();
        }
    }
}
