/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  SharpCompressor.cs
 *  Description  :  Sharp compressor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/30/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using SharpCompress.Writers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MGS.Compress
{
    /// <summary>
    /// Sharp compressor.
    /// </summary>
    public class SharpCompressor : ICompressor
    {
        #region Public Method
        /// <summary>
        /// Compress entrie[file or directorie] to dest zip file.
        /// </summary>
        /// <param name="entries">Target entrie[files or directories].</param>
        /// <param name="destFile">The dest file.</param>
        /// <param name="encoding">Encoding for zip file.</param>
        /// <param name="directoryPathInArchive">Directory path in archive of zip file [Not supported this version].</param>
        /// <param name="clearBefor">Clear origin file(if exists) befor compress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        public virtual void Compress(IEnumerable<string> entries, string destFile,
            Encoding encoding, string directoryPathInArchive = null, bool clearBefor = true,
            Action<float> progressCallback = null, Action<bool, string, Exception> finishedCallback = null)
        {
            try
            {
                if (clearBefor && File.Exists(destFile))
                {
                    File.Delete(destFile);
                }

                using (var archive = ZipArchive.Create())
                {
                    var index = 0f;
                    var count = new List<string>(entries).Count;
                    foreach (var entry in entries)
                    {
                        archive.AddAllFromDirectory(entry);
                        index++;

                        var progress = (index / count) * 0.75f;
                        ActionUtility.Invoke(progressCallback, progress);
                    }

                    using (var stream = File.OpenWrite(destFile))
                    {
                        var archiveEncoding = new ArchiveEncoding { Default = encoding };
                        var options = new WriterOptions(CompressionType.Deflate) { ArchiveEncoding = archiveEncoding };
                        archive.SaveTo(stream, options);
                    }
                }

                ActionUtility.Invoke(progressCallback, 1.0f);
                ActionUtility.Invoke(finishedCallback, true, destFile, null);
            }
            catch (Exception ex)
            {
                ActionUtility.Invoke(finishedCallback, false, null, ex);
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
            Action<float> progressCallback = null, Action<bool, string, Exception> finishedCallback = null)
        {
            try
            {
                if (clearBefor)
                {
                    if (Directory.Exists(destDir))
                    {
                        Directory.Delete(destDir, true);
                    }
                }

                using (var archive = ArchiveFactory.Open(filePath))
                {
                    var totalSize = archive.TotalUncompressSize;
                    long unzipSize = 0;
                    archive.EntryExtractionEnd += (s, e) =>
                    {
                        unzipSize += e.Item.Size;
                        var progress = (float)unzipSize / totalSize;
                        ActionUtility.Invoke(progressCallback, progress);
                    };

                    var options = new ExtractionOptions()
                    {
                        Overwrite = true,
                        ExtractFullPath = true,
                        PreserveFileTime = false,
                        PreserveAttributes = false
                    };

                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            entry.WriteToDirectory(destDir, options);
                        }
                    }
                }

                ActionUtility.Invoke(finishedCallback, true, destDir, null);
            }
            catch (Exception ex)
            {
                ActionUtility.Invoke(finishedCallback, false, null, ex);
            }
        }
        #endregion
    }
}