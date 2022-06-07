using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;


public class UI_Ctrl : MonoBehaviour
{
  public Dictionary<string, GameObject> view = new Dictionary<string, GameObject>();

  private void Load_all_objects(GameObject root, string path)
  {
    foreach (Transform ts in root.transform)
    {
      Debug.Log(ts.name);
      Load_all_objects(ts.gameObject, path + ts.gameObject.name + "/");
    }

  }

  public virtual void Awake()
  {
    Load_all_objects(this.gameObject, "");
  }

}

//public class UIManager : UnitySingleton<UIManager>
public class UIManager : MonoBehaviour
{
  public GameObject canvas;
  // string ui_prefab_root = null;

  /*   public override void Awake()
    {
      base.Awake();
      this.canvas = GameObject.Find("Canvas");
      if (this.canvas == null)
      {
        Debug.LogError("canvas component not exits");
      }
    } */

  public UI_Ctrl Show_ui(string name)
  {
    GameObject ui_prefab = null;
    //应该实现一个通用资源加载类来实现在不同模式下的资源加载ResManager.Load()
    GameObject ui_view = GameObject.Instantiate(ui_prefab);

    Type type = Type.GetType(name + "_UICtrl");
    UI_Ctrl ctrl = (UI_Ctrl)ui_view.AddComponent(type);
    return ctrl;

  }

  public void Remove_ui(string name)
  {
    Transform view = this.canvas.transform.Find(name);
    if (view)
    {
      GameObject.Destroy(view.gameObject);
    }
  }

  public void Remove_all_ui()
  {
    List<Transform> children = new List<Transform>();
    foreach (Transform transform in this.canvas.transform)
    {
      children.Add(transform);
    }

    for (var i = 0; i < children.Count; i++)
    {
      GameObject.Destroy(children[i].gameObject);
    }
  }

  public void Add_button_listener(UI_Ctrl ctrl, string view_name, UnityAction onclick)
  {
    Button btn = ctrl.view[view_name].GetComponent<Button>();
    if (btn == null)
    {
      Debug.LogWarning("该组件没有按钮组件");
      return;
    }

    btn.onClick.AddListener(onclick);

  }

  public void Add_slider_listener(UI_Ctrl ctrl, string view_name, UnityAction<float> onclick)
  {
    Slider slider = ctrl.view[view_name].GetComponent<Slider>();
    if (slider == null)
    {
      Debug.LogWarning("该组件没有Slider组件");
      return;
    }

    slider.onValueChanged.AddListener(onclick);

  }

}
