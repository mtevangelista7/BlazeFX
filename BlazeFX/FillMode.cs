namespace BlazeFX;

public enum FillMode
{
    /// <summary>
    /// No styles are applied before or after the animation. 
    /// The element returns to its original state after the animation ends.
    /// </summary>
    None,

    /// <summary>
    /// The element retains the styles from the last keyframe of the animation after it ends.
    /// </summary>
    Forwards,

    /// <summary>
    /// The element applies the styles from the first keyframe of the animation during the delay period.
    /// </summary>
    Backwards,

    /// <summary>
    /// The element applies the styles from the first keyframe during the delay and retains the last keyframe styles after the animation ends.
    /// </summary>
    Both
}