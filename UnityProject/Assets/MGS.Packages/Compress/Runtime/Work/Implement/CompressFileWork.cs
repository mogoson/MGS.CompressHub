/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressFileWork.cs
 *  Description  :  Work for compress file.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  04/12/2023
 *  Description  :  Initial development version.
 *************************************************************************/

using System.Collections.Generic;
using System.Text;

namespace MGS.Work.Compress
{
    /// <summary>
    /// Work for compress file.
    /// </summary>
    public class CompressFileWork : CompressWork
    {
        /// <summary>
        /// Target entrie[files or directories].
        /// </summary>
        protected IEnumerable<string> entries;

        /// <summary>
        /// The dest file.
        /// </summary>
        protected string destFile;

        /// <summary>
        /// Encoding for zip file.
        /// </summary>
        protected Encoding encoding;

        /// <summary>
        /// Directory path in archive of zip file.
        /// </summary>
        protected string directoryPathInArchive;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="compressor">Compressor to execute work.</param>
        /// <param name="entries">Target entrie[files or directories].</param>
        /// <param name="destFile">The dest file.</param>
        /// <param name="encoding">Encoding for zip file.</param>
        /// <param name="directoryPathInArchive">Directory path in archive of zip file.</param>
        /// <param name="clearBefor">Clear origin file(if exists) befor compress.</param>
        public CompressFileWork(ICompressor compressor, IEnumerable<string> entries, string destFile,
            Encoding encoding, string directoryPathInArchive = null, bool clearBefor = true)
            : base(compressor, clearBefor)
        {
            this.entries = entries;
            this.destFile = destFile;
            this.encoding = encoding;
            this.directoryPathInArchive = directoryPathInArchive;
            Key = $"{entries}{destFile}".GetHashCode().ToString();
        }

        /// <summary>
        /// On execute work operation.
        /// </summary>
        protected override void OnExecute()
        {
            compressor.Compress(entries, destFile, encoding,
                directoryPathInArchive, clearBefor,
                progress =>
                {
                    Progress = progress;
                },
                (result, error) =>
                {
                    Result = result;
                    Error = error;
                });
        }
    }
}