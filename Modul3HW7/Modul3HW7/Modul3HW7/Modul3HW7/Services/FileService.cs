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
        private readonly object _lock = new object();
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private FileStream _newFile;

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

           // _newFile = CreateStream(filePath);
        }

        public void SetMainFile(FileStream file)
        {
            _newFile = file;
        }

        public async Task SaveInFile(string message, string filePath = "", bool backup = false, string backupPath = "", int countConfigLines = 0)
        {
            // _buffer = System.Text.Encoding.Default.GetBytes($"{message} {Environment.NewLine}");
            await _semaphoreSlim.WaitAsync();

            using (var sw = new StreamWriter(filePath, true, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(message);
            }

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

            _semaphoreSlim.Release();
        }

        public void SetBackupPath(string dirPath)
        {
            InitDirectory(dirPath, null);
        }

        public FileStream CreateStream(string dirPath)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
            {
                return null;
            }

            var disposable = new FileStream(dirPath, FileMode.OpenOrCreate);
            return disposable;
        }

        public FileStream CloseStream(string dirPath, FileStream disposable)
        {
            if (string.IsNullOrWhiteSpace(dirPath))
            {
                return null;
            }

            disposable.Close();
            return disposable;
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
