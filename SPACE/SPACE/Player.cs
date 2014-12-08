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
	public class Player : Entity
	{
		private enum DirState { Still, Left, Right };
		private enum ActState { Ground, Jump, Fall };
		
		private DirState dirState;
		private ActState actState;
		
		private float walkSpeed;
		
		
		public Player ()
		{
			texInfo = new TextureInfo ("/Application/textures/spacebro2.png");
			sprite = new SpriteUV (texInfo);
			
			sprite.Quad.S = texInfo.TextureSizef;
			sprite.Position = new Vector2 (0, 0);
			
			dirState = DirState.Still;
			actState = ActState.Ground;
			walkSpeed = 2.0f;
		}
		
		override public void Update(float _deltaTime, bool hard, bool second)
		{
			GetInput ();
			
			switch(dirState)
			{
				case DirState.Right:
					sprite.Position = new Vector2(sprite.Position.X+walkSpeed, sprite.Position.Y);
				break;
				
				case DirState.Left:
					sprite.Position = new Vector2(sprite.Position.X-walkSpeed, sprite.Position.Y);
				break;
			}
			
			switch(actState)
			{
				case ActState.Jump:
				break;
				
				case ActState.Fall:
				break;
			}
		}
		
		private void GetInput()
		{
			dirState = DirState.Still;
			
			if(InputHandler.KeyPressed (InputHandler.Key.Right))
			{
				dirState = DirState.Right;
			}
			
			if(InputHandler.KeyPressed (InputHandler.Key.Left))
			{
				dirState = DirState.Left;
			}
		}
		
	}
}

