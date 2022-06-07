using System.Collections;
using UnityEngine;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using UnityEditor;

public class LuaScriptCreator
{
  [MenuItem("Assets/Create/xLua Script", false, 80)]

  public static void CreateNewLua()
  {
    ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
    ScriptableObject.CreateInstance<CreateScriptAssetAction>(),
    GetSelectedPathOrFallback() + "/New Lua.lua",
    EditorGUIUtility.FindTexture("Assets/Editor/Template/]0YZ$CY_}258PP{2[3~214Q.png") as Texture2D,
    "Assets/Editor/Template/LuaComponent.lua"
    );
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

  class CreateScriptAssetAction : EndNameEditAction
  {
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
      UnityEngine.Object obj = CreateAssetFromTemplate(pathName, resourceFile);
      ProjectWindowUtil.ShowCreatedAsset(obj);
    }
    internal static UnityEngine.Object CreateAssetFromTemplate(string pathName, string resourceFile)
    {
      string fullName = Path.GetFullPath(pathName);
      StreamReader reader = new StreamReader(resourceFile);
      //线程安全函数Synchronized进行包装
      TextReader safeReader = StreamReader.Synchronized(reader);
      string content = safeReader.ReadToEnd();
      safeReader.Close();
      reader.Close();

      content = content.Replace("#TIME", System.DateTime.Now.ToString());
      StreamWriter writer = new StreamWriter(fullName, false);
      TextWriter safeWriter = StreamWriter.Synchronized(writer);
      safeWriter.Write(content);
      safeWriter.Close();
      writer.Close();

      AssetDatabase.ImportAsset(pathName);
      AssetDatabase.Refresh();

      return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.TextAsset));

    }
  }

}
