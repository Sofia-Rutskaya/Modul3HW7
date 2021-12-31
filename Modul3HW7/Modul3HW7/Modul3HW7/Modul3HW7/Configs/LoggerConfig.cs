using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modul3HW7.Configs
{
    public class LoggerConfig
    {
        public string FileName { get; set; }
        public string TimeFormat { get; set; }
        public string FileExtensions { get; set; }
        public string DirectoryName { get; set; }
        public string FilePath { get; set; }
        public string DirectoryPath { get; set; }
        public string DirectoryBackupPath { get; set; }
        public int Backup { get; set; }
    }
}
