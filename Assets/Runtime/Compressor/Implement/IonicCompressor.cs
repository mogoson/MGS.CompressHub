/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  IonicCompressor.cs
 *  Description  :  Ionic compressor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/30/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MGS.Work.Compress
{
    /// <summary>
    /// Ionic compressor.
    /// </summary>
    public class IonicCompressor : ICompressor
    {
        #region Public Method
        /// <summary>
        /// Compress entrie[file or directorie] to dest zip file.
        /// </summary>
        /// <param name="entries">Target entrie[files or directories].</param>
        /// <param name="destFile">The dest file.</param>
        /// <param name="encoding">Encoding for zip file.</param>
        /// <param name="directoryPathInArchive">Directory path in archive of zip file.</param>
        /// <param name="clearBefor">Clear origin file(if exists) befor compress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        public virtual void Compress(IEnumerable<string> entries, string destFile,
            Encoding encoding, string directoryPathInArchive = null, bool clearBefor = true,
            Action<float> progressCallback = null, Action<string, Exception> finishedCallback = null)
        {
            try
            {
                if (clearBefor && File.Exists(destFile))
                {
                    File.Delete(destFile);
                }

                using (var zipFile = new ZipFile(destFile, encoding))
                {
                    zipFile.SaveProgress += (s, e) =>
                    {
                        if (e == null || e.EntriesTotal == 0)
                        {
                            return;
                        }

                        var progress = (float)e.EntriesSaved / e.EntriesTotal;
                        progressCallback?.Invoke(progress);
                    };

                    foreach (var entry in entries)
                    {
                        zipFile.AddItem(entry, directoryPathInArchive);
                    }
                    zipFile.Save();
                }
                finishedCallback?.Invoke(destFile, null);
            }
            catch (Exception ex)
            {
                finishedCallback?.Invoke(null, ex);
            }
        }

        /// <summary>
        /// Decompress zip file to dest dir.
        /// </summary>
        /// <param name="filePath">Target file.</param>
        /// <param name="destDir">The dest decompress directory.</param>
        /// <param name="clearBefor">Clear the dest dir before decompress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        public virtual void Decompress(string filePath, string destDir, bool clearBefor = true,
            Action<float> progressCallback = null, Action<string, Exception> finishedCallback = null)
        {
            try
            {
                if (clearBefor && Directory.Exists(destDir))
                {
                    Directory.Delete(destDir, true);
                }

                using (var zipFile = new ZipFile(filePath))
                {
                    zipFile.ExtractProgress += (s, e) =>
                    {
                        if (e == null || e.EntriesTotal == 0)
                        {
                            return;
                        }

                        var progress = (float)e.EntriesExtracted / e.EntriesTotal;
                        progressCallback?.Invoke(progress);
                    };
                    zipFile.ExtractAll(destDir, ExtractExistingFileAction.OverwriteSilently);
                }

                finishedCallback?.Invoke(destDir, null);
            }
            catch (Exception ex)
            {
                finishedCallback?.Invoke(null, ex);
            }
        }
        #endregion
    }
}