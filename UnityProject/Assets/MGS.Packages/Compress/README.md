[TOC]

# MGS.Compress

## Summary
- Compress and decompress file.

## Environment
- Unity 5.0 or above.
- .Net Framework 3.5 or above.

## Platform
- Windows.

## Demand
- Compress entrie[file or directorie] to dest file.
- Decompress file to dest dir.

## Implemented
```C#
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
  CompressManager.Instance.CompressAsync(new string[] { filePath }, zipFile, Encoding.UTF8, rootDir, true,
      progress =>
      {
          //TODO: Show progress.
      },
      (isSucceed, info) =>
      {
          //TODO: Show result.
          //if isSucceed==true, the type of info is string
          //and content is the path of zipFile;
          
          //if isSucceed==false, the type of info is Exception.
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
          //if isSucceed==true, the type of info is string
          //and content is the path of unzip dir;
          
          //if isSucceed==false, the type of info is Exception.
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

## Demo
- Demos in the path "MGS.Packages/Compress/Demo/" provide reference to you.

## Source
- https://github.com/mogoson/MGS.Compress.

------

Copyright © 2021 Mogoson.	mogoson@outlook.com