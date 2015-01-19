/*==============================================================================
Copyright (c) 2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using System.IO;
using UnityEditor;
using UnityEngine;


public class SampleOrientationSetter : AssetPostprocessor
{
    // This method is called by Unity whenever assets are updated (deleted,
    // moved or added)
    public static void OnPostprocessAllAssets(string[] importedAssets,
                                              string[] deletedAssets,
                                              string[] movedAssets,
                                              string[] movedFromAssetPaths)
    {
        foreach (var importedAsset in importedAssets)
        {
            // if this script is imported, force the build settings to potrait left
            if (importedAsset.Equals("Assets/Editor/QCAR/SampleOrientationSetter.cs"))
            {
                PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            }
        }
    }
}
