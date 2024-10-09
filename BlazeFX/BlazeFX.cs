using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazeFX;

public class BlazeFX : ComponentBase
{
	[Parameter] public RenderFragment ChildContent { get; set; }
	[Parameter] public Animations Animation { get; set; }
	[Parameter] public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(1);
	[Parameter] public TimeSpan Delay { get; set; } = TimeSpan.Zero;
	[Parameter] public Easing Easing { get; set; } = Easing.EaseIn;

	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		builder.OpenElement(0, "div");
		builder.AddAttribute(1, "class", $"blazefx-animation {Animation.ToString().ToLowerInvariant()}");
		builder.AddAttribute(2, "style", GetAnimationStyle());
		builder.AddContent(3, ChildContent);
		builder.CloseElement();
	}

	private string GetAnimationStyle()
	{
		return $"animation-duration: {Duration.TotalSeconds}s; " +
			   $"animation-delay: {Delay.TotalSeconds}s; " +
			   $"animation-timing-function: {GetEasingFunction()};";
	}

	private string GetEasingFunction()
	{
		return Easing switch
		{
			Easing.Ease => "ease",
			Easing.EaseIn => "ease-in",
			Easing.EaseOut => "ease-out",
			Easing.EaseInOut => "ease-in-out",
			Easing.Linear => "linear",
			Easing.StepStart => "step-start",
			Easing.StepEnd => "step-end",
			Easing.EaseInSine => "ease-in-sine",
			Easing.EaseOutSine => "ease-out-sine",
			Easing.EaseInOutSine => "ease-in-out-sine",
			Easing.EaseInQuad => "ease-in-quad",
			Easing.EaseOutQuad => "ease-out-quad",
			Easing.EaseInOutQuad => "ease-in-out-quad",
			Easing.EaseInCubic => "ease-in-cubic",
			Easing.EaseOutCubic => "ease-out-cubic",
			Easing.EaseInOutCubic => "ease-in-out-cubic",
			Easing.EaseInQuart => "ease-in-quart",
			Easing.EaseOutQuart => "ease-out-quart",
			Easing.EaseInOutQuart => "ease-in-out-quart",
			Easing.EaseInQuint => "ease-in-quint",
			Easing.EaseOutQuint => "ease-out-quint",
			Easing.EaseInOutQuint => "ease-in-out-quint",
			Easing.EaseInExpo => "ease-in-expo",
			Easing.EaseOutExpo => "ease-out-expo",
			Easing.EaseInOutExpo => "ease-in-out-expo",
			Easing.EaseInCirc => "ease-in-circ",
			Easing.EaseOutCirc => "ease-out-circ",
			Easing.EaseInOutCirc => "ease-in-out-circ",
			Easing.EaseInBack => "ease-in-back",
			Easing.EaseOutBack => "ease-out-back",
			Easing.EaseInOutBack => "ease-in-out-back",
			Easing.EaseInElastic => "ease-in-elastic",
			Easing.EaseOutElastic => "ease-out-elastic",
			Easing.EaseInOutElastic => "ease-in-out-elastic",
			Easing.EaseInBounce => "ease-in-bounce",
			Easing.EaseOutBounce => "ease-out-bounce",
			Easing.EaseInOutBounce => "ease-in-out-bounce",
			_ => "ease" // default
		};
	}
}