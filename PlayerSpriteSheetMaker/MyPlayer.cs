using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;


namespace PlayerSpriteSheetMaker {
	class PlayerSpriteSheetMakerPlayer : ModPlayer {
		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = (PlayerSpriteSheetMakerMod)this.mod;

			try {
				if( mymod.DumpKeyClear.JustPressed ) {
					Main.gameMenu = true;
					mymod.DumpPlayerToTexture( Color.Transparent );
					Main.gameMenu = false;
				}
				//else if( mymod.DumpKeyBlack.JustPressed ) {
				//	mymod.DumpPlayerToTexture( Color.Black );
				//} else if( mymod.DumpKeyWhite.JustPressed ) {
				//	mymod.DumpPlayerToTexture( Color.White );
				//}
			} catch( Exception e ) {
				this.mod.Logger.Warn( "ProcessTriggers - " + e.ToString() );
				return;
			}
		}
	}
}
