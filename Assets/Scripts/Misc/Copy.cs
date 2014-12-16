﻿using UnityEngine;
using System.Collections;

public static class Copy {

	static public string About {
		get {
			return "@Stake is a game designed by the Engagement Lab at Emerson College";
		}
	}

	static public string Instructions {
		get {
			return "Instructions on how to play the game:\n1) purchase a new microwave and\n2) reconnect with your older sister";
		}
	}

	static public string NewDeck {
		get {
			return "Here's where people will build new desks";
		}
	}

	static public string[,] stageInstructions = new string[,] {
		{ "Brainstorm", "All players brainstorm silently for 90 seconds" },
		{ "Pitch", "Now have each player pitch their idea." },
		{ "Deliberate", "Have players discuss their pitches." }
	};

	static public string GetInstructions (string stageName) {
		for (int i = 0; i < stageInstructions.Length / 2; i ++) {
			string s1 = stageInstructions[i, 0];
			string s2 = stageInstructions[i, 1];
			if (s1 == stageName)
				return s2;
		}
		return "stage name not found :(";
	}
}
