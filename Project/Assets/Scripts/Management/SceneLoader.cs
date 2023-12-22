using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoader : Singleton<SceneLoader>
{
	[SerializeField]
	private string _menuSceneName;
	[SerializeField]
	private string _editorSceneName;
	[SerializeField]
	private string[] _levelSceneNames;



	public void LoadMenu ()
	{
		SceneManager.LoadScene (_menuSceneName);
	}

	public void LoadEditor ()
	{
		SceneManager.LoadScene (_editorSceneName);
	}

	public void LoadLevel (int index)
	{
		if (index < _levelSceneNames.Length)
			SceneManager.LoadScene (_levelSceneNames[index]);
	}
}
