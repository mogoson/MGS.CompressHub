/*************************************************************************
 *  Copyright © 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressHub.cs
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
    public class CompressHub : ICompressHub
    {
        public IAsyncOperateHub AsyncHub { protected set; get; }

        public CompressHub(IAsyncOperateHub asyncHub)
        {
            AsyncHub = asyncHub;
        }

        public ICompressOperate CompressAsync(string sourceDir, string destFile,
            Encoding encoding, bool includeBaseDirectory = true, bool clearBefor = true)
        {
            var operate = new DoCompressOperate(sourceDir, destFile, encoding, includeBaseDirectory, clearBefor);
            AsyncHub.Enqueue(operate);
            return operate;
        }

        public ICompressOperate DecompressAsync(string filePath, string destDir, bool clearBefor = true)
        {
            var operate = new DecompressOperate(filePath, destDir, clearBefor);
            AsyncHub.Enqueue(operate);
            return operate;
        }

        public void AbortAsync(ICompressOperate operate)
        {
            AsyncHub.Dequeue(operate);
        }
    }
}