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
	public class Enemy : Entity
	{
		private enum DirState { Still, Left, Right };
		private DirState dirState;
		private float moveSpeed;
		int i = 0;
		public Enemy ()
		{
			texInfo = new TextureInfo ("/Application/textures/bird.png");
			sprite = new SpriteUV (texInfo);
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (50, 50);
			dirState = DirState.Still;
			moveSpeed = 5.0f;
		}	
		override public void Update(float _deltaTime)
		{
			switch(dirState)
			{
				case DirState.Right:
					sprite.Position = new Vector2(sprite.Position.X+moveSpeed, sprite.Position.Y);
				break;
				
				case DirState.Left:
					sprite.Position = new Vector2(sprite.Position.X-moveSpeed, sprite.Position.Y);
				break;
			}
			weakEnemyMovement();
		}
		public void weakEnemyMovement()
		{
			
			if(i>20)
			{
				dirState = DirState.Left;
				if(i==40)
				i=0;
			}
			else
			{ 
				dirState = DirState.Right;
			}
			i++;
		}
	}
}

