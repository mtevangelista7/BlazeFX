using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazeFX;

public enum Animations
{
    ZoomIn,
    FadeIn,
    SlideIn
}

public enum Easing
{
    EaseIn,
    EaseOut,
    EaseInOut,
    Linear
}

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
            Easing.EaseIn => "ease-in",
            Easing.EaseOut => "ease-out",
            Easing.EaseInOut => "ease-in-out",
            Easing.Linear => "linear",
            _ => "ease-in"
        };
    }
}