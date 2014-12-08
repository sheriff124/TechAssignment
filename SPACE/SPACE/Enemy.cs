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
		private float RmoveSpeed;
		private Vector2 min;
		private Vector2 max;
		private Bounds2 box;
		int i = 0;
		public Enemy (Vector2 Pos, String FileName)
		{
			texInfo = new TextureInfo ("/Application/textures/" + FileName + ".png");
			sprite = new SpriteUV (texInfo);
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (Pos.X, Pos.Y);
			moveSpeed = 2.0f;
			RmoveSpeed = 5.0f;
			
			min = new Vector2 (sprite.Position.X,sprite.Position.Y);
			max = new Vector2 (sprite.Position.X+44.0f,sprite.Position.Y+50.0f);
			box = new Bounds2 (min, max);
		}	
		override public void Update(float _deltaTime, bool hard, bool second)
		{
			if(hard == false)
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
			else
			{
				switch(dirState)
				{
					case DirState.Right:
						sprite.Position = new Vector2(sprite.Position.X+RmoveSpeed, sprite.Position.Y);
					break;
					
					case DirState.Left:
						sprite.Position = new Vector2(sprite.Position.X-RmoveSpeed, sprite.Position.Y);
					break;
				}
				strongEnemyMovement(second);
			}
			
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
		public void strongEnemyMovement(bool second)
		{
			if(second == false)
			{
				if(i>50)
				{
					dirState = DirState.Left;
					sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y - 5);
					if(i==100)
					i=0;
				}
				else
				{ 
					dirState = DirState.Right;
					sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y + 5);
				}
			}else
			{
				if(i>50)
				{
					dirState = DirState.Right;
					sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y - 5);
					if(i==100)
					i=0;
				}
				else
				{ 
					dirState = DirState.Left;
					sprite.Position = new Vector2(sprite.Position.X, sprite.Position.Y + 5);
				}
			}
				i++;
		}
		public Vector2 Pos()
		{
			Vector2 Pos = new Vector2(sprite.Position.X, sprite.Position.Y);
			return Pos;
		}
				
		public Bounds2 Bbox()
		{
				return box;
		}
	}
}

