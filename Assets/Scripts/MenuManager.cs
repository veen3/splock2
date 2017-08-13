using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

	private PlayerController _pc;

	private Text scoreDisplay;

	public CanvasGroup gameOverGroup;


	private float highScore;


	void Start(){
		_pc = GameObject.Find("Player").GetComponent<PlayerController>();

		highScore = PlayerPrefs.GetFloat("HighScore", 0.0f);
	}

	public void GameOver(float score){
		scoreDisplay = GameObject.Find("Score").GetComponent<Text>();

		highScore = PlayerPrefs.GetFloat("HighScore", 0.0f);

		if(highScore == 0.0f){
			scoreDisplay.text = "SCORE\n" + score.ToString("F1");
			PlayerPrefs.SetFloat("HighScore", score);
		}else{
			if(score > highScore){
				scoreDisplay.text = "SCORE\n" + score.ToString("F1") + " " + "<color=green>+" + (score - highScore).ToString("F1") + "</color>";
				PlayerPrefs.SetFloat("HighScore", score);
				highScore = PlayerPrefs.GetFloat("HighScore");
			}else{
				scoreDisplay.text = "SCORE\n" + score.ToString("F1") + " " + "<color=red>" + (score - highScore).ToString("F1") + "</color>";
				highScore = PlayerPrefs.GetFloat("HighScore");
			}
		}

		StartCoroutine(FadeInGameOver(1.0f));

	}
	

	IEnumerator FadeInGameOver(float fadeTime){
		yield return new WaitForSeconds(fadeTime);

		while(gameOverGroup.alpha < 1){
			gameOverGroup.alpha += 2.0f * Time.deltaTime;

			yield return null;
		}

		GameObject[] blocks;
		GameObject[] portals;

		blocks = GameObject.FindGameObjectsWithTag("Block");
		portals = GameObject.FindGameObjectsWithTag("Portal");

		foreach(GameObject b in blocks){
			Destroy(b);
		}

		foreach(GameObject p in portals){
			Destroy(p);
		}

		Camera.main.transform.position = new Vector3(0,0,0);
		_pc.playerSphere.SetActive(true);
		_pc.playerCollided = false;
		StartCoroutine(FadeInGame(1.5f));

		yield return null;
	}
		

	IEnumerator FadeInGame(float pauseDuration){
		yield return new WaitForSeconds(pauseDuration);

		while(gameOverGroup.alpha > 0){
			gameOverGroup.alpha -= 2.0f * Time.deltaTime;

			yield return null;
		}

		_pc.SetupPlayer();

		yield return null;
	}

}
