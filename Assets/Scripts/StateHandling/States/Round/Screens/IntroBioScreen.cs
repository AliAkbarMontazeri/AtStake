﻿using UnityEngine;
using System.Collections;

public class IntroBioScreen : IntroductionScreen {

	public IntroBioScreen (GameState state, string name = "Bio") : base (state, name) {
		ScreenElements.AddDisabled ("description", new LabelElement (Copy.IntroBio, 0, new DefaultCenterTextStyle ()));
	}

	protected override void OnScreenStartDecider () {
		ScreenElements.SuspendUpdating ();
		ScreenElements.DisableAll ();
		ScreenElements.Enable ("description");
		ScreenElements.Enable ("next");
		ScreenElements.EnableUpdating ();
	}

	protected override void SetBackEnabled () {}

	void CreateBio () {
		Player player = Player.instance;
		if (player.MyRole != null) {
			ScreenElements.DisableAll ();
			Role playerRole = player.MyRole;
			CreateBio (player.Name, playerRole.name, playerRole.bio);
			ScreenElements.Enable ("next");
		}
	}

	protected override void OnUpdateRoleEvent (UpdateRoleEvent e) {
		Debug.Log ("update role. decider ? " + Player.instance.IsDecider);
		if (!Player.instance.IsDecider) {
			CreateBio ();
		}
	}
}
