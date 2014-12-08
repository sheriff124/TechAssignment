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
		private Entity[]		enemy;
		
		
		public GameScene()
		{
			Scheduler.Instance.ScheduleUpdateForTarget(this, 1, false);	// Tells the director that this "node" requires to be updated
			
			scenePaused = false;
			swapScene = false;;
			
			enemy = new Entity[8];
			enemy[0] = new Enemy(new Vector2(100f,0f), "WeakEnemy4");
			this.AddChild(enemy[0].Sprite);
			enemy[1] = new Enemy(new Vector2(300f,0f), "WeakEnemy4");
			this.AddChild(enemy[1].Sprite);
			enemy[2] = new Enemy(new Vector2(500f,0f), "WeakEnemy4");
			this.AddChild(enemy[2].Sprite);
			enemy[3] = new Enemy(new Vector2(700f,0f), "WeakEnemy4");
			this.AddChild(enemy[3].Sprite);
			enemy[4] = new Enemy(new Vector2(400,0f), "WeakEnemy3");
			this.AddChild(enemy[4].Sprite);
			enemy[5] = new Enemy(new Vector2(400,0f), "WeakEnemy3");
			this.AddChild(enemy[5].Sprite);
			enemy[6] = new Enemy(new Vector2(600,0f), "WeakEnemy3");
			this.AddChild(enemy[6].Sprite);
			enemy[7] = new Enemy(new Vector2(600,0f), "WeakEnemy3");
			this.AddChild(enemy[7].Sprite);
			
			player = new Player();
			this.AddChild(player.Sprite);
			
		}
		
		public override void Update(float deltaTime)
		{
			InputHandler.KeyPressed(InputHandler.Key.Down);
			
			if(!scenePaused)
			{
				player.Update (deltaTime,false,false);
				enemy[0].Update (deltaTime,false,false);
				enemy[1].Update (deltaTime,false,false);
				enemy[2].Update (deltaTime,false,false);
				enemy[3].Update (deltaTime,false,false);
				enemy[4].Update (deltaTime,true,false);
				enemy[5].Update (deltaTime,true,true);
				enemy[6].Update (deltaTime,true,false);
				enemy[7].Update (deltaTime,true,true);
			}
		}
		
		public void ResetScene()
		{
		}
		
		
	}
}

