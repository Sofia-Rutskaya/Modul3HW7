using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW7.Configs;
using Modul3HW7.Services.Additional;

namespace Modul3HW7.Services
{
    public class FileService : IFileService
    {
        private FileStream _newFile;
        private byte[] _buffer;
        private int _backup;

        public FileService()
        {
        }

        public async Task InitDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await ClearDirect(path);
        }

        public async Task SaveInFile(string message)
        {
            _buffer = System.Text.Encoding.Default.GetBytes($"{message} {Environment.NewLine}");
            await _newFile.WriteAsync(_buffer, 0, _buffer.Length);
            _backup++;
        }

        public IDisposable CreateStream(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            _newFile = new FileStream(path, FileMode.Append);
            return _newFile;
        }

        public IDisposable CloseStream(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            _newFile.Close();
            return _newFile;
        }

        private async Task ClearDirect(string path)
        {
            var result = Directory.GetFiles(path);

            foreach (var item in result)
            {
                await Task.Run(() => { File.Delete(item); });
            }
        }
    }
}
