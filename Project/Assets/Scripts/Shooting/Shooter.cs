using UnityEngine;



public class Shooter : MonoBehaviour 
{
	[SerializeField]
	private Transform _shootingDirection; //shoots from this transform's "forward"
	[SerializeField]
	private Projectile _projectile;
	[SerializeField]
	private float _reloadTime;
	[SerializeField]
	private float _randomAngleChange;

	private Timer _reloadTimer;
	private bool _isReloaded = true;



	void Start ()
	{
		_reloadTimer = Timer.New (_reloadTime, true);
		_reloadTimer.OnTimeRanOut += ReloadTimerOnTimeRanOut;
	}

	void OnDestroy ()
	{
		Destroy (_reloadTimer);
	}
		


	private void ReloadTimerOnTimeRanOut (object sender, System.EventArgs e)
	{
		_isReloaded = true;

		_reloadTimer.Pause ();
	}

	public void Shoot (Structure target) 
	{
		if (_isReloaded)
		{
			Vector3 randomAngle = new Vector3 (Random.Range (_randomAngleChange * -1, _randomAngleChange), Random.Range (_randomAngleChange * -1, _randomAngleChange), Random.Range (_randomAngleChange * -1, _randomAngleChange));
			Instantiate (_projectile.gameObject, _shootingDirection.transform.position, Quaternion.Euler (_shootingDirection.eulerAngles + randomAngle)).GetComponent<Projectile> ().Initiate (this, _shootingDirection.forward + randomAngle, target);


			_isReloaded = false;
			_reloadTimer.Play ();
		}
	}
}