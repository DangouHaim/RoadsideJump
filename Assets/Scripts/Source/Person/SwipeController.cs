using UnityEngine;

public class SwipeController : MonoBehaviour
{
	public float SensetivityX = 0.3f;
	public float SensetivityY = 0.3f;

	private Vector2 _firstPressPosition;
	private Vector2 _secondPressPosition;
	private Vector2 _currentSwipe;
	
	public Direction GetSwipeDirection()
	{
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				_firstPressPosition = new Vector2(t.position.x,t.position.y);
			}

			if(t.phase == TouchPhase.Ended)
			{
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
			}
		}

		return Direction.Empty;
	}

}