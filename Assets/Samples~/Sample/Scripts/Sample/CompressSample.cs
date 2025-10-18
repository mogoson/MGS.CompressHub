/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  CompressSample.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  09/26/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Compress.Sample
{
    public class CompressSample : MonoBehaviour
    {
        [SerializeField]
        InputField ipt_FilePath;

        [SerializeField]
        InputField ipt_ZipName;

        [SerializeField]
        Button btn_StartZip;

        [SerializeField]
        Scrollbar sbar_Progress;

        [SerializeField]
        Text txt_Info;

        void Start()
        {
            ipt_FilePath.text = $"{Environment.CurrentDirectory}/TestDir/TestZipDir/";
            ipt_ZipName.text = "TestZipFile.zip";
            btn_StartZip.onClick.AddListener(OnBtnStartZipClick);
        }

        void OnBtnStartZipClick()
        {
            btn_StartZip.interactable = false;
            sbar_Progress.size = 0;
            txt_Info.text = string.Empty;

            var filePath = ipt_FilePath.text.Trim();
            var zipName = ipt_ZipName.text.Trim();
            var zipFile = $"{Environment.CurrentDirectory}/TestDir/{zipName}";

            var handler = Global.CompressHub.CompressAsync(filePath, zipFile, Encoding.UTF8, true);
            handler.OnProgressed += progress =>
            {
                sbar_Progress.size = progress;
            };
            handler.OnCompleted += (info, error) =>
            {
                sbar_Progress.size = 1.0f;
                if (error == null)
                {
                    Debug.Log(info);
                }
                else
                {
                    info = error.Message;
                    Debug.LogError(info);
                }

                btn_StartZip.interactable = true;
                txt_Info.text = info;
            };
        }
    }
}