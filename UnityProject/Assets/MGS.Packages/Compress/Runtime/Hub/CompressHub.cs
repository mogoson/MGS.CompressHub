/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressHub.cs
 *  Description  :  Interface of hub to manage compress work and cache
 *                  data, and let other thread notify status.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  03/10/2023
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.Text;

namespace MGS.Work.Compress
{
    /// <summary>
    /// Hub to manage compress work and cache
    /// data, and let other thread notify status.
    /// </summary>
    public class CompressHub : ICompressHub
    {
        /// <summary>
        /// Async Work hub to manage net work.
        /// </summary>
        public IAsyncWorkStatusHub AsyncHub { set; get; }

        /// <summary>
        /// Compressor to do compress work.
        /// </summary>
        public ICompressor Compressor { set; get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="asyncHub">Async Work hub to manage net work.</param>
        /// <param name="compressor">Compressor to do compress work.</param>
        public CompressHub(IAsyncWorkStatusHub asyncHub, ICompressor compressor)
        {
            AsyncHub = asyncHub;
            Compressor = compressor;
        }

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
        public IAsyncWorkHandler<string> CompressAsync(IEnumerable<string> entries, string destFile,
            Encoding encoding, string directoryPathInArchive = null, bool clearBefor = true)
        {
            var work = new CompressFileWork(Compressor, entries, destFile, encoding, directoryPathInArchive, clearBefor);
            return AsyncHub.EnqueueWork(work);
        }

        /// <summary>
        /// Decompress file to dest dir async.
        /// </summary>
        /// <param name="filePath">Target file.</param>
        /// <param name="destDir">The dest decompress directory.</param>
        /// <param name="clearBefor">Clear the dest dir before decompress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        public IAsyncWorkHandler<string> DecompressAsync(string filePath, string destDir, bool clearBefor = true)
        {
            var work = new DecompressFileWork(Compressor, filePath, destDir, clearBefor);
            return AsyncHub.EnqueueWork(work);
        }

        /// <summary>
        /// Clear cache resources.
        /// </summary>
        public virtual void Clear()
        {
            AsyncHub.Clear(true, true);
        }

        /// <summary>
        /// Dispose all resources.
        /// </summary>
        public virtual void Dispose()
        {
            AsyncHub.Dispose();
            AsyncHub = null;
            Compressor = null;
        }
    }
}