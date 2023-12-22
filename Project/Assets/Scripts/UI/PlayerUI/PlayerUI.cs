using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerUI : MonoBehaviour
{
	[SerializeField]
	private PlayerHuman _player;
	[SerializeField]
	private TurretGroupsUI _turretGroupsButtons;//TODO this entire code is barely working and full of holes. Make it safer
	[SerializeField]
	private SelectTargetUI _selectTargetButtons;
	[SerializeField]
	private CreateTurretGroupUI _createTurretGroups;



	void Start ()
	{
		_turretGroupsButtons.SelectPlayer (_player);
		_turretGroupsButtons.SelectStructure (_player.SelectedStructure);

		_selectTargetButtons.SelectPlayer (_player);

		_createTurretGroups.SelectPlayer (_player);

		_player.OnSelectedStructureChanged += NewStructureSelected;
	}

	private void NewStructureSelected (object sender, StructureArgs e)
	{
		_turretGroupsButtons.SelectStructure (_player.SelectedStructure);
	}
}
