using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XLua;


public class LuaBehaviour : MonoBehaviour
{

  public LuaMono luaMono = null;
  void Start()
  {
    LuaEnv lua = LuaManager.LuaInstance;

    lua.DoString("require 'Utils/Base' ");

    luaMono = lua.Global.Get<LuaMono>("Base");
  }

  void Update()
  {
    if (luaMono != null)
    {
      luaMono.Update();
    }
    else
    {
      Debug.Log("映射失败!!!");
    }

  }


  private void OnDestroy()
  {
    luaMono = null;
  }

}

[CSharpCallLua]
public interface LuaMono
{
  public void Update();
}
