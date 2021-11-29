/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressProcessor.cs
 *  Description  :  Compress processor.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  5/30/2020
 *  Description  :  Initial development version.
 *************************************************************************/

#define USE_IONIC_ZIP

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MGS.Compress
{
    /// <summary>
    /// Compress processor (Singleton, Lazy, Thread safety).
    /// </summary>
    public sealed class CompressProcessor : ICompressProcessor
    {
        #region Singleton
        /// <summary>
        /// Instance of processor (Lazy).
        /// </summary>
        public static CompressProcessor Instance { get { return Agent.instance; } }

        /// <summary>
        /// Agent provide the single instance (Thread safety).
        /// </summary>
        private class Agent { internal static readonly CompressProcessor instance = new CompressProcessor(); }
        #endregion

        #region Field and Property
        /// <summary>
        /// Processor is active?
        /// </summary>
        public bool IsActive { set; get; }

        /// <summary>
        /// Interval of processor run time (ms).
        /// </summary>
        public int Interval { set; get; }

        /// <summary>
        /// Compressor for manager.
        /// </summary>
        public ICompressor Compressor { set; get; }

        /// <summary>
        /// Max count of async operate run in parallel.
        /// </summary>
        public int ParallelCount { set; get; }

        /// <summary>
        /// List to cache tasks.
        /// </summary>
        private List<ITask> taskCache = new List<ITask>();

        /// <summary>
        /// List to cache entries.
        /// </summary>
        private List<string> entryCache = new List<string>();

        /// <summary>
        /// Locker for task cache.
        /// </summary>
        private readonly object locker = new object();
        #endregion

        #region Private Method
        /// <summary>
        /// Constructor.
        /// </summary>
        private CompressProcessor()
        {
            IsActive = true;
            Interval = 200;
#if USE_IONIC_ZIP
            Compressor = new IonicCompressor();
#elif USE_SHARPCOMPRESS
            Compressor = new SharpCompressor();
#endif
            ParallelCount = 10;
            new Thread(ThreadCruise) { IsBackground = true }.Start();
        }

        /// <summary>
        /// Thread cruise.
        /// </summary>
        private void ThreadCruise()
        {
            while (true)
            {
                if (IsActive)
                {
                    ScanTaskExecute();
                }
                Thread.Sleep(Interval);
            }
        }

        /// <summary>
        /// Scan task and execute.
        /// </summary>
        private void ScanTaskExecute()
        {
            if (taskCache.Count == 0)
            {
                return;
            }

            lock (locker)
            {
                var runCount = 0;
                for (int i = 0; i < taskCache.Count; i++)
                {
                    if (runCount >= ParallelCount)
                    {
                        //The rest of the tasks are waiting(Idle state).
                        break;
                    }

                    var task = taskCache[i];
                    switch (task.State)
                    {
                        case TaskState.Idle:
                            if (CheckRunnable(task))
                            {
                                RegisterEntries(task.Entries);
                                task.Start();
                                runCount++;
                            }
                            break;

                        case TaskState.Working:
                            runCount++;
                            break;

                        case TaskState.Finished:
                            UnregisterEntries(task.Entries);
                            RemoveTask(task);
                            i--;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Add task to cache list.
        /// </summary>
        /// <param name="task"></param>
        private void AddTask(ITask task)
        {
            lock (locker)
            {
                taskCache.Add(task);
            }
        }

        /// <summary>
        /// Remove task from cache list.
        /// </summary>
        /// <param name="task"></param>
        private void RemoveTask(ITask task)
        {
            lock (locker)
            {
                taskCache.Remove(task);
            }
        }

        /// <summary>
        /// Register entries to cache entry list.
        /// </summary>
        /// <param name="entries"></param>
        private void RegisterEntries(IEnumerable<string> entries)
        {
            foreach (var entry in entries)
            {
                if (entryCache.Contains(entry))
                {
                    continue;
                }
                entryCache.Add(entry);
            }
        }

        /// <summary>
        /// Unregister entries from cache entry list.
        /// </summary>
        /// <param name="entries"></param>
        private void UnregisterEntries(IEnumerable<string> entries)
        {
            foreach (var entry in entries)
            {
                entryCache.Remove(entry);
            }
        }

        /// <summary>
        /// Check compressor is valid?
        /// </summary>
        /// <param name="compressor"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckCompressor(ICompressor compressor, out Exception error)
        {
            error = null;
            if (compressor == null)
            {
                error = new NullReferenceException("The compressor for manager does not set an instance.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check the task is runnable.
        /// </summary>
        /// <param name="task"></param>
        /// <returns>Task is runnable?</returns>
        private bool CheckRunnable(ITask task)
        {
            if (task.Entries == null)
            {
                return true;
            }

            foreach (var entry in task.Entries)
            {
                if (entryCache.Contains(entry))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region Public Method
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
        public void CompressAsync(IEnumerable<string> entries, string destFile,
            Encoding encoding, string directoryPathInArchive = null, bool clearBefor = true,
            Action<float> progressCallback = null, Action<bool, string, Exception> finishedCallback = null)
        {
            if (entries == null)
            {
                var error = new ArgumentNullException("entries", "The params is invalid.");
                ActionUtility.Invoke(finishedCallback, false, null, error);
                return;
            }

            if (string.IsNullOrEmpty(destFile))
            {
                var error = new ArgumentNullException("destFile", "The params is invalid.");
                ActionUtility.Invoke(finishedCallback, false, null, error);
                return;
            }

            Exception ex = null;
            if (!CheckCompressor(Compressor, out ex))
            {
                ActionUtility.Invoke(finishedCallback, false, null, ex);
                return;
            }

            var task = new AsyncCompressTask(Compressor, entries, destFile, encoding,
                directoryPathInArchive, clearBefor, progressCallback, finishedCallback);

            AddTask(task);
        }

        /// <summary>
        /// Decompress file to dest dir async.
        /// </summary>
        /// <param name="filePath">Target file.</param>
        /// <param name="destDir">The dest decompress directory.</param>
        /// <param name="clearBefor">Clear the dest dir before decompress.</param>
        /// <param name="progressCallback">Progress callback.</param>
        /// <param name="finishedCallback">Finished callback.</param>
        public void DecompressAsync(string filePath, string destDir, bool clearBefor = false,
            Action<float> progressCallback = null, Action<bool, string, Exception> finishedCallback = null)
        {
            if (!File.Exists(filePath))
            {
                var error = new FileNotFoundException("Can not find the file.", filePath);
                ActionUtility.Invoke(finishedCallback, false, null, error);
                return;
            }

            if (string.IsNullOrEmpty(destDir))
            {
                var error = new ArgumentNullException("destDir", "The params is invalid.");
                ActionUtility.Invoke(finishedCallback, false, null, error);
                return;
            }

            Exception ex = null;
            if (!CheckCompressor(Compressor, out ex))
            {
                ActionUtility.Invoke(finishedCallback, false, null, ex);
                return;
            }

            var task = new AsyncDecompressTask(Compressor, filePath, destDir,
                clearBefor, progressCallback, finishedCallback);

            AddTask(task);
        }
        #endregion
    }
}