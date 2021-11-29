/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/5/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Compress.Demo
{
    public class CompressDemo : MonoCompress
    {
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

        void Start()
        {
            ipt_FilePath.text = string.Format("{0}/TestZipDir/", Environment.CurrentDirectory);
            ipt_ZipName.text = "TestZipFile.zip";
            ipt_RootDir.text = "CustomRootDir";

            btn_StartZip.onClick.AddListener(OnBtnStartZipClick);
        }

        void OnBtnStartZipClick()
        {
            btn_StartZip.interactable = false;
            sbar_Progress.size = 0;
            txt_Info.text = string.Empty;

            var filePath = ipt_FilePath.text.Trim();
            var zipName = ipt_ZipName.text.Trim();
            var zipFile = string.Format("{0}/{1}", Path.GetDirectoryName(filePath), zipName);
            var rootDir = ipt_RootDir.text.Trim();

            CompressProcessor.Instance.CompressAsync(new string[] { filePath }, zipFile, Encoding.UTF8, rootDir, true,
                progress =>
                {
                    BeginInvoke(() =>
                    {
                        //Refresh UI in main thread.
                        sbar_Progress.size = progress;
                    });
                },
                (isSucceed, info, error) =>
                {
                    if (isSucceed)
                    {
                        Debug.Log(info);
                    }
                    else
                    {
                        info = error.Message;
                        Debug.LogError(info);
                    }

                    BeginInvoke(() =>
                    {
                        //Refresh UI in main thread.
                        btn_StartZip.interactable = true;
                        txt_Info.text = info;
                    });
                });
        }
    }
}