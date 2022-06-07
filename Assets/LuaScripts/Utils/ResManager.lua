--require "Utils/Base"
require "Manager/Define"
ResManager = {}

--Base.LuaObjExtend(ResManager)

--[[ function ResManager:Update()
  print("congratulate!!!")
end ]]


function ResManager.LoadAssetBundleResources(resName)
  local path = CS.Application.streamingAssetsPath .. "/" .. resName;
  return CS.AssetBundle.LoadFromFile(path)
end

return ResManager


--注释代码为继承luaBehaviour实现

--该lua功能是为了提供加载ab包资源的通用方法
