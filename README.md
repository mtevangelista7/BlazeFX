# BlazeFX

[![NuGet](https://img.shields.io/nuget/v/BlazeFX.svg)](https://www.nuget.org/packages/BlazeFX/)

BlazeFX is a lightweight front-end animation library designed specifically for Blazor components. It empowers developers to effortlessly incorporate a wide array of animations into their Blazor projects without the need for manual CSS writing. By leveraging our pre-built components, you can enjoy seamless animations with minimal setup, enhancing the user experience of your web applications.

## Features

- Easy-to-use animation components
- Wide variety of pre-defined animations
- Customizable duration and easing functions
- Compatible with both Blazor WebAssembly and Blazor Server

## Installation

To add BlazeFX to your project, run the following command in your terminal:

```bash
dotnet add package BlazeFX
```

## Setup

1. Add the following line to your `_Imports.razor` file:

    ```razor
    @using BlazeFX
    ```

2. Include the required stylesheet in your HTML `<head>` section:

    - For Blazor WebAssembly, add the following to `index.html`:

    ```html
    <link rel="stylesheet" href="/_content/BlazeFX/blazefx.min.css" />
    ```

   For Blazor Server, add it to `_Layout.cshtml`, `_Host.cshtml`, or `App.razor`, depending on your project setup.

## Usage

BlazeFX simplifies the process of adding animations to your Blazor components. Here's a basic example:

```razor
<BlazeFX Animation="Animations.Grow" Easing="Easing.EaseInBounce" Duration="TimeSpan.FromSeconds(2)">
    <h1>Hello, Animated World!</h1>
</BlazeFX>
```

You can easily switch between various animations and adjust parameters like `Easing` and `Duration` to customize the effect.

### Available Animations

- Fade
- Grow
- Shrink
- Slide
- Rotate
- Flip
- Bounce
- ... and more!

### Customization Options

- `Animation`: Choose from a variety of pre-defined animations
- `Easing`: Select an easing function to control the animation's progression
- `Duration`: Set the length of the animation
- `Delay`: Add a delay before the animation starts

## Contributing

We welcome contributions to BlazeFX! If you encounter any issues, have feature requests, or want to improve the library, please feel free to:

1. Open an issue
2. Submit a pull request
3. Discuss potential changes in the issues section

## Future Plans

BlazeFX is under active development. Our roadmap includes:

- Introducing more customization options for animations
- Expanding documentation and providing more examples

## Support

If you need help or have any questions, please:

- Open an [Issue](https://github.com/mtevangelista7/BlazeFX/issues)

## Troubleshooting

#### Double Animation on First Load

If you encounter issues where animations run twice when your Blazor project/component is loaded for the first time, you may need to disable pre-rendering.