using MathEx;
using System;
using SystemEx;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		public static IDisposable BeginDrawing()
		{
			Raylib.BeginDrawing();
			return DisposableLock.Lock(() => Raylib.EndDrawing());
		}

		public static IDisposable BeginDrawing(colorb color)
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(color);
			return DisposableLock.Lock(() => Raylib.EndDrawing());
		}

		public static IDisposable BeginBlendMode(BlendMode mode)
		{
			Raylib.BeginBlendMode(mode);
			return DisposableLock.Lock(() => Raylib.EndBlendMode());
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

		public static void DrawTextureTiled(Texture2D texture, rect2 dest)
			=> DrawTextureTiled(texture, dest, colorb.WHITE);

		public static void DrawTextureTiled(Texture2D texture, rect2 dest, colorb tint)
			=> DrawTextureTiled(texture, texture.GetRectangle(), dest, tint);

		public static void DrawTextureTiled(Texture2D texture, rect2 source, rect2 dest, colorb tint)
		{
			var xy = (dest.a.xyzw(0, 1) * transformStack.currentTransform).xy();
			var wh = (dest.size.xyzw(0, 0) * transformStack.currentTransform).xy();
			//currentTransform
			//Vector2.Transform(dest.xy(), currentTransform);
			//.Tra
			//currentTransform.

			Raylib.DrawTextureTiled(texture, source, rect2.xywh(xy, wh), vec2.zero, 0, transformStack.currentTransform.scale.x, tint);
		}

		public static void DrawTexture(this Texture2D texture)
			=> DrawTexture(texture, colorb.WHITE);
		public static void DrawTexture(this Texture2D texture, colorb tint)
			=> Raylib.DrawTexture(texture, 0, 0, tint);
		public static void DrawTexture(this Texture2D texture, vec2i position, colorb tint)
			=> Raylib.DrawTexture(texture, position.x, position.y, tint);
		public static void DrawTexture(this Texture2D texture, vec2 position, vec2 scale)
			=> DrawTexture(texture, position, scale, colorb.WHITE);
		public static void DrawTexture(this Texture2D texture, vec2 position, vec2 scale, colorb tint)
			=> Raylib.DrawTexturePro(texture, texture.GetRectangle(), rect2.wh(texture.size * scale), position, 0, tint);

		public static void DrawTexture(this Texture2D texture, rect2 destRec)
			=> DrawTexture(texture, destRec, colorb.WHITE);
		public static void DrawTexture(this Texture2D texture, rect2 destRec, colorb tint)
			=> Raylib.DrawTexturePro(texture, texture.GetRectangle(), destRec, vec2.zero, 0, tint);

		public static void DrawTextureNPatch(this Texture2D texture, NPatchInfo nPatchInfo, rect2 destRec)
			=> DrawTextureNPatch(texture, nPatchInfo, destRec, colorb.WHITE);

		public static void DrawTextureNPatch(this Texture2D texture, NPatchInfo nPatchInfo, rect2 destRec, colorb tint)
		{
			Raylib.DrawTextureNPatch(texture, nPatchInfo, destRec, vec2.zero, 0, tint);
		}

		public static void DrawLine(float startPosX, float startPosY, float endPosX, float endPosY, colorb color)
			=> DrawLine((startPosX, startPosY), (endPosX, endPosY), color);

		public static void DrawLine(vec2 start, vec2 end, colorb color)
		{
			Raylib.DrawLineV(start, end, color);
		}

		public static void DrawLine(vec2 start, vec2 end, float thick, colorb color)
		{
			Raylib.DrawLineEx(start, end, thick, color);
		}

		public static void DrawRectangle(rect2 rect, colorb color)
			=> Raylib.DrawRectangleRec(rect.normalized, color);

		public static void DrawRectangleLines(rect2 rect, colorb color)
			=> Raylib.DrawRectangleLinesEx(rect.normalized, 1, color);

		public static void DrawText(Font font, string text)
			=> DrawText(font, text, vec2.zero);

		public static void DrawText(Font font, string text, vec2 position)
		{
			Raylib.DrawTextEx(font, text, position, font.baseSize, 0, colorb.WHITE);
		}

		public static void DrawText(Font font, string text, rect2 destRec)
		{
			Raylib.DrawTextRec(font, text, destRec, font.baseSize, 0, true, colorb.WHITE);
		}

		public static void DrawText(Font font, string text, rect2 destRec, colorb color)
		{
			Raylib.DrawTextRec(font, text, destRec, font.baseSize, 0, true, color);
		}

		public static vec2 MeasureText(Font font, string text) => Raylib.MeasureTextEx(font, text, font.baseSize, 0);
	}
}
