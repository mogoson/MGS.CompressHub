[TOC]

﻿# MGS.Compress.dll

## Summary

- Compress and decompress base Ionic.Zip.dll lib.

## Environment

- .Net Framework 3.5 or above.

## Dependence

- System.dll
- Ionic.Zip.dll
- MGS.DesignPattern.dll

## Demand

- Compress entrie[file or directorie] to dest file.
- Decompress file to dest dir.

## Implemented

```c#
public interface ICompressor{}

public interface ICompressManager{}

public sealed class CompressManager : SingleTimer<CompressManager>, ICompressManager{}
```

## Usage
### Native

- Compress async.

  ```c#
  var fileDir = string.Format("{0}/TestFileDir/", Path.GetDirectoryName(filePath));
  var zipFile = string.Format("{0}/TestZipFile.zip", Path.GetDirectoryName(filePath));
  var rootDir = "TestRootDir";
  
  //CompressManager default with IonicCompressor to do compress and decompress tasks.
  CompressManager.Instance.CompressAsync(new string[] { filePath }, 
      zipFile, Encoding.UTF8, rootDir, true,
      progress =>
      {
          //TODO: Show progress.
      },
      (isSucceed, info) =>
      {
          //TODO: Show result.
          //if isSucceed==true, the info is the path of zipFile;
          //if isSucceed==false, the info is error message.
      });
  ```

- Decompress async.

  ```C#
  var filePath = string.Format("{0}/TestZipFile.zip", Path.GetDirectoryName(filePath));
  var unzipDirPath = string.Format("{0}/TestZipDir/", Path.GetDirectoryName(filePath));
  
  //CompressManager default with IonicCompressor to do compress  and decompress tasks.
  CompressManager.Instance.DecompressAsync(filePath, unzipDirPath, true,
      progress =>
      {
          //TODO: Show progress.
      },
      (isSucceed, info) =>
      {
          //TODO: Show result.
          //if isSucceed==true, the info is the path of unzip dir;
          //if isSucceed==false, the info is error message.
      });
  ```

### Expand

- Custom compressor.

  ```C#
  //Implemente the interface ICompressor,
  public class CustomCompressor : ICompressor
  {
      public void Compress(IEnumerable<string> entries, string destFile,
          Encoding encoding, string directoryPathInArchive = null,
          bool clearBefor = true,
          Action<float> progressCallback = null,
          Action<bool, object> completeCallback = null)
      {
          //TODO: Implemente compress logic.
          //Usually completeCallback.Invoke(false, new Exception(msg)) on error.
      }
  
      public void Decompress(string filePath, string destDir,
          bool clearBefor = true,
          Action<float> progressCallback = null,
          Action<bool, object> completeCallback = null)
      {
          //TODO: Implemente decompress logic.
          //Usually completeCallback.Invoke(false, new Exception(msg)) on error.
      }
  }
  
  //and register to CompressManager.
  CompressManager.Instance.Compressor = new CustomCompressor();
  ```

------

[Previous](../../README.md)

------

Copyright © 2021 Mogoson.	mogoson@outlook.com