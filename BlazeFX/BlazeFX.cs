﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazeFX;

/// <inheritdoc />
public class BlazeFX : ComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; }

    /// <summary>
    /// The type of animation to apply.
    /// </summary>
    [Parameter]
    public Animations Animation { get; set; }

    /// <summary>
    /// The duration of the animation.
    /// </summary>
    [Parameter]
    public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(1);

    /// <summary>
    /// The delay before the animation starts.
    /// </summary>
    [Parameter]
    public TimeSpan Delay { get; set; } = TimeSpan.Zero;

    /// <summary>
    /// The easing function that controls the animation's acceleration.
    /// </summary>
    [Parameter]
    public Easing Easing { get; set; } = Easing.EaseIn;

    /// <summary>
    /// If true, the animation will only render when complete.
    /// IMPORTANT! If RenderCompleteOnly is set to true but pre-rendering is not enabled (e.g., in a static server environment), the animation and the component involved may not appear, remaining visibly hidden.
    /// </summary>
    [Parameter]
    public bool RenderCompleteOnly { get; set; } = false;

    /// <summary>
    /// Defines how the animation affects the element's styles before and after the animation runs
    /// </summary>
    [Parameter]
    public FillMode FillMode { get; set; } = FillMode.Both;

    [Inject] private IJSRuntime JsRuntime { get; set; }

    private ElementReference _elementReference;
    private bool _shouldAnimate = false;
    private string _currentStyle;

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        if (RenderCompleteOnly)
        {
            _currentStyle = "visibility: hidden;";
            _shouldAnimate = false;
        }
        else
        {
            SetAnimationStyle();
        }
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && RenderCompleteOnly)
        {
            SetAnimationStyle();
            StateHasChanged();
        }

        if (_shouldAnimate)
        {
            await ApplyAnimationAsync();
        }
    }

    private void SetAnimationStyle()
    {
        _currentStyle = GetAnimationStyle();
        _shouldAnimate = true;
    }

    private async Task ApplyAnimationAsync()
    {
        var classAttribute = GetClassAttribute();
        await JsRuntime.InvokeVoidAsync("blazeFX.applyAnimation", _elementReference, classAttribute, _currentStyle);
    }

    /// <inheritdoc />
    protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", GetClassAttribute());
        builder.AddAttribute(2, "style", _currentStyle);
        builder.AddElementReferenceCapture(3, elementReference => _elementReference = elementReference);
        builder.AddContent(4, ChildContent);
        builder.CloseElement();
    }

    private string GetClassAttribute()
    {
        const string baseClass = "blazefx-animation";
        var animationClass = Animation.ToString().ToLowerInvariant();
        return _shouldAnimate ? $"{baseClass} {animationClass}" : baseClass;
    }

    private string GetAnimationStyle()
    {
        return $"animation-duration: {Duration.TotalSeconds}s; " +
               $"animation-delay: {Delay.TotalSeconds}s; " +
               $"animation-timing-function: {GetEasingFunction()}; " +
               $"animation-fill-mode: {GetFillMode()}; " +
               $"visibility: visible;";
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
            _ => "ease" // default
        };
    }

    private string GetFillMode()
    {
        return FillMode.ToString().ToLowerInvariant();
    }
}