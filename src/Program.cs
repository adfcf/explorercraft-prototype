using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Adfcf.ExplorerCraft
{
    internal class Program
    {
        public static void Main()
        {

            var gameWindowSettings = new GameWindowSettings
            {
                IsMultiThreaded = false,
                UpdateFrequency = 60,
                RenderFrequency = 60
            };

            var nativeWindowSettings = new NativeWindowSettings
            {
                Title = "ExplorerCraft",
                Size = new Vector2i(1600, 900),
                StartVisible = false,
            };

            using var game = new ExplorerCraft(gameWindowSettings, nativeWindowSettings);

            game.CenterWindow();
            game.IsVisible = true;

            game.Run();

        }
    }
}