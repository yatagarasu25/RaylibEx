namespace Raylib_cs;

public static partial class RaylibEx
{
	public static IDisposable<Texture2D> Lock(this Texture2D texture)
		=> DisposableLock.Lock(texture, Raylib.UnloadTexture);

	public static IDisposable<Image> LockTextureData(this Texture2D texture)
		=> Raylib.LoadImageFromTexture(texture).Lock();

	public static IDisposable<Image> Lock(this Image image)
		=> DisposableLock.Lock(image, Raylib.UnloadImage);

	public static unsafe IDisposable<IntPtr> LockImageColors(this Image image)
		=> DisposableLock.Lock(() => (IntPtr)Raylib.LoadImageColors(image), p => Raylib.UnloadImageColors((Color*)p));

	public static IDisposable<Font> Lock(this Font font)
		=> DisposableLock.Lock(font, Raylib.UnloadFont);
}
