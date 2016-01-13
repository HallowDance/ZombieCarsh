using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float playerFuel;
	public Boundary boundary; 
	public float playerSpeed;
	public float playerJumpForce;
	public Vector3 movement;
	public int playerJumpBoosts;
	public float tilt;
	public Text fuelText;
	public Text jumpBoostsText;

	private Rigidbody rigidBody;
	public static PlayerController player;

	void Start ()
	{
		player = this;
		rigidBody = GetComponent<Rigidbody>();
		fuelText.text = "Fuel " + playerFuel.ToString();
		jumpBoostsText.text = "Jump Boosts " + playerJumpBoosts.ToString();
	}

	void FixedUpdate ()
	{
		//Acceleration & movement of player. Fuel managing.
		float moveHorizontal = Input.GetAxis ("Horizontal");
		movement = new Vector3 (moveHorizontal, 0.0f, Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax));
		rigidBody.AddForce (movement * playerSpeed);



		if ((moveHorizontal != 0)&(playerFuel!=0))
		{
			playerFuel--;
			fuelText.text = "Fuel " + playerFuel.ToString();
		}
		if (playerFuel == 0)
		{
			playerSpeed = 0;
			fuelText.text = "Out of Fuel! ";
		}

		//Debug.Log("Fuel " + playerFuel);
	}

	void Update()
	{
		if(Input.GetKeyUp("up")&(playerJumpBoosts!=0))
		{

			rigidBody.AddForce(new Vector3(0.0f, 5.0f, 0.0f)*playerJumpForce);
			playerJumpBoosts--;
			jumpBoostsText.text = "Jump Boosts " + playerJumpBoosts.ToString();
			Debug.Log ("IN DA WOODS");
		}

	}
	//Killing zombuls on collision with a wait subroutine
	void OnCollisionEnter(Collision enemy)
	{
		if (enemy.gameObject.tag == "enemy")
		{
			//EnemieController.enemyController.gameObject.transform.position = (PlayerController.player.gameObject.transform.position - EnemieController.enemyController.gameObject.transform.position) + new Vector3 (3.0f, 1f, 1f);
			StartCoroutine(WaitToDie()); 	

		}
	}

	public IEnumerator WaitToDie() 
	{
		EnemieController.enemyController.dieNow = false;
		yield return new WaitForSeconds(1.5f);
		EnemieController.enemyController.dieNow = true; 
	}
}
