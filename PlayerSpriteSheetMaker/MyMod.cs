using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace PlayerSpriteSheetMaker {
	partial class PlayerSpriteSheetMakerMod : Mod {
		public ModHotKey DumpKeyClear;
		//public ModHotKey DumpKeyBlack;
		//public ModHotKey DumpKeyWhite;
		public RenderTarget2D RenderTarget;



		////////////////

		public PlayerSpriteSheetMakerMod() { }

		public override void Load() {
			if( Main.netMode != NetmodeID.Server && !Main.dedServ ) {
				GraphicsDevice device = Main.instance.GraphicsDevice;   //Main.graphics.GraphicsDevice

				this.DumpKeyClear = this.RegisterHotKey( "Create Player Sheet (clear)", "L" );
				//this.DumpKeyBlack = this.RegisterHotKey( "Create Player Sheet (black)", ":" );
				//this.DumpKeyWhite = this.RegisterHotKey( "Create Player Sheet (white)", "\"" );

				this.RenderTarget = new RenderTarget2D(
					device,
					device.PresentationParameters.BackBufferWidth,
					device.PresentationParameters.BackBufferHeight,
					false,
					device.PresentationParameters.BackBufferFormat,
					DepthFormat.Depth24
				);
			}
		}

		public override void Unload() {
			if( Main.netMode == NetmodeID.Server || Main.dedServ ) {
				this.RenderTarget.Dispose();
			}
		}
	}
}
