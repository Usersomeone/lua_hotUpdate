using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UIScriptCreate : EditorWindow
{

  [MenuItem("UIScriptCreate/uiscript_auto_create")]
  private static void ShowWindow()
  {
    var window = GetWindow<UIScriptCreate>();
    window.titleContent = new GUIContent("UIScriptCreate");
    window.Show();
  }

  private void OnGUI()
  {
    GUILayout.Label("选择一个UI视图根节点");
    if (GUILayout.Button("生成代码"))
    {
      if (Selection.activeGameObject != null)
      {
        Debug.Log("开始生成...");
        CreateUICtrlScript.CreateUISourceFile(Selection.activeGameObject);
        Debug.Log("代码生成完成");
        AssetDatabase.Refresh();
      }
    }
    if (Selection.activeGameObject != null)
    {
      GUILayout.Label(Selection.activeGameObject.name);
    }
    else
    {
      GUILayout.Label("没有选中激活的UI节点，代码无法生成");
    }
  }
}


