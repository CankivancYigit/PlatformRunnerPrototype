using UnityEngine;

public readonly struct PlayerReachedFinishEvent
{
	public readonly Transform WallPaintTransform;
	public PlayerReachedFinishEvent(Transform wallPaintTransform)
	{
		WallPaintTransform = wallPaintTransform;
	}
}
