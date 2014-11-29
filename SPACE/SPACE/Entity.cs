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
	public class Entity
	{
		protected Vector2 		position;
		protected Vector2 		scale;
		protected float   		angle;

		protected SpriteUV 		sprite;

		public SpriteUV Sprite {
			get {
				return this.sprite;
			}
			set {
				sprite = value;
			}
		}

		protected TextureInfo 	texInfo;

		protected Boolean 		gravityIsDown;
		
		public Entity ()
		{
			position = new Vector2(0.0f,0.0f);
			scale = new Vector2(1.0f,1.0f);
			angle = 0.0f;
			
			sprite = null;
			texInfo = null;
			
			gravityIsDown = true;
		}
		
		public virtual void AddToScene (Scene _scene)
		{
			if(sprite != null)
			{
				_scene.AddChild(sprite);
			}
		}
		
		public virtual void Update(float deltaTime){}
		
	}
}

