using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW7.Configs;

namespace Modul3HW7.Services
{
    public interface ILogger
    {
        public event Action LoggerEvent;

        public Task LogInfo(string message);

        public Task LogError(string message);

        public Task LogWarning(string message);

        public void SetConfig(Config config);

        public string FilePath();

        public void Invoke();
    }
}
