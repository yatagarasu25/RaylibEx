using static System.Net.Mime.MediaTypeNames;

namespace Raylib_cs;

public static partial class RaylibEx
{
	public static IDisposable BeginDrawing()
	{
		Raylib.BeginDrawing();
		return DisposableLock.Lock(Raylib.EndDrawing);
	}

	public static IDisposable BeginDrawing(Color color)
	{
		Raylib.BeginDrawing();
		Raylib.ClearBackground(color);
		return DisposableLock.Lock(Raylib.EndDrawing);
	}

	public static IDisposable BeginBlendMode(BlendMode mode)
	{
		Raylib.BeginBlendMode(mode);
		return DisposableLock.Lock(Raylib.EndBlendMode);
	}


	public static Camera2D currentCamera2D { get; private set; }
	public static IDisposable BeginMode2D(Camera2D camera)
	{
		var prevCamera2D = camera;
		currentCamera2D = camera;
		Raylib.BeginMode2D(camera);
		return DisposableLock.Lock(() => {
			Raylib.EndMode2D();
			currentCamera2D = prevCamera2D;
		});
	}

	public static void DrawTextureTiled(Texture2D texture, Rectangle dest)
		=> DrawTextureTiled(texture, dest, Color.WHITE);

	public static void DrawTextureTiled(Texture2D texture, Rectangle dest, Color tint)
		=> DrawTextureTiled(texture, texture.GetRectangle(), dest, tint);

	public static void DrawTextureTiled(Texture2D texture, Rectangle source, Rectangle dest, Color tint)
	{
		var xy = (dest.a.xyzw(0, 1) * transformStack.currentTransform).xy();
		var wh = (dest.size.xyzw(0, 0) * transformStack.currentTransform).xy();
		//currentTransform
		//Vector2.Transform(dest.xy(), currentTransform);
		//.Tra
		//currentTransform.

		Raylib.DrawTextureTiled(texture, source, Rectangle.xywh(xy, wh), Vector2.zero, 0, transformStack.currentTransform.scale.x, tint);
	}

	public static void DrawTexture(this IDisposable<Texture2D> texture) => DrawTexture(texture._);
	public static void DrawTexture(this Texture2D texture)
		=> DrawTexture(texture, Color.WHITE);
	public static void DrawTexture(this Texture2D texture, Color tint)
		=> Raylib.DrawTexture(texture, 0, 0, tint);
	public static void DrawTexture(this Texture2D texture, Vector2i position, Color tint)
		=> Raylib.DrawTexture(texture, position.x, position.y, tint);
	public static void DrawTexture(this Texture2D texture, Vector2 position, Vector2 scale)
		=> DrawTexture(texture, position, scale, Color.WHITE);
	public static void DrawTexture(this Texture2D texture, Vector2 position, Vector2 scale, Color tint)
		=> DrawTexture(texture, Rectangle.xywh(position, texture.size * scale), tint);
	public static void DrawTexture(this Texture2D texture, Rectangle destRec)
		=> DrawTexture(texture, destRec, Color.WHITE);
	public static void DrawTexture(this Texture2D texture, Rectangle destRec, Color tint)
		=> Raylib.DrawTexturePro(texture, texture.GetRectangle(), destRec, Vector2.zero, 0, tint);

	public static void DrawTextureNPatch(this Texture2D texture, NPatchInfo nPatchInfo, Rectangle destRec)
		=> DrawTextureNPatch(texture, nPatchInfo, destRec, Color.WHITE);

	public static void DrawTextureNPatch(this Texture2D texture, NPatchInfo nPatchInfo, Rectangle destRec, Color tint)
	{
		Raylib.DrawTextureNPatch(texture, nPatchInfo, destRec, Vector2.zero, 0, tint);
	}

	public static void DrawLine(float startPosX, float startPosY, float endPosX, float endPosY, Color color)
		=> DrawLine((startPosX, startPosY), (endPosX, endPosY), color);

	public static void DrawLine(Vector2 start, Vector2 end, Color color)
		=> Raylib.DrawLineV(start, end, color);
	public static void DrawLine(Vector2 start, Vector2 end, float thick, Color color)
		=> Raylib.DrawLineEx(start, end, thick, color);

	public static void DrawRectangle(Rectangle rect, Color color)
		=> Raylib.DrawRectangleRec(rect.normalized, color);

	public static void DrawRectangleLines(Rectangle rect, Color color)
		=> Raylib.DrawRectangleLinesEx(rect.normalized, 1, color);

	public static void DrawText(Font font, string text)
		=> DrawText(font, text, Vector2.zero);

	public static void DrawText(Font font, string text, Vector2 position)
	{
		Raylib.DrawTextEx(font, text, position, font.baseSize, 0, Color.WHITE);
	}

	public static void DrawText(Font font, string text, Rectangle destRec)
	{
		//Raylib.DrawTextRec(font, text, destRec, font.baseSize, 0, true, Color.WHITE);
	}

	public static void DrawText(Font font, string text, Rectangle destRec, Color color)
	{
		//Raylib.DrawTextRec(font, text, destRec, font.baseSize, 0, true, color);
	}

	public static Vector2 MeasureText(Font font, string text) => Raylib.MeasureTextEx(font, text, font.baseSize, 0);
}
