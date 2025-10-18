/*************************************************************************
 *  Copyright © 2024 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DoCompressOperate.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  2024/7/22
 *  Description  :  Initial development version.
 *************************************************************************/

using System.IO;
using System.IO.Compression;
using System.Text;

namespace MGS.Compress
{
    public class DoCompressOperate : CompressOperate
    {
        protected string sourceDir;
        protected string destFile;
        protected Encoding encoding;
        protected bool includeBaseDirectory;
        protected bool clearBefor;

        public DoCompressOperate(string sourceDir, string destFile, Encoding encoding,
            bool includeBaseDirectory = true, bool clearBefor = true)
        {
            this.sourceDir = sourceDir;
            this.destFile = destFile;
            this.encoding = encoding;
            this.includeBaseDirectory = includeBaseDirectory;
            this.clearBefor = clearBefor;
        }

        protected override string OnExecute()
        {
            if (clearBefor && File.Exists(destFile))
            {
                File.Delete(destFile);
            }

            ZipFile.CreateFromDirectory(sourceDir, destFile, CompressionLevel.Optimal, includeBaseDirectory, encoding);
            return destFile;
        }
    }
}