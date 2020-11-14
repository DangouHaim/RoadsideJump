using UnityEngine;

public class SwipeController : MonoBehaviour
{
	public float SensetivityX = 0.5f;
	public float SensetivityY = 0.5f;

	private Vector2 _firstPressPosition;
	private Vector2 _secondPressPosition;
	private Vector2 _currentSwipe;
	private int _frames = 0;
	
	public Direction GetSwipeDirection()
	{
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				_firstPressPosition = new Vector2(t.position.x,t.position.y);
				_frames = Time.frameCount;
			}

			if(t.phase == TouchPhase.Ended)
			{
				if(Time.frameCount - _frames < 10)
				{
					return Direction.Forward;
				}

				//save ended touch 2d point
				_secondPressPosition = new Vector2(t.position.x,t.position.y);
							
				//create vector from the two points
				_currentSwipe = new Vector3(_secondPressPosition.x - _firstPressPosition.x, _secondPressPosition.y - _firstPressPosition.y);
				
				//normalize the 2d vector
				_currentSwipe.Normalize();
	
				//swipe upwards
				if(_currentSwipe.y > SensetivityY && _currentSwipe.y > _currentSwipe.x)
				{
					return Direction.Forward;
				}

				//swipe down
				if(_currentSwipe.y < -SensetivityY && _currentSwipe.y < _currentSwipe.x)
				{
					return Direction.Backward;
				}

				//swipe left
				if(_currentSwipe.x < -SensetivityX && _currentSwipe.x < _currentSwipe.y)
				{
					return Direction.Left;
				}

				//swipe right
				if(_currentSwipe.x > SensetivityX && _currentSwipe.x > _currentSwipe.y)
				{
					return Direction.Right;
				}

				//just tap
				return Direction.Forward;
			}
		}

		return Direction.Empty;
	}

}