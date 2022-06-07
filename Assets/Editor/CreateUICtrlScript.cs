using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;


public class CreateUICtrlScript
{
  public static void CreateUISourceFile(GameObject selectObject)
  {
    string gameObjectName = selectObject.name;
    string fileName = gameObjectName + "_UISrc";
    string className = gameObjectName + "_UICtrl";

    string scriptsPath = Application.dataPath + "/Scripts/UI_Controller/" + fileName + ".cs";

    string content = "using UnityEngine;\nusing System.Collections;\nusing UnityEngine.UI;\nusing System.Collections.Generic;\npublic class " + className + " : UI_Ctrl " + "\n{\n  public  override void Awake()\n  {\n    base.Awake();\n  }\n  void Start()\n  {\n\n  }\n}";

    if (File.Exists(scriptsPath) != false)
    {
      Debug.Log("文件已存在！！！");
    }

    using (FileStream fs = File.Create(scriptsPath))
    {
      byte[] info = new UTF8Encoding(true).GetBytes(content);
      fs.Write(info, 0, info.Length);
    }

  }

}
