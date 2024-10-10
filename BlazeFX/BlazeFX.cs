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
            Easing.EaseInSine => "cubic-bezier(0.12, 0, 0.39, 0)",
            Easing.EaseOutSine => "cubic-bezier(0.61, 1, 0.88, 1",
            Easing.EaseInOutSine => "cubic-bezier(0.37, 0, 0.63, 1)",
            Easing.EaseInQuad => "cubic-bezier(0.11, 0, 0.5, 0)",
            Easing.EaseOutQuad => "cubic-bezier(0.5, 1, 0.89, 1)",
            Easing.EaseInOutQuad => "cubic-bezier(0.45, 0, 0.55, 1)",
            Easing.EaseInCubic => "cubic-bezier(0.32, 0, 0.67, 0)",
            Easing.EaseOutCubic => "cubic-bezier(0.33, 1, 0.68, 1",
            Easing.EaseInOutCubic => "cubic-bezier(0.65, 0, 0.35, 1)",
            Easing.EaseInQuart => "cubic-bezier(0.5, 0, 0.75, 0)",
            Easing.EaseOutQuart => "cubic-bezier(0.25, 1, 0.5, 1)",
            Easing.EaseInOutQuart => "cubic-bezier(0.76, 0, 0.24, 1)",
            Easing.EaseInQuint => "cubic-bezier(0.64, 0, 0.78, 0)",
            Easing.EaseOutQuint => "cubic-bezier(0.22, 1, 0.36, 1)",
            Easing.EaseInOutQuint => "cubic-bezier(0.83, 0, 0.17, 1)",
            Easing.EaseInExpo => "cubic-bezier(0.7, 0, 0.84, 0)",
            Easing.EaseOutExpo => "cubic-bezier(0.16, 1, 0.3, 1)",
            Easing.EaseInOutExpo => "cubic-bezier(0.87, 0, 0.13, 1)",
            Easing.EaseInCirc => "cubic-bezier(0.55, 0, 1, 0.45)",
            Easing.EaseOutCirc => "cubic-bezier(0, 0.55, 0.45, 1)",
            Easing.EaseInOutCirc => "cubic-bezier(0.85, 0, 0.15, 1)",
            Easing.EaseInBack => "cubic-bezier(0.36, 0, 0.66, -0.56)",
            Easing.EaseOutBack => "cubic-bezier(0.34, 1.56, 0.64, 1)",
            Easing.EaseInOutBack => "cubic-bezier(0.68, -0.6, 0.32, 1.6)",

            // Soon ->

            // Easing.StepStart => "step-start",
            // Easing.StepEnd => "step-end",
            //
            // Easing.EaseInElastic => "ease-in-elastic",
            // Easing.EaseOutElastic => "ease-out-elastic",
            // Easing.EaseInOutElastic => "ease-in-out-elastic",
            //
            // Easing.EaseInBounce => "ease-in-bounce",
            // Easing.EaseOutBounce => "ease-out-bounce",
            // Easing.EaseInOutBounce => "ease-in-out-bounce",

            _ => "ease" // default
        };
    }
}