require "Manager/GameManager"

CS.UnityEngine.GameObject.Find("GameApp"):AddComponent(typeof(CS.LuaBehaviour))

GameManager.Init()
