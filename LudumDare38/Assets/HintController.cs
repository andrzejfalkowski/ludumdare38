using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HintController : MonoBehaviour 
{
	[SerializeField]
	CanvasGroup canvasGroup;
	[SerializeField]
	TextMeshProUGUI textMesh;
	[SerializeField]
	Image icon;

	[SerializeField]
	List<string> hints;
	[SerializeField]
	List<Sprite> icons;

	int cnt = 0;

	void Start()
	{
		Next();
	}

	void Next() 
	{
		if(cnt < hints.Count)
		{
			textMesh.text = hints[cnt];

			if(icons[cnt] != null)
			{
				icon.sprite = icons[cnt];
				icon.gameObject.SetActive(true);
			}
			else
			{
				icon.gameObject.SetActive(false);
			}

			cnt++;

			DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 1f, 1f).SetDelay(1f).OnComplete(
				()=>
			{
				DOTween.To(() => canvasGroup.alpha, (a) => canvasGroup.alpha = a, 0f, 1f).SetDelay(8f).OnComplete(
					()=>
				{
					Next();
				}
				);
			}
			);
		}
	}

}
