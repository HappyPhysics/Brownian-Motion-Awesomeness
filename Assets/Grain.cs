using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grain : MonoBehaviour {
	private Rigidbody2D grainRB; // rigid body of the grain 

	private float forceMuX = 0;   // average of the force in the x direction 
	private float forceMuY = 0;  // average of the force in the y direction
	private float forceSigma; // standard deviation of the force.

	private Vector2 startingPosition;

	// Use this for initialization
	void Start () {
		forceSigma = 300.0f * Mathf.Sqrt (Time.fixedDeltaTime);
		grainRB = GetComponent<Rigidbody2D> (); 
		startingPosition = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate () {
		grainRB.AddForce (randomForceEta());

		Vector2 grainDisplacement = (Vector2) transform.position - startingPosition;
		if (Mathf.Abs(grainDisplacement.x) > 1.65f || Mathf.Abs(grainDisplacement.y) > 1.65f) {
			transform.position = startingPosition;
		}
	}

	/*********************************************************************************************
	  return a normally distributed force with the correct average and standard deviation.
      This uses the Box-Muller transform algorith, starting from two unifrom distributions.
	**********************************************************************************************/
	Vector2 randomForceEta() {
		// First genereate the two uniformly distributed numbers.
		Random.InitState(System.DateTime.Now.Millisecond + 3* Random.Range(0, 50));
		float u1 = Random.Range(0.0000001f, 1.0f);
		Random.InitState(System.DateTime.Now.Millisecond +3* Random.Range(0, 50));
		float u2 = Random.Range(0.0000001f, 1.0f);
		// Transform from u1 and u2 to rescaled polar coordinates R, theta
		float R = Mathf.Sqrt(-2 * Mathf.Log(u1));
		float theta = 2 * Mathf.PI * u1 * Mathf.Rad2Deg;

		// Transform to standard normally distributed x,y coordinates of the force
		float Fx = R * Mathf.Cos (theta);
		float Fy = R * Mathf.Sin (theta);

		// Adjust to correct sigma and average.
		Fx = forceSigma * Fx + forceMuX;
		Fy = forceSigma * Fy + forceMuY;

		// return the 2D force
		return  (new Vector2(Fx, Fy));
	}
}
