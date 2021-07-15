/*************************************************************************
 *  Copyright © 2020 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  LoggerTest.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  11/28/2020
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Logger
{
    public class Filter : IFilter
    {
        public bool Select(string tag, string format, params object[] args)
        {
            return tag.Contains(FileLogger.TAG_ERROR);
        }
    }

    public class LoggerTest : MonoBehaviour
    {
        #region Private Method
        private void Awake()
        {
            //Register another logger with filter to output error logs.
            var logDir = string.Format("{0}/Log/", System.Environment.CurrentDirectory);
            var errorLogger = new FileLogger(logDir, new Filter());
            LogUtility.Register(errorLogger);

            //This log can not output to the default logger before Unity5.3, see LogUtilityInitializer.Awake to learn more.
            LogUtility.Log("LoggerTest Awake");
        }

        private void Start()
        {
            //Just simple example,
            //LogUtility is usually used for libraries that don't rely on Unity.
            LogUtility.LogWarning("LoggerTest Start");
        }

        private void OnDestroy()
        {
            LogUtility.LogError("LoggerTest OnDestroy");
        }
        #endregion
    }
}