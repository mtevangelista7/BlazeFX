using Microsoft.AspNetCore.Components;

namespace BlazeFX
{
    public class AnimationBase : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        protected string ClassNames { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter] public Animations Animations { get; set; }
        [Parameter] public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(1);
        [Parameter] public TimeSpan Delay { get; set; } = TimeSpan.Zero;
        [Parameter] public Easing Easing { get; set; } = Easing.EaseIn;
    }
}