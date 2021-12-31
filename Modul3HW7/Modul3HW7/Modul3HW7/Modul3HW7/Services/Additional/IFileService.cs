using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW7.Configs;

namespace Modul3HW7.Services.Additional
{
    public interface IFileService
    {
        public Task SaveInFile(string message);
        public Task InitDirectory(string path);
        public IDisposable CreateStream(string path);
        public IDisposable CloseStream(string path);
    }
}
