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
	public class BackgroundLoader : Entity
	{
		public BackgroundLoader (String FileName)
		{
			sprite	= new SpriteUV();
			texInfo = new TextureInfo ("/Application/textures/" + FileName + ".png");
			sprite = new SpriteUV (texInfo);
			sprite.Quad.S = texInfo.TextureSizef;
		}	
		
		public void Dispose()
		{
			texInfo.Dispose();
		}
		
		public void Update()
		{			
		}
	}
}

