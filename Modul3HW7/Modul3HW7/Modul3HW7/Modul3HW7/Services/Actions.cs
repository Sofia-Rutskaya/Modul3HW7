using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW7.Services
{
    public class Actions
    {
        private readonly ILogger _logger;

        public Actions()
        {
            _logger = Logger.Instance;
        }

        public async Task Method_1()
        {
            await _logger.LogInfoAsync($"Start method: {nameof(Method_1)}");
        }

        public bool Method_2()
        {
            throw BusinessException.SkippedLogic("Skipped logic in method");
        }

        public bool Method_3()
        {
            throw new Exception("I broke a logic");
        }
    }
}
