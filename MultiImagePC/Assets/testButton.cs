using UnityEngine;
using System.Collections;

public class testButton : MonoBehaviour {

	bool show = false; 
	public GUIText debugText;
	// Update is called once per frame
	void Update () {
	if (show) {

			debugText.text = "Button Pressed ";

				}
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (100, 100, 100, 100), "Test")) {
						Debug.Log ("Button presses fine");
				
			show = true;
		}
	}
}
