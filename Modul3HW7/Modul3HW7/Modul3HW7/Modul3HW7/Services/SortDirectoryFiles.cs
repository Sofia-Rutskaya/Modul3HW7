using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul3HW7.Services.Additional;

namespace Modul3HW7.Services
{
    public class SortDirectoryFiles : IComparer<FileInfo>
    {
        public int Compare(FileInfo first, FileInfo second)
        {
            if (first.CreationTimeUtc > second.CreationTimeUtc)
            {
                return 1;
            }
            else if (first.CreationTimeUtc < second.CreationTimeUtc)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
