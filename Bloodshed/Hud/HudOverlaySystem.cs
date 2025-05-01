using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace Bloodshed.Hud
{
    public class HudOverlaySystem : ModSystem, IDisposable
    {
        ICoreClientAPI capi;
        StaminaBarRenderer renderer;
        private static BloodshedModSystem Core => BloodshedModSystem.Instance;

        public override bool ShouldLoad(EnumAppSide forSide)
        {
            return forSide == EnumAppSide.Client;
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            capi = api;
            renderer = new StaminaBarRenderer(api);
            api.Event.RegisterRenderer(renderer, EnumRenderStage.Ortho, $"{Core.ModId}:staminabar");
        }

        public override void Dispose()
        {
            renderer.Dispose();
        }
    }
}
