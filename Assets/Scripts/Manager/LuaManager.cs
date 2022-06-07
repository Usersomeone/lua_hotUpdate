using UnityEngine;
using XLua;
using System.IO;

public class LuaManager : UnitySingleton<LuaManager>
{
  internal static LuaEnv luaEnv = null;

  public static LuaEnv LuaInstance
  {
    get
    {
      return luaEnv;
    }
  }

  public override void Awake()
  {
    base.Awake();
    this.LuaInit();
  }

  private void LuaInit()
  {
    if (luaEnv == null)
    {
      luaEnv = new LuaEnv();
    }

    LuaTable scriptEnv = luaEnv.NewTable();

    LuaTable meta = luaEnv.NewTable();

    meta.Set("_index", luaEnv.Global);

    scriptEnv.SetMetaTable(meta);

    meta.Dispose();

    scriptEnv.Set("self", this);

    luaEnv.AddLoader(CustomMyLoader);

  }

  private static byte[] CustomMyLoader(ref string fileName)
  {
    byte[] byArray = null;
#if UNITY_EDITOR
    string luaPath = Application.dataPath + "/LuaScripts/" + fileName + ".lua";
    string luaContentByString = File.ReadAllText(luaPath);
    byArray = System.Text.Encoding.UTF8.GetBytes(luaContentByString);
    return byArray;
#endif
    //对应各平台下的可读写文件夹PC平台是streamingAssets文件夹下的ab包文件
    //应该先访问可读写文件夹内的文件资源以实现热更新逻辑
    /* string assetPath = Application.streamingAssetsPath;
    string abPath = assetPath + "/xlua/test";
    AssetBundle ab = AssetBundle.LoadFromFile(abPath);
    Object txt = ab.LoadAsset(fileName, typeof(TextAsset));
    byArray = System.Text.Encoding.UTF8.GetBytes(txt.ToString());
    return byArray; */
  }
  //加载lua入口主脚本
  public void EnterGame()
  {
    this.DoString("require 'main'");
  }

  public void DoString(string loadFilePath)
  {
    luaEnv.DoString(loadFilePath);
  }

  private void OnDestroy()
  {
    luaEnv.Dispose();
  }

  //tips:luaBehaviour要先于LuaManager释放，以避免在luaEnv.Dispose执行时引用未及时清空导致报错！！！

}
