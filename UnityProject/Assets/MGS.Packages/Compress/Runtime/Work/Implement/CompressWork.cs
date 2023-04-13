/*************************************************************************
 *  Copyright © 2023 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressWork.cs
 *  Description  :  Work for compress.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  04/12/2023
 *  Description  :  Initial development version.
 *************************************************************************/

namespace MGS.Work.Compress
{
    /// <summary>
    /// Work for compress.
    /// </summary>
    public abstract class CompressWork : AsyncWork<string>, ICompressWork
    {
        /// <summary>
        /// Compressor to execute work.
        /// </summary>
        protected ICompressor compressor;

        /// <summary>
        /// Clear origin file(if exists) befor compress.
        /// </summary>
        protected bool clearBefor = true;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="compressor">Compressor to execute work.</param>
        /// <param name="clearBefor">Clear origin file(if exists) befor compress.</param>
        public CompressWork(ICompressor compressor, bool clearBefor = true)
        {
            this.compressor = compressor;
            this.clearBefor = clearBefor;
        }
    }
}