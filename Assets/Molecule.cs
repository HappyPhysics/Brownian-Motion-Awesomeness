using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule : MonoBehaviour {
	Rigidbody2D moleculeRB; 

	float velocitySigma = 5;
	// Use this for initialization
	void Start () {
		moleculeRB = GetComponent<Rigidbody2D>();
		transform.position = new Vector2 (Random.Range (-4f, 4f), Random.Range (-4f, 4f));
	}

	void FixedUpdate() {
		moleculeRB.velocity = randomVelocity (velocitySigma);
	}
	
	Vector2 randomVelocity(float sigma) {
		// First genereate the two uniformly distributed numbers.
		Random.InitState(System.DateTime.Now.Millisecond + 3* Random.Range(0, 50));
		float u1 = Random.Range(0.0000001f, 1.0f);
		Random.InitState(System.DateTime.Now.Millisecond +3* Random.Range(0, 50));
		float u2 = Random.Range(0.0000001f, 1.0f);
		// Transform from u1 and u2 to rescaled polar coordinates R, theta
		float R = Mathf.Sqrt(-2 * Mathf.Log(u1));
		float theta = 2 * Mathf.PI * u1 * Mathf.Rad2Deg;

		// Transform to standard normally distributed x,y coordinates of the force
		float vX = R * Mathf.Cos (theta);
		float vY = R * Mathf.Sin (theta);

		// Adjust to correct sigma and average.
		vX = sigma * vX;
		vY = sigma * vY;

		// return the 2D force
		return  (new Vector2(vX, vY));
	}
}
