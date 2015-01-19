/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES
 
    private TrackableBehaviour mTrackableBehaviour;
    
    #endregion // PRIVATE_MEMBER_VARIABLES

	public GUIText debugText;	
	public GameObject bluecase;
	float FadeSpeed = 2; 
	GameObject manText, gearText;

    #region UNTIY_MONOBEHAVIOUR_METHODS
    
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

		manText = GameObject.Find ("lumberjackText");
		gearText = GameObject.Find ("gearText");
		manText.SetActive (false);
		gearText.SetActive (false);	
	}

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

	// Added an update function to calculte the distance of the camera from the target.
	void Update() 
	{
				// It will have to stay at a distance of lower than 8 for the blue case to disappear
				//	bluecase.SetActive(true);

				Vector3 delta = Camera.main.transform.position - mTrackableBehaviour.transform.position;
				float distance = delta.magnitude;
		
				//debugText.text = "  " + distance;

	//	bluecase = GameObject.Find ("blueCase_fix1");
				// what it will do at a lesser distance. works!
				if (distance < 8) {
				
		//	debugText.text = " Revealing internal objects ";

			//fade instead
			//bluecase.SetActive (false);

//			Color colorStart = bluecase.renderer.material.color;
//			Color endColor = new Color(colorStart.r, colorStart.g, colorStart.b, 0.0f);
//			bluecase.renderer.material.color = Color.Lerp(bluecase.renderer.material.color,endColor, Time.deltaTime*FadeSpeed);

	//SR: Fade out for cover - Doesnt work for blend models?
//			for (float f = 5f; f >= 0; f -= 0.05f) {
//				Color c = bluecase.renderer.material.color;
//				c.a = f/5f;
//				bluecase.renderer.material.color = c;
//
//			}
			bluecase.SetActive (false);
			manText.SetActive (true);
			gearText.SetActive (true);


		} else if (distance > 8){
			bluecase.SetActive (true);
			manText.SetActive (false);
			gearText.SetActive (false);
		}

	}

    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();

		}
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS


    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }

    #endregion // PRIVATE_METHODS
}
