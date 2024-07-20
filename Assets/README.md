[TOC]

# MGS.CompressHub

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

## Usage
### Native

- Hub instance.

  ```c#
  var compressHub = new CompressHub();
  ```

  

- Compress async.

  ```c#
  var fileDir = string.Format("{0}/TestFileDir/", Path.GetDirectoryName(filePath));
  var zipFile = string.Format("{0}/TestZipFile.zip", Path.GetDirectoryName(filePath));
  var rootDir = "TestRootDir";
  
  var handler = compressHub.CompressAsync(new string[] { filePath }, zipFile, Encoding.UTF8, rootDir, true);
  handler.OnProgressChanged += progress =>
  {
      //TODO: Show progress.
  };
  handler.OnCompleted += (info, error) =>
  {
      //TODO: Show result.
  }
  ```
  
- Decompress async.

  ```C#
  var filePath = string.Format("{0}/TestZipFile.zip", Path.GetDirectoryName(filePath));
  var unzipDirPath = string.Format("{0}/TestZipDir/", Path.GetDirectoryName(filePath));
  
  var handler = compressHub.DecompressAsync(filePath, unzipDirPath, true);
  handler.OnProgressChanged += progress =>
  {
      //TODO: Show progress.
  };
  handler.OnCompleted += (info, error) =>
  {
      //TODO: Show result.
  }
  ```

------

Copyright © 2021 Mogoson.	mogoson@outlook.com