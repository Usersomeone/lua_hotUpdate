require "/Scripts/XLuaFrameWork/Common/Define"

require "/Scripts/XLuaFrameWork/Modules/UIRoot/UIRootControll"

ControllManager = {};

local this = ControllManager;

local ControllList = {};

function ControllManager.Init()
  ControllList[ControllNames.UIRootControll] = UIRootControll.New()
  return this
end

function ControllManager.GetControll(ctrlName)
  return ControllList[ctrlName]
end
