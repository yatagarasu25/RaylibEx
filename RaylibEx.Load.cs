namespace Raylib_cs;

public static partial class RaylibEx
{
	static Action<Texture2D> textureModifier = null;

	/*
	 * using (RaylibEx.TextureModifier(t => Raylib.SetTextureFilter(t, filterMode)))
	 * {
	 * 	return RaylibEx.LoadTexture(Path.Combine(root, path));
	 * }
	 */
	public static IDisposable TextureModifier(Action<Texture2D> modifier)
	{
		var prev = textureModifier;
		textureModifier = modifier;
		return DisposableLock.Lock(() => { textureModifier = prev; });
	}

	public static IDisposable<Texture2D> LoadTexture(string path)
		=> Raylib.LoadTexture(path)
			.Also(t => {
				textureModifier?.Invoke(t);
			}).Lock();

	public static IDisposable<Texture2D> LoadTextureFromImage(IDisposable<Image> image)
		=> Raylib.LoadTextureFromImage(image._)
			.Also(t => {
				textureModifier?.Invoke(t);
			}).Lock();



	public static IDisposable<Font> LoadFont(string path, int fontSize, int[] fontChars = null, int charsCount = 0)
		=> Raylib.LoadFontEx(path, fontSize, fontChars, charsCount)
			.Also(f => {
				textureModifier?.Invoke(f.texture);
			}).Lock();
}
