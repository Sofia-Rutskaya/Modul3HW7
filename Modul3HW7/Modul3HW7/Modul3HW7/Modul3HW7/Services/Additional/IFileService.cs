using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW7.Configs;

namespace Modul3HW7.Services.Additional
{
    public interface IFileService
    {
        public Task SaveInFile(string message, string filePath = "", bool backup = false, string backupPath = "");
        public void InitDirectory(string dirPath, string filePath);
        public FileStream CreateStream(string filePath);
        public FileStream CloseStream(string filePath, FileStream disposable);
        public void SetBackupPath(string filePath);
        public void SetMainFile(FileStream file);
    }
}
