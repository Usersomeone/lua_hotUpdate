Base = {}
Base.ListMap = {}

local function addMember(class, list)
  if Base.Find(class, list) == nil then
    table.insert(list, class)
  end
end

function Base.Remove(class, list)
  --异常处理未实现，日志收集类进行信息收集
  local position = Base.Find(class, list)
  if position ~= nil then
    table.remove(list, position)
    -- else
    ---异常收集
  end
end

function Base.Find(target, list)
  for k, v in ipairs(list) do
    if v == target then
      return k
    else
      return nil
    end
  end
end

function Base.LuaObjExtend(class)
  addMember(class, Base.ListMap)
end

function Base.Update()
  if #Base.ListMap > 0 then
    for _, v in ipairs(Base.ListMap) do
      if (v ~= nil) then
        v.Update()
      end
    end
  end
end

return Base



--addmember、removed作为通用的方法处理各种委托表，要拓展功能直接模仿update进行实现,可以使用元表进行功能拓展
