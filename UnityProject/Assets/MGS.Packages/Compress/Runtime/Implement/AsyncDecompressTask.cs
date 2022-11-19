/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncDecompressTask.cs
 *  Description  :  File decompress async task.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/30/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;

namespace MGS.Compress
{
    /// <summary>
    /// File decompress async task.
    /// </summary>
    internal class AsyncDecompressTask : AsyncTask
    {
        /// <summary>
        /// Entries associated with the task.
        /// </summary>
        public override IEnumerable<string> Entries
        {
            get { return new string[] { filePath, destDir }; }
        }

        protected string filePath;
        protected string destDir;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="compressor"></param>
        /// <param name="filePath"></param>
        /// <param name="destDir"></param>
        /// <param name="clearBefor"></param>
        /// <param name="progressCallback"></param>
        /// <param name="finishedCallback"></param>
        public AsyncDecompressTask(ICompressor compressor,
            string filePath, string destDir, bool clearBefor = true,
            Action<float> progressCallback = null, Action<bool, string, Exception> finishedCallback = null) :
            base(compressor, clearBefor, progressCallback, finishedCallback)
        {
            this.filePath = filePath;
            this.destDir = destDir;
        }

        /// <summary>
        /// Execute decompress operate.
        /// </summary>
        protected override void Execute()
        {
            compressor.Decompress(filePath, destDir, clearBefor, progressCallback, finishedCallback);
        }
    }
}