using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	[SerializeField] private AudioSource soundSource;
	[SerializeField] private AudioClip collectPickupSound;
	[SerializeField] private AudioClip victorySound;

	private Rigidbody rb;
	public float speed = 250;
	public Text countText;
	public Text winText;
	private int count;
	private bool gameOver;

	private float speedScale;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		speedScale = speed * Time.deltaTime;
		count = 0;
		SetCountText ();
		winText.text = "";
		gameOver = false;
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
		rb.AddForce (movement * speedScale);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			soundSource.PlayOneShot (collectPickupSound);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText() {
		countText.text = "Count: " + count.ToString ();
		if (count >= 12) {
			winText.text = "You Win!";
			if (!gameOver) {
				soundSource.PlayOneShot (victorySound);
				gameOver = true;
			}
		}
	}

}
