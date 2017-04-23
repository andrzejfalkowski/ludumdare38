using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour 
{
	[SerializeField]
	float xDistance = 10f;
	[SerializeField]
	float yDistance = 10f;

	[SerializeField]
	float time = 10f;

	[SerializeField]
	float minDelay = 3f;
	[SerializeField]
	float maxDelay = 8f;

	[SerializeField]
	Transform spawnMarker1;
	[SerializeField]
	Transform spawnMarker2;

	[SerializeField]
	List<GameObject> prefabs;

	void Start()
	{
		SpawnBird();
	}

	void SpawnBird() 
	{
		GameObject bird = GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Count)]);

		Vector3 startPosition = new Vector3(Random.Range(spawnMarker1.localPosition.x, spawnMarker2.localPosition.x), 
			Random.Range(spawnMarker1.localPosition.y, spawnMarker2.localPosition.y), 0f);

		bird.transform.SetParent(this.transform);
		bird.transform.localPosition = startPosition;
		bird.gameObject.SetActive(true);

		Vector3 target = startPosition + new Vector3(xDistance, yDistance, 0f);
		bird.transform.DOLocalMove(target, time).SetEase(Ease.Linear).OnComplete(()=>{ GameObject.Destroy(bird.gameObject); });

		DOVirtual.DelayedCall(Random.Range(minDelay, maxDelay), ()=>{ SpawnBird(); }, false);
	}
}
