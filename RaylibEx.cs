namespace Raylib_cs;

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

	public static Vector2 GetScreenSize()
		=> Vector2.xy(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

	public static Vector2 GetScreenToWorld2D(this Camera2D camera, Vector2 position)
		=> Raylib.GetScreenToWorld2D(position, camera);

	public static Rectangle GetScreenToWorldRect(this Camera2D camera)
	{
		var a = camera.GetScreenToWorld2D(Vector2.zero);
		var b = camera.GetScreenToWorld2D(GetScreenSize());

		return Rectangle.xywh(a, b - a);
	}

	public static Vector2 GetScreenToDesktop(this Vector2 v)
		=> v + Raylib.GetWindowPosition();
}
