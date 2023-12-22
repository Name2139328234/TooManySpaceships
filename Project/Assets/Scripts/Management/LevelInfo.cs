using System.Collections.Generic;
using UnityEngine;



public class LevelInfo : Singleton<LevelInfo>
{
	public Structure[] LoadedStructures {get{return _loadedStructures.ToArray ();}}

	private List<Structure> _loadedStructures;



	public void Awake ()
	{

		_loadedStructures = new List<Structure> (FindObjectsOfType<Structure> ());

		foreach (Structure structure in _loadedStructures)
		{
			structure.OnDeath += RemoveSenderFromStructureList;
		}
	}

	void RemoveSenderFromStructureList (object sender, System.EventArgs e)
	{
		
	}
}