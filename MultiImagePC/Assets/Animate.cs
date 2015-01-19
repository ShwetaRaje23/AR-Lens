using UnityEngine;
using System.Collections;

public class Animate: MonoBehaviour, ITrackableEventHandler {
	
	private TrackableBehaviour mTrackableBehaviour;
	private bool playanimation = false;
	public bool showMan = false;
	private bool mShowGUIButton = false;
	private Rect mButtonRect = new Rect(50,50,120,60);
	private float speed = 0.05f;   // check speed. 
	public GUIText debugText;	
	public GameObject lj;
	
	void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		 lj = GameObject.Find("Lumberjack1");

	}
	
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED)
		{
			mShowGUIButton = true;
		}
		else
		{
			mShowGUIButton = false;
		}
	
	}

	void Update()
	{
		GameObject gear = GameObject.Find("Gear");
		gear.transform.Rotate (351, 180, speed*Time.deltaTime ); //test this change

		// only show the man and chopping on button press 
		// Still to test ! 
	//	lj.SetActive( false);
//		if (showMan == true) {
//			lj.SetActive(true);
//			lj.animation.enabled = true;
//			lj.animation["Lumbering"].enabled = true;
//		
//
//				} else {
//			lj.SetActive(false);
//				}
		if (playanimation == true) {
						animation.Play ("shavingBoxAnimation");
					
			//animation.Play("");		
		 }
		}

	void OnGUI() {
		if (mShowGUIButton) {
			// draw the GUI button
//			if (GUI.Button(mButtonRect, " Animate ")) {
//				playanimation = true;
//				Debug.Log(" animation in progress");
//			}

			if(GUI.Button(new Rect(50,100,200,100), "Younger Kids" )){

				showMan = !showMan;
				debugText.text = " Button pressed ! ";
				debugText.text = " State of showMan  " + showMan;

			}
		}
	}
}
