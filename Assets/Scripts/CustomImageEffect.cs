using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CustomImageEffect : MonoBehaviour
{
	[Range(64.0f, 512.0f)] public float PixelationAmount = 128;
	public Material outlineImageEffect;
	public Material pixelationImageEffect;

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		float k = Camera.main.aspect;
		Vector2 count = new Vector2(PixelationAmount, PixelationAmount / k);
		Vector2 size = new Vector2(1.0f / count.x, 1.0f / count.y);

		pixelationImageEffect.SetVector("BlockCount", count);
		pixelationImageEffect.SetVector("BlockSize", size);

		//RenderTexture renderTemp = RenderTexture.GetTemporary(src.width, src.height);
		Graphics.Blit(src, dest, pixelationImageEffect);
		//Graphics.Blit(renderTemp, dest, pixelationImageEffect);

		//RenderTexture.ReleaseTemporary(renderTemp);
	}
}
