using UnityEngine;
using System.Collections.Generic;

public class MultiFrameTracker : MonoBehaviour, ITrackerEventHandler {

	private Dictionary<MultiFrameTrackableEventHandler, SavedTransform> anchors;

	//public Transform target;
	private Vector3 newTargetPosition;
	private Quaternion newTargetRotation;
	private bool tracked;
	
	void Start () 
	{
		anchors = new Dictionary<MultiFrameTrackableEventHandler, SavedTransform>();
		foreach(MultiFrameTrackableEventHandler marker in GetComponentsInChildren<MultiFrameTrackableEventHandler>(true))
		{
			anchors.Add(marker, new SavedTransform(marker.transform.localPosition, marker.transform.localRotation));
		}
		Object.FindObjectOfType<QCARBehaviour>().RegisterTrackerEventHandler(this, true);
		tracked = false;
		OnTrackingLost();
	}
	
	void LateUpdate () 
	{
		transform.rotation = newTargetRotation;
		transform.position = newTargetPosition;
	}
	
	public void OnInitialized()
	{
		
	}
	
	public void OnTrackablesUpdated()
	{
		Vector4 targetRotation = Vector4.zero;
		Quaternion referenceRotation = new Quaternion();
		bool hasReferenceRotation = false;
		int totalActive = 0;
		Vector3 targetPostition = Vector3.zero;
		
		foreach(MultiFrameTrackableEventHandler marker in anchors.Keys)
		{
			if(marker.Tracked)
			{
				totalActive++;
			}
		}
		if(tracked)
		{
			if(totalActive == 0)
			{
				OnTrackingLost();
				tracked = false;
			}
			else
			{
				foreach(MultiFrameTrackableEventHandler marker in anchors.Keys)
				{
					if(marker.Tracked)
					{
						if(!hasReferenceRotation)
						{
							referenceRotation = marker.transform.rotation * anchors[marker].InverseRotation;
							hasReferenceRotation = true;
						}
						Math3d.AverageQuaternion(ref targetRotation, marker.transform.rotation * anchors[marker].InverseRotation, referenceRotation, totalActive);
						targetPostition += (marker.transform.position - marker.transform.rotation * anchors[marker].Position) / totalActive;
					}
				}
				newTargetRotation = new Quaternion(targetRotation.x, targetRotation.y, targetRotation.z, targetRotation.w);
				newTargetPosition = targetPostition;
			}
		}
		else if(totalActive > 0)
		{
			OnTrackingFound();
			tracked = true;
		}
	}

	private void OnTrackingFound()
	{
		Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
		Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

		Debug.Log(rendererComponents + "");

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
	}
}
