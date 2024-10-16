using Xunit;
using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Bunit;

namespace BlazeFX.Tests
{
    public class BlazeFXTests : TestContext, IDisposable
    {
        public BlazeFXTests()
        {
            JSInterop.Mode = JSRuntimeMode.Loose;
            JSInterop.SetupVoid("blazeFX.applyAnimation", _ => true);
        }


        [Fact]
        public void Animation_ShouldSetCorrectClass()
        {
            var cut = RenderComponent<BlazeFX>(parameters => parameters
                .Add(p => p.Animation, Animations.FadeIn)
                .Add(p => p.ChildContent, builder => builder.AddContent(0, "Test Content")));

            var divElement = cut.Find("div");
            Assert.Contains("blazefx-animation fadein", divElement.GetAttribute("class"));
        }

        [Fact]
        public void AnimationStyle_ShouldReflectParameters()
        {
            var cut = RenderComponent<BlazeFX>(parameters => parameters
                .Add(p => p.Duration, TimeSpan.FromSeconds(2))
                .Add(p => p.Delay, TimeSpan.FromSeconds(1))
                .Add(p => p.Easing, Easing.EaseInOut)
                .Add(p => p.FillMode, FillMode.Forwards)
                .Add(p => p.ChildContent, builder => builder.AddContent(0, "Test Content")));

            var divElement = cut.Find("div");
            var style = divElement.GetAttribute("style");
            Assert.Contains("animation-duration: 2s", style);
            Assert.Contains("animation-delay: 1s", style);
            Assert.Contains("animation-timing-function: ease-in-out", style);
            Assert.Contains("animation-fill-mode: forwards", style);
        }

        [Fact]
        public void OnAfterRender_ShouldCallJsRuntime()
        {
            var cut = RenderComponent<BlazeFX>(parameters => parameters
                .Add(p => p.ChildContent, builder => builder.AddContent(0, "Test Content"))
                .Add(p => p.Animation, Animations.FadeIn));

            var invocation = Assert.Single(JSInterop.Invocations);
            Assert.Equal("blazeFX.applyAnimation", invocation.Identifier);
        }

        [Theory]
        [InlineData(Easing.EaseIn, "ease-in")]
        [InlineData(Easing.EaseInOutBack, "cubic-bezier(0.68, -0.6, 0.32, 1.6)")]
        public void Easing_ShouldTranslateToCorrectCSSValue(Easing easing, string expectedValue)
        {
            var cut = RenderComponent<BlazeFX>(parameters => parameters
                .Add(p => p.Easing, easing)
                .Add(p => p.ChildContent, builder => builder.AddContent(0, "Test Content")));

            var divElement = cut.Find("div");
            Assert.Contains($"animation-timing-function: {expectedValue}", divElement.GetAttribute("style"));
        }
    }
}