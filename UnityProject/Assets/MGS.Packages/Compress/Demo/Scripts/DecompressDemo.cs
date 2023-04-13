/*************************************************************************
 *  Copyright © 2021 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  DecompressDemo.cs
 *  Description  :  Null.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  0.1.0
 *  Date         :  6/5/2021
 *  Description  :  Initial development version.
 *************************************************************************/

using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Work.Compress.Demo
{
    public class DecompressDemo : MonoCompress
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
            ipt_ZipFile.text = $"{Application.dataPath}/TestZipDir/TestZipFile.zip";
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
            var unzipDirPath = $"{Path.GetDirectoryName(filePath)}/{unzipDirName}/";

            handler = hub.DecompressAsync(filePath, unzipDirPath, true);
            handler.OnProgressChanged += progress =>
            {
                sbar_Progress.size = progress;
            };
            handler.OnCompleted += (result, error) =>
            {
                if (error == null)
                {
                    Debug.Log(result);
                }
                else
                {
                    result = error.Message;
                    Debug.LogError(result);
                }

                btn_StartUnzip.interactable = true;
                txt_Info.text = result;
            };
        }
    }
}