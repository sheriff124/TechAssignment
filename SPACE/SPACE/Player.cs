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
	public class Player : Entity
	{
		public Player ()
		{
			TextureInfo texInfo = new TextureInfo("/Application/textures/bird.png");
			sprite = new SpriteUV(texInfo); 
		}
		
		override public void Update(float deltaTime)
		{
		}
		
	}
}

