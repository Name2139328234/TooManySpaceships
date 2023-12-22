using UnityEngine;



public class ConstructionSettings : MonoBehaviour
{
	public KeyCode FinishKey {get{return _finishKey;}}
	public KeyCode DeleteKey {get{return _deleteKey;}}
	public KeyCode RightKey {get{return _rightKey;}}
	public KeyCode LeftKey {get{return _leftKey;}}
	public KeyCode UpKey {get{return _upKey;}}
	public KeyCode DownKey {get{return _downKey;}}
	public KeyCode ForwardKey {get{return _forwardKey;}}
	public KeyCode BackwardsKey {get{return _backwardsKey;}}

	[SerializeField]
	private KeyCode _finishKey;
	[SerializeField]
	private KeyCode _deleteKey;
	[SerializeField]
	private KeyCode _rightKey;
	[SerializeField]
	private KeyCode _leftKey;
	[SerializeField]
	private KeyCode _upKey;
	[SerializeField]
	private KeyCode _downKey;
	[SerializeField]
	private KeyCode _forwardKey;
	[SerializeField]
	private KeyCode _backwardsKey;
}
