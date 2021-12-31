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
            var firstRun = new TaskCompletionSource();
            var secondRun = new TaskCompletionSource();
            Task.Run(() =>
            {
                for (var i = 0; i < 50; i++)
                {
                    GetMethod();
                }

                firstRun.SetResult();
            });

            Task.Run(() =>
            {
                for (var i = 0; i < 50; i++)
                {
                    GetMethod();
                }

                secondRun.SetResult();
            });

            firstRun.Task.GetAwaiter().GetResult();
            secondRun.Task.GetAwaiter().GetResult();
        }

        public void GetMethod()
        {
            try
            {
                switch (_rand.Next(1, 4))
                {
                    case 1:
                        Console.WriteLine($"Run_1 ---- 1 ----");

                        Task.Run(async () => { await _actions.Method_1(); });
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
                _log.LogWarning($"Action got this custom Exception : {ex.Message}");
            }
            catch (Exception ex)
            {
                _log.LogError($"Action failed by reason: {ex.Message}");
            }
        }
    }
}
