using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.Core.Audio;

using Sce.PlayStation.HighLevel.GameEngine2D;
using Sce.PlayStation.HighLevel.GameEngine2D.Base;


namespace SPACE
{
	public class GameScene : Scene
	{

		private bool		scenePaused;
		public bool			swapScene {get; set;}
		
		private Entity			player;
		private Entity			enemy;
		
		
		public GameScene()
		{
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);	// Tells the director that this "node" requires to be updated
			
			scenePaused = false;
			swapScene = false;;
			
			enemy = new Enemy();
			this.AddChild(enemy.Sprite);
			
			player = new Player();
			this.AddChild(player.Sprite);
			
		}
		
		public override void Update(float deltaTime)
		{
			InputHandler.KeyPressed(InputHandler.Key.Down);
			
			if(!scenePaused)
			{
				player.Update (deltaTime);
				enemy.Update (deltaTime);
			}
		}
		
		public void ResetScene()
		{
		}
		
		
	}
}

