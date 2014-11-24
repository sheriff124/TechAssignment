using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;
using Sce.PlayStation.HighLevel.UI;

namespace SPACE
{
	public class GameScene 
	{
		private Sce.PlayStation.HighLevel.GameEngine2D.Scene 	gameScene;
		
		private bool		scenePaused;
		public bool			swapScene {get; set;}
		
		private Entity		player;
		
		public GameScene()
		{
			gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			scenePaused = false;
			swapScene = false;
			
			player = new Player();
			player.AddToScene (gameScene);
		}
		
		public void Update(float deltaTime)
		{
			var touches = Touch.GetData(0);

			if(!scenePaused)
			{
			}
		}
		
		public void ResetScene()
		{
		}
		
		public Sce.PlayStation.HighLevel.GameEngine2D.Scene GetScene()
		{
			return gameScene;
		}
	}
}

