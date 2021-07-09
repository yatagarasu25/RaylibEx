using MathEx;
using Raylib_cs;
using System;
using System.Reactive.Subjects;

namespace Raylib_cs
{
	public static partial class RaylibEx
	{
		public interface IButtonState
		{
			bool IsUp();
			bool IsDown();

			bool ThisFrame();
			bool NotThisFrame();
		}

		public struct ButtonState : IButtonState
		{
			[Flags]
			public enum Value : byte
			{
				None,
				Down,

				ThisFrame = 2,
			}

			Value v;

			public bool IsUp() => !v.HasFlag(Value.Down);
			public bool IsDown() => v.HasFlag(Value.Down);
			public bool ThisFrame() => v.HasFlag(Value.ThisFrame);
			public bool NotThisFrame() => !v.HasFlag(Value.ThisFrame);

			public ButtonState UpdateThisFrame(ButtonState c)
			{
				return new ButtonState { v = (((byte)v & 1) != ((byte)c.v & 1)) ? c.v | Value.ThisFrame : c.v };
			}

			public static implicit operator ButtonState(Value v) => new ButtonState { v = v };
		}

		public struct MouseState
		{
			public uint frameCounter;

			public vec2 position;
			public vec2 delta;

			public ButtonState buttonLeft;
			public ButtonState buttonRight;
			public ButtonState buttonMiddle;
		}

		static MouseState lastMouseState = new MouseState { frameCounter = uint.MaxValue };

		

		public static MouseState GetMouseState()
		{
			if (lastMouseState.frameCounter == frameCounter)
				return lastMouseState;

			var mousePosition = Raylib.GetMousePosition();
			MouseState mouseState = new MouseState {
				position = mousePosition,
				delta = (lastMouseState.frameCounter != uint.MaxValue) ? mousePosition - lastMouseState.position : vec2.zero,

				buttonLeft = Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON) ? ButtonState.Value.Down : ButtonState.Value.None,
				buttonRight = Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON) ? ButtonState.Value.Down : ButtonState.Value.None,
				buttonMiddle = Raylib.IsMouseButtonDown(MouseButton.MOUSE_MIDDLE_BUTTON) ? ButtonState.Value.Down : ButtonState.Value.None
			};

			mouseState.buttonLeft = lastMouseState.buttonLeft.UpdateThisFrame(mouseState.buttonLeft);
			mouseState.buttonRight = lastMouseState.buttonRight.UpdateThisFrame(mouseState.buttonRight);
			mouseState.buttonMiddle = lastMouseState.buttonMiddle.UpdateThisFrame(mouseState.buttonMiddle);
			lastMouseState = mouseState;

			return mouseState;
		}

		public class InputStream : IObserver<MouseState>, IObservable<MouseState>
		{
			public Subject<MouseState> s_;
			public Func<MouseState, bool> sensor;
			public Action<MouseState> action;

			public bool active = true;
			public bool triggered = false;

			public InputStream(Func<MouseState, bool> sensor)
			{
				this.sensor = sensor;
				s_ = new Subject<MouseState>();
			}

			public void OnCompleted()
			{
				if (!active)
					return;

				s_.OnCompleted();
			}

			public void OnError(Exception error)
			{
				if (!active)
					return;

				s_.OnError(error);
			}

			public void OnNext(MouseState value)
			{
				triggered = active && sensor(value);
				if (!triggered)
					return;

				s_.OnNext(value);
			}

			public IDisposable Subscribe(IObserver<MouseState> observer) => s_.Subscribe(observer);
		}
	}
}
