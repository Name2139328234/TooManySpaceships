using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : Singleton<GameRules>
{
	public DamageRules DamageRules {get{return _damageRules;}}
	public PhysicsSettings PhysicsSettings {get{return _physicsSettings;}}

	[SerializeField]
	private DamageRules _damageRules;
	[SerializeField]
	private PhysicsSettings _physicsSettings;
}