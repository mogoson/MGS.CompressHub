/*************************************************************************
 *  Copyright © 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DecompressWork.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/21
 *  Description  :  Initial development version.
 *************************************************************************/

using System.IO;
using Ionic.Zip;
using MGS.Work;

namespace MGS.Compress
{
    public class DecompressWork : AsyncWork<string>, ICompressWork
    {
        protected string filePath;
        protected string destDir;
        protected bool clearBefor;

        public DecompressWork(string filePath, string destDir, bool clearBefor = true)
        {
            this.filePath = filePath;
            this.destDir = destDir;
            this.clearBefor = clearBefor;
        }

        protected override string OnExecute()
        {
            if (clearBefor)
            {
                if (Directory.Exists(destDir))
                {
                    Directory.Delete(destDir, true);
                }
            }

            using (var zipFile = new ZipFile(filePath))
            {
                zipFile.ExtractProgress += (s, e) =>
                {
                    if (e == null || e.EntriesTotal == 0)
                    {
                        return;
                    }

                    Progress = (float)e.EntriesExtracted / e.EntriesTotal;
                };
                zipFile.ExtractAll(destDir, ExtractExistingFileAction.OverwriteSilently);
            }
            return destDir;
        }
    }
}