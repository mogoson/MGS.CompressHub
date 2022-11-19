/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  AsyncCompressTask.cs
 *  Description  :  File compress async task.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/30/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace MGS.Compress
{
    /// <summary>
    /// File compress async task.
    /// </summary>
    internal class AsyncCompressTask : AsyncTask
    {
        /// <summary>
        /// Entries associated with the task.
        /// </summary>
        public override IEnumerable<string> Entries
        {
            get { return new List<string>(entries) { destFile }; }
        }

        protected IEnumerable<string> entries;
        protected string destFile;
        protected Encoding encoding;
        protected string directoryPathInArchive;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="compressor"></param>
        /// <param name="entries">Target entrie[files or directories].</param>
        /// <param name="destFile">The dest file.</param>
        /// <param name="encoding">Encoding for zip file.</param>
        /// <param name="directoryPathInArchive">Directory path in archive of zip file.</param>
        /// <param name="clearBefor">Clear origin file(if exists) befor compress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        public AsyncCompressTask(ICompressor compressor,
            IEnumerable<string> entries, string destFile,
            Encoding encoding, string directoryPathInArchive = null, bool clearBefor = true,
            Action<float> progressCallback = null, Action<bool, string, Exception> finishedCallback = null) :
            base(compressor, clearBefor, progressCallback, finishedCallback)
        {
            this.entries = entries;
            this.destFile = destFile;

            this.encoding = encoding;
            this.directoryPathInArchive = directoryPathInArchive;
        }

        /// <summary>
        /// Execute compress operate.
        /// </summary>
        protected override void Execute()
        {
            compressor.Compress(entries, destFile, encoding,
                directoryPathInArchive, clearBefor, progressCallback, finishedCallback);
        }
    }
}