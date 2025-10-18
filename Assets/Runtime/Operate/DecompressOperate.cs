/*************************************************************************
 *  Copyright © 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DecompressOperate.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/22
 *  Description  :  Initial development version.
 *************************************************************************/

using System.IO;
using System.IO.Compression;

namespace MGS.Compress
{
    public class DecompressOperate : CompressOperate
    {
        protected string filePath;
        protected string destDir;
        protected bool clearBefor;

        public DecompressOperate(string filePath, string destDir, bool clearBefor = true)
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

            ZipFile.ExtractToDirectory(filePath, destDir);
            return destDir;
        }
    }
}