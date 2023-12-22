using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRules : MonoBehaviour
{
	public bool SelfDamage {get{return _selfDamage;}}
	public bool AllyDamage {get{return _allyDamage;}}
	public bool FriendlyDamage {get{return _friendlyDamage;}}
	public bool NeutralDamage {get{return _neutralDamage;}}

	[SerializeField]
	private bool _selfDamage;
	[SerializeField]
	private bool _allyDamage;
	[SerializeField]
	private bool _friendlyDamage;
	[SerializeField]
	private bool _neutralDamage;



	public bool CanDamageBeDone (Structure damageDoer, Structure damageReciever)
	{
		if (_selfDamage && damageDoer == damageReciever)
			return true;
		if (_allyDamage && damageDoer.Data.Team.CheckAttitude (damageReciever.Data.Team) == Relationship.Ally)
			return true;
		if (_friendlyDamage && damageDoer.Data.Team.CheckAttitude (damageReciever.Data.Team) == Relationship.Friend)
			return true;
		if (_neutralDamage && damageDoer.Data.Team.CheckAttitude (damageReciever.Data.Team) == Relationship.Neutral)
			return true;
		if (damageDoer.Data.Team.CheckAttitude (damageReciever.Data.Team) == Relationship.Enemy)
			return true;

		return false;
	}
}