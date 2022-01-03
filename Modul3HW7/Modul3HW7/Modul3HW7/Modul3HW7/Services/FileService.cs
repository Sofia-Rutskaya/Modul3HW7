using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Modul3HW7.Configs;
using Modul3HW7.Services.Additional;

namespace Modul3HW7.Services
{
    public class FileService : IFileService
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public FileService()
        {
        }

        public void InitDirectory(string dirPath, string filePath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            ClearDirect(dirPath);

            if (filePath == null)
            {
                return;
            }
        }

        public async Task SaveInFile(string message, string filePath = "")
        {
            await _semaphoreSlim.WaitAsync();

            using (var sw = new StreamWriter(filePath, true, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(message);
            }

            _semaphoreSlim.Release();
        }

        public async Task Backup(string filePath = "", bool backup = false, string backupPath = "", int countConfigLines = 0)
        {
            if (backup)
            {
                var time = $"{DateTime.UtcNow.Hour}-{DateTime.UtcNow.Minute}-{DateTime.UtcNow.Second}-{DateTime.UtcNow.Millisecond}";
                backupPath = $"{backupPath}\\{time}.txt";
                using (var sw = new StreamWriter(backupPath, true, System.Text.Encoding.Default))
                {
                    var lines = File.ReadAllLinesAsync(filePath).Result;
                    var num = lines.Length / countConfigLines;
                    for (int i = 0, j = 0; i < num * countConfigLines; i++, ++j)
                    {
                        await sw.WriteLineAsync(lines[i]);
                        if (j >= countConfigLines - 1)
                        {
                            j = -1;
                            await sw.WriteLineAsync(Environment.NewLine);
                        }
                    }
                }
            }
        }

        public void SetBackupPath(string dirPath)
        {
            InitDirectory(dirPath, null);
        }

        private void ClearDirect(string dirPath)
        {
            var result = Directory.GetFiles(dirPath);

            foreach (var item in result)
            {
                File.Delete(item);
            }
        }
    }
}
