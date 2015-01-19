/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/

using UnityEngine;
using System;

/// <summary>
/// This script checks for OpenGL ES 2.0 support on startup.
/// The Occlusion sample does not support OpenGL ES 1.1
/// </summary>
[RequireComponent(typeof(VideoTextureBehaviour))]
[RequireComponent(typeof(GLErrorHandler))]
public class OcclusionManagementInitialization : MonoBehaviour 
{
    #region PRIVATE_MEMBER_VARIABLES
    private bool mErrorOccurred = false;
    private const string ERROR_TEXT_GL = "The Occlusion sample requires OpenGL ES 2.0 or higher";
    private const string CHECK_STRING = "OpenGL ES";
    #endregion PRIVATE_MEMBER_VARIABLES

    #region PUBLIC_METHODS
    public void Init()
    {
        // This sample requires OpenGL ES 2.0 otherwise it won't work.
        mErrorOccurred = !IsOpenGLES2();

        if (mErrorOccurred)
        {
            ShowError(ERROR_TEXT_GL);
        }
    }

    #endregion PUBLIC_METHODS

    #region PRIVATE_METHODS
    /// <summary>
    /// This method checks if we are using OpenGL ES 2.0 or later.
    /// </summary>
    private bool IsOpenGLES2()
    {
        // in play mode on a desktop machine, always return true
        if (QCARRuntimeUtilities.IsPlayMode()) return true;

        string graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;

        Debug.Log("Sample using " + graphicsDeviceVersion);

        int oglStringIdx = graphicsDeviceVersion.IndexOf(CHECK_STRING, StringComparison.Ordinal);
        if (oglStringIdx >= 0)
        {
            // it's open gl es, parse the version number
            float esVersion;
            if (float.TryParse(graphicsDeviceVersion.Substring(oglStringIdx + CHECK_STRING.Length + 1, 3), out esVersion))
            {
                return esVersion >= 2.0f;
            }
        }
        return false;
    }

    private void ShowError(string errorText)
    {
        Debug.LogError(errorText);

        // Show a dialog box with an error message.
        GLErrorHandler.SetError(errorText);

        // Turn off renderer to make sure the unsupported shader is not used.
        renderer.enabled = false;

        // disable trackable behaviours
        QCARRuntimeUtilities.ForceDisableTrackables();
    }

    #endregion PRIVATE_METHODS
}
