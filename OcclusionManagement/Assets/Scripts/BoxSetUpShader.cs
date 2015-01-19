/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/

using UnityEngine;

/// <summary>
/// This script sets up shader variables for the occlusion shaders.
/// Different parameters are used for each device orientation.
/// </summary>
public class BoxSetUpShader : MonoBehaviour
{
    #region PRIVATE_MEMBER_VARIABLES

    private VideoTextureBehaviour mVideoTextureBehaviour;

    private float mTextureRatioX = 0.0f;
    private float mTextureRatioY = 0.0f;

    private float mViewportSizeX = 0.0f;
    private float mViewportSizeY = 0.0f;

    private float mViewportOrigX = 0.0f;
    private float mViewportOrigY = 0.0f;

    private float mScreenWidth = 0.0f;
    private float mScreenHeight = 0.0f;

    private float mPrefixX = 0.0f;
    private float mPrefixY = 0.0f;

    private float mInversionMultiplierX = 0.0f;
    private float mInversionMultiplierY = 0.0f;

    #endregion // PRIVATE_MEMBER_VARIABLES


    
    #region UNITY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        mVideoTextureBehaviour = (VideoTextureBehaviour)FindObjectOfType(typeof(VideoTextureBehaviour));
        mVideoTextureBehaviour.TextureChanged += OnTextureChanged;
    }

    void Update()
    {
        renderer.material.SetFloat("_TextureRatioX", mTextureRatioX);
        renderer.material.SetFloat("_TextureRatioY", mTextureRatioY);
        renderer.material.SetFloat("_ViewportSizeX", mViewportSizeX);
        renderer.material.SetFloat("_ViewportSizeY", mViewportSizeY);
        renderer.material.SetFloat("_ViewportOrigX", mViewportOrigX);
        renderer.material.SetFloat("_ViewportOrigY", mViewportOrigY);
        renderer.material.SetFloat("_ScreenWidth", mScreenWidth);
        renderer.material.SetFloat("_ScreenHeight", mScreenHeight);
        renderer.material.SetFloat("_PrefixX", mPrefixX);
        renderer.material.SetFloat("_PrefixY", mPrefixY);
        renderer.material.SetFloat("_InversionMultiplierX", mInversionMultiplierX);
        renderer.material.SetFloat("_InversionMultiplierY", mInversionMultiplierY);
    }
    
    void OnDestroy()
    {
        mVideoTextureBehaviour.TextureChanged -= OnTextureChanged;
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS



    #region PRIVATE_METHODS

    private void OnTextureChanged()
    {
        SetVideoTexture(mVideoTextureBehaviour.GetTexture());
        SetTextureRatio(mVideoTextureBehaviour.GetScaleFactorX(),
                        mVideoTextureBehaviour.GetScaleFactorY());
    }

    private void SetVideoTexture(Texture nTexture)
    {
        renderer.material.mainTexture = nTexture;
    }

    private void SetTextureRatio(float ratioX, float ratioY)
    {
        mTextureRatioX = ratioX;
        mTextureRatioY = ratioY;
        SetViewportParameters();
    }


    private void SetViewportParameters()
    {
        QCARBehaviour qcarbehaviour = (QCARBehaviour)FindObjectOfType(typeof(QCARBehaviour));
        if (qcarbehaviour != null)
        {
            // update viewport size
            Rect viewport = qcarbehaviour.GetViewportRectangle();
            mViewportOrigX = viewport.xMin;
            mViewportOrigY = viewport.yMin;
            mViewportSizeX = viewport.xMax - viewport.xMin;
            mViewportSizeY = viewport.yMax - viewport.yMin;
            // update screen size
            mScreenWidth = qcarbehaviour.camera.GetScreenWidth();
            mScreenHeight = qcarbehaviour.camera.GetScreenHeight();

            bool isMirrored = qcarbehaviour.VideoBackGroundMirrored;

            // determine for which orientation the shaders should be set up:
            switch (QCARRuntimeUtilities.ScreenOrientation)
            {
                case ScreenOrientation.Portrait:
                    SetParametersForPortraitNormal(isMirrored);
                    break;
                case ScreenOrientation.PortraitUpsideDown:
                    SetParametersForPortraitUpsideDown(isMirrored);
                    break;
                case ScreenOrientation.LandscapeLeft:
                    SetParametersForLandscapeLeft(isMirrored);
                    break;
                case ScreenOrientation.LandscapeRight:
                    SetParametersForLandscapeRight(isMirrored);
                    break;
            }
        }
    }

    private void SetParametersForPortraitNormal(bool isMirrored)
    {
        Shader.DisableKeyword("PORTRAIT_OFF");
        Shader.EnableKeyword("PORTRAIT_ON");

        if (isMirrored)
        {
            mPrefixX = 0.0f;
            mPrefixY = 1.0f;

            mInversionMultiplierX = 1.0f;
            mInversionMultiplierY = -1.0f;
        }
        else
        {
            mPrefixX = 1.0f;
            mPrefixY = 1.0f;

            mInversionMultiplierX = -1.0f;
            mInversionMultiplierY = -1.0f;
        }
    }

    private void SetParametersForPortraitUpsideDown(bool isMirrored)
    {
        Shader.DisableKeyword("PORTRAIT_OFF");
        Shader.EnableKeyword("PORTRAIT_ON");

        if (isMirrored)
        {
            mPrefixX = 1.0f;
            mPrefixY = 0.0f;

            mInversionMultiplierX = -1.0f;
            mInversionMultiplierY = 1.0f;
        }
        else
        {
            mPrefixX = 0.0f;
            mPrefixY = 0.0f;

            mInversionMultiplierX = 1.0f;
            mInversionMultiplierY = 1.0f;
        }
    }

    private void SetParametersForLandscapeLeft(bool isMirrored)
    {
        Shader.DisableKeyword("PORTRAIT_ON");
        Shader.EnableKeyword("PORTRAIT_OFF");

        if (isMirrored)
        {
            mPrefixX = 1.0f;
            mPrefixY = 1.0f;

            mInversionMultiplierX = -1.0f;
            mInversionMultiplierY = -1.0f;
        }
        else
        {
            mPrefixX = 0.0f;
            mPrefixY = 1.0f;

            mInversionMultiplierX = 1.0f;
            mInversionMultiplierY = -1.0f;
        }
    }

    private void SetParametersForLandscapeRight(bool isMirrored)
    {
            Shader.DisableKeyword("PORTRAIT_ON");
            Shader.EnableKeyword("PORTRAIT_OFF");

            if (isMirrored)
            {
                mPrefixX = 0.0f;
                mPrefixY = 0.0f;

                mInversionMultiplierX = 1.0f;
                mInversionMultiplierY = 1.0f;
            }
            else
            {
                mPrefixX = 1.0f;
                mPrefixY = 0.0f;

                mInversionMultiplierX = -1.0f;
                mInversionMultiplierY = 1.0f;
            }
    }

    #endregion // PRIVATE_METHODS
}
