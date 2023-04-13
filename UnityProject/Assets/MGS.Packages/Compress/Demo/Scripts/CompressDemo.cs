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

using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MGS.Work.Compress.Demo
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
            ipt_FilePath.text = $"{Application.dataPath}/TestZipDir/";
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

            handler = hub.CompressAsync(new string[] { filePath }, zipFile, Encoding.UTF8, rootDir, true);
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

                btn_StartZip.interactable = true;
                txt_Info.text = result;
            };
        }
    }
}