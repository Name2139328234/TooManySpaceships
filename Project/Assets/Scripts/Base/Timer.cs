using System;
using UnityEngine;



public class Timer : MonoBehaviour 
{
	private static GameObject _timerCollector;

	public bool IsPaused {get{return _isPaused;}}
	public bool IsOver {get{return _timeLeft == 0;}} //== instead of <= is fine, because values below 0 will be set to 0 by code in Update() function

	public event EventHandler<EventArgs> OnTimeRanOut;

	public bool _isLoop;

	private float _maxTime;
	private float _timeLeft;
	private bool _isPaused;



	void Start ()
	{
		_timeLeft = _maxTime;
		_isPaused = false;
	}

	void Update ()
	{
		if (_isPaused == false)
			_timeLeft -= Time.deltaTime;

		if (_timeLeft <= 0)
		{
			_timeLeft = 0; //look at IsOver comment, before ever changing this

			if (OnTimeRanOut != null)
				OnTimeRanOut (this, new EventArgs ());

			if (_isLoop)
				_timeLeft = _maxTime;
			else
				Destroy (this);
		}
	}



	public static Timer New (float time, bool isLoop)
	{
		if (_timerCollector == null)
		{
			_timerCollector = new GameObject ("Timers");
		}

		Timer timer = _timerCollector.AddComponent<Timer> ();

		timer.StartTimer (time, isLoop);

		return timer;
	}

	public void Play ()
	{
		_isPaused = false;
	}

	public void Pause ()
	{
		_isPaused = true;
	}

	public void Stop ()
	{
		_isLoop = false;

		_timeLeft = 0;
	}

	public void StopWithoutEvent ()
	{
		Destroy (this);
	}

	private void StartTimer (float time, bool isLoop)
	{
		_maxTime = time;
		_isLoop = isLoop;
	}
}
