using UnityEngine;
using System.Collections;

public class EnemieController : MonoBehaviour {

	private Rigidbody zombieRigidBody;
	public float zombieSpeed;
	Vector3 zombieMovement;
	public bool dieNow;
	public EnemieController enemyController;
	void Start()
	{
		zombieRigidBody = GetComponent<Rigidbody> ();
		enemyController = this;

	}
	void FixedUpdate ()
	{
		
		//Zombie movement controll

		 //new Vector3 (-3.0f, 0f, 0f);//PlayerController.player.gameObject.transform.position;
		zombieRigidBody.AddForce(zombieMovement*zombieSpeed);
		if (zombieRigidBody.velocity == Vector3.zero) 
		{
			zombieRigidBody.AddForce(new Vector3 (0.0f, 50.0f, 0.0f));
		}
	}

	void Update()
	{
		if (gameObject != null)
		{
			zombieMovement = PlayerController.player.gameObject.transform.position - enemyController.gameObject.transform.position;
			if (gameObject.transform.position.x < -15) {
				Destroy (gameObject);
			}
			if (enemyController.dieNow)
			{
				enemyController.dieNow = false;
				enemyController.gameObject.SetActive (false);
				Destroy(this);

			}
		}
	}

}
			