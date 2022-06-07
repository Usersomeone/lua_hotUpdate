using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MeauController : MonoBehaviour
{
  private Button _start;
  private Button _load;
  private Button _setting;
  private Button _exit;
  [Range(1, 10)]
  public float speed;
  void Start()
  {
    _start = GameObject.Find("StartButton").GetComponent<Button>();
    _load = GameObject.Find("LoadButton").GetComponent<Button>();
    _setting = GameObject.Find("SetButton").GetComponent<Button>();
    _exit = GameObject.Find("ExitButton").GetComponent<Button>();

    _start.onClick.AddListener(StartGame);
  }

  // Update is called once per frame
  void Update()
  {

  }

  void StartGame()
  {
    MovePanel();
    RoleController.Instance.Approach();
    //SceneManager.LoadScene("Main");
    TranslateEffectManager ts = gameObject.AddComponent<TranslateEffectManager>().GetComponent<TranslateEffectManager>();
    ts.TranslateEffect();
  }

  void MovePanel()
  {
    StartCoroutine(Leave());
  }

  IEnumerator Leave()
  {
    while (gameObject.transform.position.y < 700)
    {
      gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + speed * Time.deltaTime * 30);
      yield return null;
    }
    yield return new WaitForSeconds(1f);
  }
}
