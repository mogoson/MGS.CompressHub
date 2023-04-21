/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  ICompressor.cs
 *  Description  :  Interface for compressor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/30/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace MGS.Work.Compress
{
    /// <summary>
    /// Interface for compressor.
    /// </summary>
    public interface ICompressor
    {
        #region Method
        /// <summary>
        /// Compress entrie[file or directorie] to dest file.
        /// </summary>
        /// <param name="entries">Target entrie[files or directories].</param>
        /// <param name="destFile">The dest file.</param>
        /// <param name="encoding">Encoding for zip file.</param>
        /// <param name="directoryPathInArchive">Directory path in archive of zip file.</param>
        /// <param name="clearBefor">Clear origin file(if exists) befor compress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        void Compress(IEnumerable<string> entries, string destFile,
            Encoding encoding, string directoryPathInArchive = null, bool clearBefor = true,
            Action<float> progressCallback = null, Action<string, Exception> finishedCallback = null);

        /// <summary>
        /// Decompress file to dest dir.
        /// </summary>
        /// <param name="filePath">Target file.</param>
        /// <param name="destDir">The dest decompress directory.</param>
        /// <param name="clearBefor">Clear the dest dir before decompress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        void Decompress(string filePath, string destDir, bool clearBefor = true,
            Action<float> progressCallback = null, Action<string, Exception> finishedCallback = null);
        #endregion
    }
}