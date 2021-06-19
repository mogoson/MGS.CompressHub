# MGS.Compress

## Summary
- Compress and decompress base Ionic.Zip.dll lib.

## Environment
- Unity 5.0 or above.
- .Net Framework 3.5 or above.

## Platform
- Windows.

## Demand
- Compress entrie[file or directorie] to dest file.
- Decompress file to dest dir.

## Implemented
- ICompressor: Interface for compressor.
- ICompressManager: Interface for compress manager.
- CompressManager: Instance for ICompressManager to manage compress  and decompress tasks.

## Usage
### Native

- Compress async.

  ```c#
  var fileDir = string.Format("{0}/TestFileDir/", Path.GetDirectoryName(filePath));
  var zipFile = string.Format("{0}/TestZipFile.zip", Path.GetDirectoryName(filePath));
  var rootDir = "TestRootDir";
  
  //CompressManager default with IonicCompressor to do compress and decompress tasks.
  CompressManager.Instance.CompressAsync(new string[] { filePath }, zipFile, Encoding.UTF8, rootDir, true,
      progress =>
      {
          //Show progress.
      },
      (isSucceed, info) =>
      {
          //Show result.
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
          //Show progress.
      },
      (isSucceed, info) =>
      {
          //Show result.
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
          Action<bool, string> completeCallback = null)
      {
          //Implemente compress logic.
      }
  
      public void Decompress(string filePath, string destDir,
          bool clearBefor = true,
          Action<float> progressCallback = null,
          Action<bool, string> completeCallback = null)
      {
          //Implemente decompress logic.
      }
  }
  
  //and register to CompressManager.
  CompressManager.Instance.Compressor = new CustomCompressor();
  ```

## Demo
- Demos in the path "./Compress/Demo/Scenes/" provide reference to you.

------

Copyright © 2021 Mogoson.	mogoson@outlook.com