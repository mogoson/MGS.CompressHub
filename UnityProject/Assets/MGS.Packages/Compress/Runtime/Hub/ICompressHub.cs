/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IAsyncWorkStatusHub.cs
 *  Description  :  Interface of hub to manage compress work and cache
 *                  data, and let other thread notify status.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/10/2023
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace MGS.Work.Compress
{
    /// <summary>
    /// Interface of hub to manage compress work and cache
    /// data, and let other thread notify status.
    /// </summary>
    public interface ICompressHub : IDisposable
    {
        /// <summary>
        /// Async Work hub to manage net work.
        /// </summary>
        IAsyncWorkStatusHub AsyncHub { set; get; }

        /// <summary>
        /// Compressor to do compress work.
        /// </summary>
        ICompressor Compressor { set; get; }

        /// <summary>
        /// Compress entrie[files or directories] to dest file async.
        /// </summary>
        /// <param name="entries">Target entrie[files or directories].</param>
        /// <param name="destFile">The dest file.</param>
        /// <param name="encoding">Encoding for zip file.</param>
        /// <param name="directoryPathInArchive">Directory path in archive of zip file.</param>
        /// <param name="clearBefor">Clear origin file(if exists) befor compress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        IAsyncWorkHandler<string> CompressAsync(IEnumerable<string> entries, string destFile,
            Encoding encoding, string directoryPathInArchive = null, bool clearBefor = true);

        /// <summary>
        /// Decompress file to dest dir async.
        /// </summary>
        /// <param name="filePath">Target file.</param>
        /// <param name="destDir">The dest decompress directory.</param>
        /// <param name="clearBefor">Clear the dest dir before decompress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        IAsyncWorkHandler<string> DecompressAsync(string filePath, string destDir, bool clearBefor = true);

        /// <summary>
        /// Clear cache resources.
        /// </summary>
        void Clear();
    }
}