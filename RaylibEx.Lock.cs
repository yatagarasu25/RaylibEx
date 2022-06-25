namespace Raylib_cs;

public static partial class RaylibEx
{
	public static DisposableLock<Texture2D> Lock(this Texture2D texture)
		=> DisposableLock.Lock(texture, Raylib.UnloadTexture);

	public static DisposableLock<Image> LockTextureData(this Texture2D texture)
		=> Raylib.LoadImageFromTexture(texture).Lock();

	public static DisposableLock<Image> Lock(this Image image)
		=> DisposableLock.Lock(image, Raylib.UnloadImage);

	public static unsafe DisposableLock<IntPtr> LockImageColors(this Image image)
		=> DisposableLock.Lock(() => (IntPtr)Raylib.LoadImageColors(image), p => Raylib.UnloadImageColors((Color*)p));
}
