--require "/Scripts/XLuaFrameWork/Common/ControllManager"
--require "Utils/ResManager"
require "Utils/Base"

GameManager = {}
local this = GameManager

Base.LuaObjExtend(this)

function GameManager.InitViews()
  require("/Scripts/XLuaFrameWork/Modules/UIRoot/UIRootView")
end

local function checkNum()
  local currentNum = CS.Test
  currentNum.num = 500
end

function GameManager.Init()
  --[[   this.InitViews()
  ControllManager.Init()
  GameManager.loadView(ControllNames.UIRootControll) ]]
  CS.UnityEngine.Debug.Log("123...")
  --[[   local path = CS.UnityEngine.Application.streamingAssetsPath .. "/" .. "ui_scene"
  local ab = CS.UnityEngine.AssetBundle.LoadFromFile(path)
  local scene = ab:GetAllScenePaths();
  CS.UnityEngine.SceneManagement.SceneManager.LoadScene(scene[0], CS.UnityEngine.SceneManagement.LoadSceneMode.Additive) ]]
end

function GameManager.loadView(type)
  local ctrl = ControllManager.GetControll(type)
  if ctrl ~= nil then
    ctrl.Awake()
  end
end

function this:Update()
  if CS.UnityEngine.Input.GetKeyDown(CS.UnityEngine.KeyCode.T) then
    --[[     local target = CS.UnityEngine.SceneManagement.SceneManager.sceneCount
    CS.UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("MainMeauScene") ]]
    checkNum()
  end
end

--[[ 
string.split = function(s, p)
  local rt = {}
  string.gsub(s, '[^' .. p .. ']+', function(w) table.insert(rt, w) end)
  return rt
end ]]
