using UnityEngine;
using System.Collections;

public class TestAnimate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {

			// draw the GUI button
			if (GUI.Button(new Rect(10,10, 50, 50), " Animate ")) {
				
				Debug.Log(" animation in progress");
			}
		}
}
