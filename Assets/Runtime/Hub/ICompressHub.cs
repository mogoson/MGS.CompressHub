/*************************************************************************
 *  Copyright © 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICompressHub.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/22
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Text;
using MGS.Operate;

namespace MGS.Compress
{
    public interface ICompressHub
    {
        IAsyncOperateHub AsyncHub { get; }

        ICompressOperate CompressAsync(string sourceDir, string destFile,
            Encoding encoding, bool includeBaseDirectory = true, bool clearBefor = true);

        ICompressOperate DecompressAsync(string filePath, string destDir, bool clearBefor = true);

        void AbortAsync(ICompressOperate operate);
    }
}