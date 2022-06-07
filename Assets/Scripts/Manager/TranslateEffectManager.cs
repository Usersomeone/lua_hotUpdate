using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateEffectManager : MonoBehaviour
{
  public MaskOfScene mask;

  public void TranslateEffect()
  {
    var camera = GameObject.Find("Main Camera");
    camera.AddComponent<MaskOfScene>();
    mask = camera.GetComponent<MaskOfScene>();
    StartCoroutine(ExecuteEffect());
  }

  public void StopTranslateEffect()
  {
    if (mask != null)
      mask.enabled = false;
  }

  IEnumerator ExecuteEffect()
  {
    while (mask.radius < 1)
    {
      mask.radius += Time.deltaTime;
      yield return null;
    }
    yield return new WaitForSeconds(1.0f);
    Debug.Log("waiting");
  }


}
