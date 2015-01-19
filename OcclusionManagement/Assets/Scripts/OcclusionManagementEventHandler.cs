/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/

using UnityEngine;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// This class is used to define the MultiTarget's behaviour in the 
/// Occlusion app. The difference between this class and the 
/// DefaultTrackableEventHanlder is that we are not rendering the FlakesBox 
/// parts since everything is rendered using the shaders.
/// </summary>
public class OcclusionManagementEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES
    private TrackableBehaviour mTrackableBehaviour;
    #endregion // PRIVATE_MEMBER_VARIABLES

    #region PUBLIC_METHODS
    public void InitHandler()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        OnTrackingLost();
    }

    #endregion PUBLIC_METHODS
    
    #region Implementation Of ITrackableEventHandler
    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }
   #endregion Implementation Of ITrackableEventHandler

    #region PRIVATE_METHODS
    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();

        // Enable rendering:
        foreach (Renderer component in rendererComponents) {
            if (!component.name.Contains("FlakesBox")) // we do not display the elements belonging to the FlakeBox.
            {
                component.enabled = true;
            }
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
    }
    
    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();

        // Disable rendering:
        foreach (Renderer component in rendererComponents) {
            component.enabled = false;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
    }
    #endregion PRIVATE_METHODS
}
