/*************************************************************************
 *  Copyright © 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICompressHub.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/21
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.Text;
using MGS.Operate;

namespace MGS.Compress
{
    public interface ICompressHub
    {
        IAsyncOperateHub AsyncHub { get; }

        IAsyncOperate<string> CompressAsync(IEnumerable<string> entries, string destFile, Encoding encoding,
            string directoryPathInArchive = null, bool clearBefor = true);

        IAsyncOperate<string> DecompressAsync(string filePath, string destDir, bool clearBefor = true);
    }
}