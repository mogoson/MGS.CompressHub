/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DecompressFileWork.cs
 *  Description  :  Work for decompress file.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  04/12/2023
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Work.Compress
{
    /// <summary>
    /// Work for decompress file.
    /// </summary>
    public class DecompressFileWork : CompressWork
    {
        /// <summary>
        /// The target zip file path.
        /// </summary>
        protected string filePath;

        /// <summary>
        /// The target decompress directory.
        /// </summary>
        protected string destDir;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="compressor">Compressor to execute work.</param>
        /// <param name="filePath">The target zip file path.</param>
        /// <param name="destDir">The target decompress directory.</param>
        /// <param name="clearBefor">Clear origin file(if exists) befor compress.</param>
        public DecompressFileWork(ICompressor compressor,
            string filePath, string destDir, bool clearBefor = true)
            : base(compressor, clearBefor)
        {
            this.filePath = filePath;
            this.destDir = destDir;
            Key = $"{filePath}{destDir}".GetHashCode().ToString();
        }

        /// <summary>
        /// Execute work operation.
        /// </summary>
        public override void Execute()
        {
            compressor.Decompress(filePath, destDir, clearBefor,
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