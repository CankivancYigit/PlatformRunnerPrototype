using UnityEngine;

public readonly struct PlayerReachedFinishEvent
{
	public readonly Transform _wallPaintTransform;
	public PlayerReachedFinishEvent(Transform wallPaintTransform)
	{
		_wallPaintTransform = wallPaintTransform;
	}
}
