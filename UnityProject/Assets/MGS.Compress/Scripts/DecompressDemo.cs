/*************************************************************************
 *  Copyright (c) 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DecompressDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/5/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using MGS.UCommon.Threading;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Compress
{
    //[AddComponentMenu("")]
    //[RequireComponent(typeof())]
    public class DecompressDemo : MonoBehaviour
    {
        #region Field and Property
        //  [Tooltip("")]
        [SerializeField]
        InputField ipt_ZipFile;

        [SerializeField]
        InputField ipt_UnzipDir;

        [SerializeField]
        Button btn_StartUnzip;

        [SerializeField]
        Scrollbar sbar_Progress;

        [SerializeField]
        Text txt_Info;
        #endregion

        #region Private Method
        // Use this for initialization.
        void Start()
        {
            ipt_ZipFile.text = string.Format("{0}/TestZipDir/TestZipFile.zip", Environment.CurrentDirectory);
            ipt_UnzipDir.text = "TestUnzipDir";

            btn_StartUnzip.onClick.AddListener(OnBtn_StartUnzip_Click);
        }

        // Update is called once per frame.
        //void Update()
        //{

        //}

        void OnBtn_StartUnzip_Click()
        {
            btn_StartUnzip.interactable = false;
            sbar_Progress.size = 0;
            txt_Info.text = string.Empty;

            var filePath = ipt_ZipFile.text.Trim();
            var unzipDirName = ipt_UnzipDir.text.Trim();
            var unzipDirPath = string.Format("{0}/{1}/", Path.GetDirectoryName(filePath), unzipDirName);

            CompressManager.Instance.DecompressAsync(filePath, unzipDirPath, true,
                progress =>
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        sbar_Progress.size = progress;
                    });
                },
                (isSucceed, info) =>
                {
                    Dispatcher.BeginInvoke(() =>
                    {
                        btn_StartUnzip.interactable = true;
                        txt_Info.text = info;

                        if (isSucceed) { Debug.Log(info); }
                        else { Debug.LogError(info); }
                    });
                });
        }
        #endregion

        #region Public Method
        #endregion
    }
}