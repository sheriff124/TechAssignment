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
	public class GameScene : Sce.PlayStation.HighLevel.GameEngine2D.Scene
	{

		
		private bool		scenePaused;
		public bool			swapScene {get; set;}
		
		private Entity		player;
		//private SpriteUV testSprite;
		
		public GameScene()
		{
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);	// Tells the director that this "node" requires to be updated
			
			//gameScene = new Sce.PlayStation.HighLevel.GameEngine2D.Scene();
			scenePaused = false;
			swapScene = false;
			
			player = new Player();
			this.AddChild(player.Sprite);
		}
		
		public override void Update(float deltaTime)
		{
			Console.WriteLine("update");
			
			
			var touches = Touch.GetData(0);

			if(!scenePaused)
			{
			}
		}
		
		public void ResetScene()
		{
		}
		
		
	}
}

