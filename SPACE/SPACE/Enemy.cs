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
		public Enemy (Vector2 Pos)
		{
			texInfo = new TextureInfo ("/Application/textures/bird.png");
			sprite = new SpriteUV (texInfo);
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (Pos.X, Pos.Y);
			moveSpeed = 1.0f;
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
			//strongEnemyMovement();
		}
		public void weakEnemyMovement()
		{
			
			if(i>100)
			{
				dirState = DirState.Left;
				if(i==200)
				i=0;
			}
			else
			{ 
				dirState = DirState.Right;
			}
			i++;
		}
		public void strongEnemyMovement()
		{
			if(i>200)
			{
				dirState = DirState.Left;
				if(i==400)
				i=0;
			}
			else
			{ 
				dirState = DirState.Right;
			}
			if(i%20 == 0)
			{
				sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y + 20);
			}
			i++;
		}
		public void setPosition(float X, float Y)
		{
			
		}
	}
}

