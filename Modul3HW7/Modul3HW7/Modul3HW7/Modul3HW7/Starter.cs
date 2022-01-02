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

namespace Modul3HW7
{
    public class Starter
    {
        private readonly Random _rand;
        private readonly ILogger _log;
        private readonly Actions _actions;

        public Starter()
        {
            _log = Logger.Instance;
            _rand = new Random();
            _actions = new Actions();
            _log.LoggerEvent += Run;

            var configFile = File.ReadAllText("config.json");
            var config = JsonConvert.DeserializeObject<Config>(configFile);
            _log.SetConfig(config);
        }

        public void Run()
        {
            var list = new List<Task>();
            list.Add(Task.Run(async () =>
            {
                for (var i = 0; i < 50; i++)
                {
                   await GetMethod();
                }
            }));

            list.Add(Task.Run(async () =>
            {
                for (var i = 0; i < 50; i++)
                {
                    await GetMethod();
                }
            }));
            Task.WhenAll(list).GetAwaiter().GetResult();
        }

        public async Task GetMethod()
        {
            try
            {
                switch (_rand.Next(1, 4))
                {
                    case 1:
                        Console.WriteLine($"Run_1 ---- 1 ----");

                        await _actions.Method_1();
                        break;
                    case 2:
                        Console.WriteLine($"Run_1 ---- 2 ----");

                        _actions.Method_2();
                        break;
                    case 3:
                        Console.WriteLine($"Run_1 ---- 3 ----");

                        _actions.Method_3();
                        break;
                }
            }
            catch (BusinessException ex)
            {
                await _log.LogWarning($"Action got this custom Exception : {ex.Message}");
            }
            catch (Exception ex)
            {
                await _log.LogError($"Action failed by reason: {ex.Message}");
            }
        }
    }
}
