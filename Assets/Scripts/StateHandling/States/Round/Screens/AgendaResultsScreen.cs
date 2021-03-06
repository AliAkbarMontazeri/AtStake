﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AgendaResultsScreen : GameScreen {

	LabelElement description;
	string defaultDescription = "Please wait while everyone finishes voting";
	List<AgendaItem> winningItems;

	public AgendaResultsScreen (GameState state, string name = "Agenda Results") : base (state, name) {
		
		Events.instance.AddListener<AllReceiveMessageEvent> (OnAllReceiveMessageEvent);
		
		if (AgendaVotingStyle.All) {
			description = new LabelElement (defaultDescription, 0);
		} else {
			description = new LabelElement ("Loading...", 0);
		}
		ScreenElements.AddEnabled ("description", description);
		ScreenElements.AddEnabled ("wait", new ImageElement ("wait", 1, Color.white));
		ScreenElements.AddDisabled ("next", CreateNextButton ());
	}

	public override void OnScreenStart (bool hosting, bool isDecider) {
		base.OnScreenStart (hosting, isDecider);
		if (AgendaVotingStyle.All) {
			MessageSender.instance.SendMessageToDecider ("FinishedVoting");
		}
	}

	void OnAllReceiveMessageEvent (AllReceiveMessageEvent e) {
		
		if (e.id != "FinishReceivingWins") {
			return;
		}

		winningItems = AgendaItemsManager.instance.WinningItems;
		Update ();
	}

	void Update () {

		Player player = Player.instance;
		ScreenElements.SuspendUpdating ();
		ScreenElements.DisableAll ();
		ScreenElements.Enable ("description");
		ScreenElements.Enable ("next");
		description.Content = "Winning Agenda Items:";

		for (int i = 0; i < winningItems.Count; i ++) {
			string item = string.Format ("{0}: {1} +{2} points", winningItems[i].playerName, winningItems[i].description, winningItems[i].bonus);
			ScreenElements.Add<LabelElement> ("item" + i.ToString(), new LabelElement (item, i+1)).Content = item;
		}

		ScreenElements.EnableUpdating ();

		// Update score
		List<AgendaItem> myWinningItems = AgendaItemsManager.instance.MyWinningItems;
		foreach (AgendaItem item in myWinningItems) {
			player.MyBeanPool.OnAddBonus (item.bonus);
		}
		BeanPoolManager.instance.UpdateMyScore ();
	}

	protected override void OnButtonPressEvent (ButtonPressEvent e) {
		if (e.screen == this && e.id == "Next") { 
			RoundState round = state as RoundState;
			if (round.RoundNumber < 3) {
				ScreenElements.Get<ButtonElement> ("next").Content = "Wait";
				MessageMatcher.instance.SetMessage (name + " next", "next screen");
			} else {
				GotoScreen ("Final Scoreboard", "End");
			}
		}
	}

	public override void OnScreenEnd () {
		if (AgendaVotingStyle.All) {
			ScreenElements.SuspendUpdating ();
			ScreenElements.DisableAll ();
			ScreenElements.Enable ("description");
			ScreenElements.Enable ("wait");
			description.Content = defaultDescription;
		}
	}

	protected override void OnMessagesMatch () {
		Events.instance.Raise (new RoundEndEvent ());
		GotoScreen ("Scoreboard", "Round");
	}
}
