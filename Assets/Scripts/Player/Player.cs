﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	new string name = "";
	public string Name {
		get { return name; }
		set { name = value; }
	}

	public bool IsDecider {
		get { return deciderManager.IsDecider; }
	}

	Role role = null;
	public Role MyRole {
		get { return role; }
	}

	BeanPool beanPool = null;
	public BeanPool MyBeanPool {
		get { 
			if (beanPool == null) {
				beanPool = new BeanPool (0);
			}
			return beanPool; 
		}
	}

	string winningPlayer = "";
	public string WinningPlayer {
		get { return winningPlayer; }
		set { winningPlayer = value; }
	}

	public bool Won {
		get { return winningPlayer == name; }
	}

	static public Player instance;

	public DeciderManager deciderManager = new DeciderManager ();

	void Awake () {

		if (instance == null)
			instance = this;

		// Events.instance.AddListener<EnterNameEvent> (OnEnterNameEvent);
		Events.instance.AddListener<SetRoleEvent> (OnSetRoleEvent);
		// Events.instance.AddListener<NameTakenEvent> (OnNameTakenEvent);
	}

	public void OnRoundStart () {
		beanPool.OnRoundStart (IsDecider);
	}

	/*void OnEnterNameEvent (EnterNameEvent e) {
		name = e.name;
	}*/

	/*void OnNameTakenEvent (NameTakenEvent e) {
		name = "";
	}*/

	void OnSetRoleEvent (SetRoleEvent e) {
		role = e.role;
	}
}
