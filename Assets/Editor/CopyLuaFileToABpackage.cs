using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CopyLuaFileToABpackage
{
  [MenuItem("Assets/CopyLuaScriptToAssetPackage", false, 50)]
  public static void MoveFile()
  {
    string targetFolderPath = GetSelectedPathOrFallback();
    CopyToAssetFloder(targetFolderPath);
    Debug.Log("Copy--Finish!");
  }


  public static string GetSelectedPathOrFallback()
  {
    string path = "Assets";
    foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
    {
      path = AssetDatabase.GetAssetPath(obj);
      if (!string.IsNullOrEmpty(path) && File.Exists(path))
      {
        path = Path.GetDirectoryName(path);
        break;
      }
    }
    return path;
  }

  static void CopyToAssetFloder(string folderPath)
  {

    try
    {
      DirectoryInfo dir = new DirectoryInfo(folderPath);

      DirectoryInfo[] childFolder = dir.GetDirectories("*", SearchOption.TopDirectoryOnly);

      FileInfo[] files = dir.GetFiles("*.lua", SearchOption.TopDirectoryOnly);

      if (files.Length > 0)
      {
        foreach (var file in files)
        {
          string filePath = file.FullName;
          string destinationPath = filePath.Replace("LuaScripts", "AssetPackage\\Lua");
          string finalFileFormat = destinationPath + ".byte";
          FileInfo targetFile = new FileInfo(filePath);
          targetFile.CopyTo(finalFileFormat, true);
        }
      }


      if (childFolder.Length > 0)
      {
        foreach (var folder in childFolder)
        {
          string momentFolderPath = folder.FullName;
          string finalFolderPath = momentFolderPath.Replace("LuaScripts", "AssetPackage\\Lua");
          DirectoryInfo finalFolderInfo = new DirectoryInfo(finalFolderPath);
          if (finalFolderInfo.Exists == false)
          {
            finalFolderInfo.Create();
          }
          CopyToAssetFloder(momentFolderPath);
        }
      }

    }
    catch (Exception e)
    {
      Debug.LogError(e);
    }
  }

}
