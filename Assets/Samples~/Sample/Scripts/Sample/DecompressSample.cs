/*************************************************************************
 *  Copyright © 2025 Mogoson All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DecompressSample.cs
 *  Description  :  Default.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0.0
 *  Date         :  09/26/2025
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Compress.Sample
{
    public class DecompressSample : MonoBehaviour
    {
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

        void Start()
        {
            ipt_ZipFile.text = $"{Environment.CurrentDirectory}/TestDir/TestZipFile.zip";
            ipt_UnzipDir.text = "TestUnzipDir";
            btn_StartUnzip.onClick.AddListener(OnBtnStartUnzipClick);
        }

        void OnBtnStartUnzipClick()
        {
            btn_StartUnzip.interactable = false;
            sbar_Progress.size = 0;
            txt_Info.text = string.Empty;

            var filePath = ipt_ZipFile.text.Trim();
            var unzipDirName = ipt_UnzipDir.text.Trim();
            var unzipDirPath = $"{Environment.CurrentDirectory}/TestDir/{unzipDirName}";

            var handler = Global.CompressHub.DecompressAsync(filePath, unzipDirPath, true);
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

                btn_StartUnzip.interactable = true;
                txt_Info.text = info;
            };
        }
    }
}