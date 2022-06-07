using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskOfScene : PostEffectsBase
{
  public Shader shader;
  private Material _material = null;

  [Range(0, 1)]
  public float radius;

  void Awake()
  {
    if (shader == null)
    {
      shader = Resources.Load<Shader>("Shader/CircleShader");
    }
  }

  public Material material
  {
    get
    {
      _material = CheckShaderAndCreateMaterial(shader, _material);
      return _material;
    }
  }

  void OnRenderImage(RenderTexture src, RenderTexture dest)
  {
    material.SetFloat("_Radius", radius);
    Graphics.Blit(src, dest, material);
  }


}
