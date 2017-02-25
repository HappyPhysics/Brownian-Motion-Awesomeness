using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public Molecule molecule;
	// Use this for initialization
	void Start () {
		for (int i = 1; i < 50; i++) {
			Instantiate (molecule);
		}
	}
	

}
