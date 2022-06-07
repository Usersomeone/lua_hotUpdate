using System.Collections;
using UnityEngine;

public class GameManager : UnitySingleton<GameManager>
{
  public override void Awake()
  {
    //初始化各类框架和管理类
    base.Awake();
    gameObject.AddComponent<LuaManager>();
    //gameObject.AddComponent<ResManager>();
  }
  //热更新逻辑
  //检查更新，下载资源
  IEnumerator CheckHotUpdate()
  {
    yield return 0;
  }

  IEnumerator StartGame()
  {
    //进入luaManager加载lua脚本准备启动游戏逻辑
    yield return this.StartCoroutine(this.CheckHotUpdate());
    LuaManager.Instance.EnterGame();
    /*  AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/lua/test");
   TextAsset tt = ab.LoadAsset<TextAsset>("base.lua");
   LuaManager.Instance.DoString(tt.ToString()); */
  }

  void Start()
  {
    StartCoroutine(StartGame());
  }


}
