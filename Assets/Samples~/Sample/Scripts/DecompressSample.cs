/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DecompressSample.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/5/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System;
using System.IO;
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

        ICompressHub compressHub;

        void Start()
        {
            ipt_ZipFile.text = string.Format("{0}/TestZipDir/TestZipFile.zip", Environment.CurrentDirectory);
            ipt_UnzipDir.text = "TestUnzipDir";
            btn_StartUnzip.onClick.AddListener(OnBtnStartUnzipClick);

            compressHub = new CompressHub();
        }

        void OnBtnStartUnzipClick()
        {
            btn_StartUnzip.interactable = false;
            sbar_Progress.size = 0;
            txt_Info.text = string.Empty;

            var filePath = ipt_ZipFile.text.Trim();
            var unzipDirName = ipt_UnzipDir.text.Trim();
            var unzipDirPath = string.Format("{0}/{1}/", Path.GetDirectoryName(filePath), unzipDirName);

            var handler = compressHub.DecompressAsync(filePath, unzipDirPath, true);
            handler.OnProgressChanged += progress =>
            {
                sbar_Progress.size = progress;
            };
            handler.OnCompleted += (info, error) =>
            {
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