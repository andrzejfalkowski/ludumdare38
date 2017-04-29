using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour 
{
	public static GameplayManager Instance = null;

	public PlayerLogic Player;
	public PlayerLogic Opponent;

	public bool GameOver = false;

	void Awake()
	{
		Instance = this;
	}

	[SerializeField]
	Transform meeplesParent;
	[SerializeField]
	GameObject meeplePrefab;

	public List<Meeple> MeeplesOnMap;

	public void CreateMeeple(PlayerLogic player, Vector2 position, int alliesInRange)
	{
		GameObject meeple = GameObject.Instantiate(meeplePrefab);
		meeple.transform.SetParent(meeplesParent);
		meeple.transform.localScale = Vector3.one;
		meeple.transform.localPosition = position;

		meeple.GetComponent<Meeple>().Init(player, alliesInRange);
	}

	void Start()
	{
		UnityEngine.Cursor.visible = false;
	}

	void Update()
	{
		if(!GameOver)
		{
			if(Player.Population >= Constants.ECONOMIC_VICTORY_THRESHOLD)
			{
				UIManager.Instance.Update();
				GameOver = true;
				UIManager.Instance.ShowResults(EGameOverResult.Victory, EGameOverType.Economic);
				UnityEngine.Cursor.visible = true;
			}
			else if(Opponent.Population >= Constants.ECONOMIC_VICTORY_THRESHOLD)
			{
				UIManager.Instance.Update();
				GameOver = true;
                UIManager.Instance.ShowResults(EGameOverResult.Defeat, EGameOverType.Economic);
                UnityEngine.Cursor.visible = true;
            }
            else if (Opponent.Population <= 0)
            {
                UIManager.Instance.Update();
                GameOver = true;
                UIManager.Instance.ShowResults(EGameOverResult.Victory, EGameOverType.Military);
                UnityEngine.Cursor.visible = true;
            }
            else if (Player.Population <= 0)
            {
                UIManager.Instance.Update();
                GameOver = true;
                UIManager.Instance.ShowResults(EGameOverResult.Defeat, EGameOverType.Military);
                UnityEngine.Cursor.visible = true;
            }
        }

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			UnityEngine.Cursor.visible = true;
			SceneManager.LoadScene("MainMenu");
		}
	}


	void OnDestroy()
	{
		Instance = null;
	}
}
