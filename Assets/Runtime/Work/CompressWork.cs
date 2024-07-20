/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressWork.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/21
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Text;
using Ionic.Zip;
using MGS.Work;

namespace MGS.Compress
{
    public class CompressWork : AsyncWork<string>, ICompressWork
    {
        protected IEnumerable<string> entries;
        protected string destFile;
        protected Encoding encoding;
        protected string directoryPathInArchive = null;
        protected bool clearBefor = true;

        public CompressWork(IEnumerable<string> entries, string destFile, Encoding encoding,
            string directoryPathInArchive = null, bool clearBefor = true)
        {
            this.entries = entries;
            this.destFile = destFile;
            this.encoding = encoding;
            this.directoryPathInArchive = directoryPathInArchive;
            this.clearBefor = clearBefor;
        }

        protected override string OnExecute()
        {
            if (clearBefor && File.Exists(destFile))
            {
                File.Delete(destFile);
            }

            using (var zipFile = new ZipFile(destFile, encoding))
            {
                zipFile.SaveProgress += (s, e) =>
                {
                    if (e == null || e.EntriesTotal == 0)
                    {
                        return;
                    }

                    Progress = (float)e.EntriesSaved / e.EntriesTotal;
                };

                foreach (var entry in entries)
                {
                    zipFile.AddItem(entry, directoryPathInArchive);
                }
                zipFile.Save();
            }
            return destFile;
        }
    }
}