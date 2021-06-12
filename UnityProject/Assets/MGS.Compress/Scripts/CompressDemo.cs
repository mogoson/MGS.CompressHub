/*************************************************************************
 *  Copyright (c) 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressDemo.cs
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
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Compress
{
    //[AddComponentMenu("")]
    //[RequireComponent(typeof())]
    public class CompressDemo : MonoBehaviour
    {
        #region Field and Property
        //  [Tooltip("")]
        [SerializeField]
        InputField ipt_FilePath;

        [SerializeField]
        InputField ipt_ZipName;

        [SerializeField]
        InputField ipt_RootDir;

        [SerializeField]
        Button btn_StartZip;

        [SerializeField]
        Scrollbar sbar_Progress;

        [SerializeField]
        Text txt_Info;
        #endregion

        #region Private Method
        // Use this for initialization.
        void Start()
        {
            ipt_FilePath.text = string.Format("{0}/TestZipDir/", Environment.CurrentDirectory);
            ipt_ZipName.text = "TestZipFile.zip";
            ipt_RootDir.text = "CustomRootDir";

            btn_StartZip.onClick.AddListener(OnBtn_StartZip_Click);
        }

        // Update is called once per frame.
        //void Update()
        //{

        //}

        void OnBtn_StartZip_Click()
        {
            btn_StartZip.interactable = false;
            sbar_Progress.size = 0;
            txt_Info.text = string.Empty;

            var filePath = ipt_FilePath.text.Trim();
            var zipName = ipt_ZipName.text.Trim();
            var zipFile = string.Format("{0}/{1}", Path.GetDirectoryName(filePath), zipName);
            var rootDir = ipt_RootDir.text.Trim();

            CompressManager.Instance.CompressAsync(new string[] { filePath }, zipFile, Encoding.UTF8, rootDir, true,
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
                        btn_StartZip.interactable = true;
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