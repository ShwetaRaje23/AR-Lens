using UnityEngine;
using System.Collections;

public class SavedTransform {

	private Vector3 position;
	private Quaternion rotation;

	public Vector3 Position
	{
		get{return position;}
	}

	public Quaternion Rotation
	{
		get{return rotation;}
	}

	public Vector3 InversePosition
	{
		get{return -position;}
	}

	public Quaternion InverseRotation
	{
		get{return Quaternion.Inverse (rotation);}
	}
	
	public SavedTransform Inverse
	{
		get{return new SavedTransform(InversePosition, InverseRotation);}
	}

	public SavedTransform(Vector3 position, Quaternion rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}

	public SavedTransform(Transform transform)
	{
		position = transform.position;
		rotation = transform.rotation;
	}
}
