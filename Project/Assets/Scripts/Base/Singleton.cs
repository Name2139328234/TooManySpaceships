using UnityEngine;



public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;
	private static System.Object _lock = new Object ();

	public static T Instance
	{
		get
		{
			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T> ();

					if (_instance == null)
					{
						_instance = Instantiate (new GameObject ()).AddComponent<T> ();
					}
				}

				return _instance;
			}
		}
	}





	void Start ()
	{
		if (_instance != null)
		{
			Destroy (gameObject);
			return;
		}

		_instance = FindObjectOfType<T> ();
	}
}
