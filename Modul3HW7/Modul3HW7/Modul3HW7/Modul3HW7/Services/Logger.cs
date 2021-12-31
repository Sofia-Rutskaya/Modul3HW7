using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW7.Configs;
using Modul3HW7.Services.Additional;

namespace Modul3HW7.Services
{
    public class Logger : ILogger
    {
        private Config _config;
        private static readonly Logger _instance = new Logger();
        private DateTime _date;
        private IFileService _fileService;
        private int _countBackup;

        static Logger()
        {
        }

        private Logger()
        {
        }

        public event Action LoggerEvent;

        public static Logger Instance => _instance;

        public async Task LogInfo(string message) => await Log(LogType.Info, message);

        public async Task LogError(string message) => await Log(LogType.Error, message);

        public async Task LogWarning(string message) => await Log(LogType.Warning, message);

        public void SetConfig(Config config)
        {
            _config = config;

            _fileService = new FileService();
            _fileService.InitDirectory(DirectPath());
            _fileService.CreateStream(FilePath());
        }

        public void Invoke()
        {
            LoggerEvent.Invoke();
            _fileService.CloseStream(FilePath());
        }

        public string FilePath()
        {
            _date = DateTime.UtcNow;
            _config.Logger.FileName = $"{_date.ToString(_config.Logger.TimeFormat)}";
            var result = $"{DirectPath()}\\{_config.Logger.FileName}{_config.Logger.FileExtensions}";
            _config.Logger.FilePath = result;
            return result;
        }

        public void Backup()
        {
            var path = $"{DirectPath()}{_config.Logger.DirectoryBackupPath}";
            _fileService.SetBackupPath(path);
            _fileService.SaveBackupInFile(path, FilePath());
        }

        public string DirectPath()
        {
            var dirPath = $"{_config.Logger.DirectoryPath}\\{_config.Logger.DirectoryName}";
            return dirPath;
        }

        private async Task Log(LogType type, string message)
        {
            if (_countBackup >= _config.Logger.Backup)
            {
                Backup();
                _countBackup = 0;
            }

            _countBackup++;
            var log = $"{DateTime.UtcNow}: {type.ToString()}: {message}";
            await _fileService.SaveInFile(log);
        }
    }
}
