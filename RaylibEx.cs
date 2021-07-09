using MathEx;
using System;
using SystemEx;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		static uint frameCounter = 0;
		static public float frameTime { get; private set; } = float.NaN;
		static public float frameDeltaTime { get; private set; } = float.NaN;

		public static IDisposable BeginFrame()
		{
			var t = Raylib.GetFrameTime();
			if (!float.IsNaN(frameTime))
				frameDeltaTime = t - frameTime;
			frameTime = t;

			return DisposableLock.Lock(() => frameCounter++);
		}


		public static IDisposable InitWindow(int width, int height, string title)
		{
			Raylib.InitWindow(width, height, title);
			return DisposableLock.Lock(() => Raylib.CloseWindow());
		}

		public static vec2 GetScreenSize()
			=> new vec2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

		public static vec2 GetScreenToWorld2D(this Camera2D camera, vec2 position)
			=> Raylib.GetScreenToWorld2D(position, camera);

		public static rect2 GetScreenToWorldRect(this Camera2D camera)
		{
			var a = camera.GetScreenToWorld2D(vec2.zero);
			var b = camera.GetScreenToWorld2D(GetScreenSize());

			return rect2.xywh(a, b - a);
		}

		public static vec2 GetScreenToDesktop(this vec2 v)
			=> v + Raylib.GetWindowPosition();
	}
}
