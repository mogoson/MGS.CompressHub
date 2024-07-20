/*************************************************************************
 *  Copyright (C) 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressHub.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/21
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.Text;
using MGS.Work;

namespace MGS.Compress
{
    public class CompressHub : AsyncWorkMonoHub, ICompressHub
    {
        public IAsyncWorkHandler<string> CompressAsync(IEnumerable<string> entries, string destFile, Encoding encoding,
            string directoryPathInArchive = null, bool clearBefor = true)
        {
            var work = new CompressWork(entries, destFile, encoding, directoryPathInArchive, clearBefor);
            return EnqueueWork(work);
        }

        public IAsyncWorkHandler<string> DecompressAsync(string filePath, string destDir, bool clearBefor = true)
        {
            var work = new DecompressWork(filePath, destDir, clearBefor);
            return EnqueueWork(work);
        }
    }
}