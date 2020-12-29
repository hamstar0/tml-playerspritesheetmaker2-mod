using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;


namespace PlayerSpriteSheetMaker {
	partial class PlayerSpriteSheetMakerMod : Mod {
		public void DumpPlayerToTexture( Color bg ) {
			GraphicsDevice device = Main.instance.GraphicsDevice;

			// Set the render target
			device.SetRenderTarget( this.RenderTarget );
			//device.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
			device.Clear( bg );

			// Draw player layers
			Main.spriteBatch.Begin();
			this.DrawPlayerFrames();
			Main.spriteBatch.End();

			// Reset render target
			device.SetRenderTarget( null );

			// Open file
			//DateTime date = DateTime.Now;
			string filename = "Sprite Sheet.png";
			Stream stream = File.Create( Main.SavePath + Path.DirectorySeparatorChar + filename );

			// Save as PNG
			this.RenderTarget.SaveAsPng( stream, this.RenderTarget.Width, this.RenderTarget.Height );
			stream.Dispose();
			//this.RenderTarget.Dispose();

			Main.NewText( "Player layers dumped to " + filename + " (bg:"+bg+")", Color.Lime );
		}


		////////////////

		private void GetNextSheetPositionOffset( ref int x, ref int y ) {
			Player plr = Main.LocalPlayer;

			if( y < Main.screenHeight - ((2 * plr.height) + 64) ) {
				y += plr.height + 16;
			} else {
				y = 0;
				x += plr.width + 64;
			}
		}

		////

		public void DrawPlayerFrames() {
			var main = Main.instance;
			Player plr = Main.LocalPlayer;
			Vector2 pos = (plr.position - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2))
				+ new Vector2( 64, 32 );
			int xOff = 0, yOff = 0;

			for( int i = 0; i < 20; i++ ) {
				var newPos = pos + new Vector2( xOff, yOff );
				this.GetNextSheetPositionOffset( ref xOff, ref yOff );

				var clonePlr = (Player)plr.Clone();
				clonePlr.legFrame.Y = plr.legFrame.Height * i;

				main.DrawPlayer( clonePlr, newPos, plr.fullRotation, plr.fullRotationOrigin );
			}

			yOff = 9999999;

			for( int i = 0; i < 20; i++ ) {
				var newPos = pos + new Vector2( xOff, yOff );
				var clonePlr = (Player)plr.Clone();

				clonePlr.bodyFrame.Y = plr.bodyFrame.Height * i;

				this.GetNextSheetPositionOffset( ref xOff, ref yOff );
				main.DrawPlayer( clonePlr, newPos, plr.fullRotation, plr.fullRotationOrigin, 1f );
			}

			yOff = 9999999;

			if( plr.mount.Active ) {
				for( int i = 0; i < plr.mount._data.totalFrames; i++ ) {
					var newPos = pos + new Vector2( xOff, yOff );
					var clonePlr = (Player)plr.Clone();

					clonePlr.mount._frame = i;
					
					this.GetNextSheetPositionOffset( ref xOff, ref yOff );
					main.DrawPlayer( clonePlr, newPos, plr.fullRotation, plr.fullRotationOrigin );
				}
			}
		}
	}
}
