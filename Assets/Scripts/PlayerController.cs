using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private MenuManager _mm;

	private Quaternion targetRotation;
	private float smoothing = 12.5f;
	private float speed = 4.5f;

	private bool moveLeft = false;
	private bool moveRight = false;

	public float currentSpeed = 150.0f;
	public float travelSpeed = 0.0f;

	private float leftAlpha = 0.5f;
	private float rightAlpha = 0.5f;

	private float timeScore;
	private float bonusScore;

	private Text scoreDisplay;

	public GameObject playerSphere;
	public GameObject leftMoveArrowColor;
	public GameObject rightMoveArrowColor;

	private Vector3 playerStartPos = new Vector3(0.0f, 3.5f, 0.0f);
	private Vector3 playerOffPos = new Vector3(0.0f, 3.5f, -20.0f);


	public bool startGame = false;
	public bool playerCollided = false;

	void Awake(){
		Resources.LoadAll("Resources");
	}

	void Start(){
		_mm = GameObject.Find("Canvas").GetComponent<MenuManager>();

		scoreDisplay = GameObject.Find("ScoreDisplay").GetComponent<Text>();

		targetRotation = transform.rotation;

		SetupPlayer();
	}

	

	void Update(){
		if(startGame && currentSpeed < travelSpeed && !playerCollided){
			currentSpeed += 50.0f * Time.deltaTime;
			//currentSpeed = travelSpeed;
		}

		if(startGame && !playerCollided){
			scoreDisplay.text = (timeScore + bonusScore).ToString("F0");

			timeScore += 1.0f * Time.deltaTime * 10;

			if(moveLeft || moveRight){
				bonusScore += 1.0f * Time.deltaTime * 10;
			}
		}else{
			scoreDisplay.text = "";
		}

		Camera.main.transform.position += new Vector3(0,0,currentSpeed) * Time.deltaTime;

		if(Input.GetKey(KeyCode.LeftArrow)){
			targetRotation *= Quaternion.AngleAxis(speed, new Vector3(0,0,1));
		}

		if(Input.GetKey(KeyCode.RightArrow)){
			targetRotation *= Quaternion.AngleAxis(speed, new Vector3(0,0,-1));
		}

		//CLEAR HIGHSCORE
		if(Input.GetKeyDown(KeyCode.D)){
			PlayerPrefs.DeleteAll();
		}

		if(moveLeft){
			targetRotation *= Quaternion.AngleAxis(speed, new Vector3(0,0,1));
			if(leftAlpha < 1.0f){
				leftAlpha += 3.0f * Time.deltaTime;
			}
		}else{
			if(leftAlpha > 0.2f){
				leftAlpha -= 3.0f * Time.deltaTime;
			}
		}

		if(moveRight){
			targetRotation *= Quaternion.AngleAxis(speed, new Vector3(0,0,-1));
			if(rightAlpha < 1.0f){
				rightAlpha += 3.0f * Time.deltaTime;
			}
		}else{
			if(rightAlpha > 0.2f){
				rightAlpha -= 3.0f * Time.deltaTime;
			}
		}

		if(playerCollided){
			currentSpeed = currentSpeed / 2;
			if(currentSpeed > 0){
				currentSpeed -= 50.0f * Time.deltaTime;
			}
		}

		//Debug.Log("Score: " + (timeScore + bonusScore) + " (" + bonusScore + " Bonus)");

		leftMoveArrowColor.GetComponent<RawImage>().color = new Color(0,1,1,leftAlpha);
		rightMoveArrowColor.GetComponent<RawImage>().color = new Color(0,1,1,rightAlpha);
			
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothing * Time.deltaTime);
	}

	public void SetupPlayer(){
		playerSphere.SetActive(true);


		StartCoroutine(StartGame(0.5f));
	}

	public void StopGame(){
		playerCollided = true;
		startGame = false;
		currentSpeed = 0;

		GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>().portalSpawned = false;
		GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>().spawnedBlocks = 0;
		GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>().spawnRate = 0.35f;
		GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>().spawnTime = 0.35f;
		GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>().stage = 0;


		playerSphere.transform.position = Camera.main.transform.position + playerOffPos;
	
		playerSphere.SetActive(false);

		_mm.GameOver(timeScore + bonusScore);
	}

	IEnumerator StartGame(float delay){
		yield return new WaitForSeconds(delay);

		startGame = true;
		timeScore = 0;
		bonusScore = 0;

		//LTMove(playerSphere, Camera.main.transform.position + playerStartPos, 1, 3);
		//LTMoveLocal(playerSphere, Camera.main.transform.position + playerStartPos, 1, 3);
		LTMoveLocal(playerSphere, transform.parent.position + playerStartPos, 1, 3);

		rightAlpha = 0.2f;
		leftAlpha = 0.2f;

		yield return null;
	}

	public void LTMove(GameObject obj, Vector3 pos, float time, float tweenTime = 0.5f){
		LeanTween.move(obj, pos, time).setEase(LeanTweenType.easeOutExpo).setTime(tweenTime);
		//LeanTween.move(obj, pos, time).setEase(LeanTweenType.easeInCubic).setTime(tweenTime);
	}

	public void LTMoveLocal(GameObject obj, Vector3 pos, float time, float tweenTime = 0.5f){
		LeanTween.moveLocal(obj, pos, time).setEase(LeanTweenType.easeOutExpo).setTime(tweenTime);
		//LeanTween.move(obj, pos, time).setEase(LeanTweenType.easeInCubic).setTime(tweenTime);
	}

	public void MoveLeft(bool pressDown){
		if(pressDown){
			moveRight = false;
			moveLeft = true;
		}else{
			moveLeft = false;
		}
	}


	public void MoveRight(bool pressDown){
		if(pressDown){
			moveLeft = false;
			moveRight = true;
		}else{
			moveRight = false;
		}
	}

}
