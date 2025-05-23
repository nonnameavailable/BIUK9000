# Animated Gif Editor
![showcasesmallerres](https://github.com/user-attachments/assets/a865d1fa-0dc8-43a2-a772-c50d5e5e89d5)
## Features
### Layer system
- You can add images, animated gifs, text or shapes via a layer system similar to other image-processing programs. Simply drag an image file onto the window to start.
- Layers can be manipulated separately on each frame.
- All layers can be moved, rotated, resized and have the saturation, brightness, transparency and hue altered.
- Text layers have more options like font, border, color etc.
### Layer manipulation + basic controls
- Move (LMB)
- Rotate (RMB)
- Resize with locked aspect ratio (MMB)
- Resize with unlocked aspect ratio (Shift + MMB)
- Crop (Ctrl + MMB) - this just resizes the frame
- Move all layers at once (CTRL + LMB)
- Change the rotation center (Ctrl + Shift + LMB)
- Add a text layer (T)
- Add a shape layer (B)
- Go to next frame (D)
- Go to previous frame (A)
### Screen recording
- you can easily record a section of the screen, edit and export it as a gif.
### Animation
- Any changes to a layer propagate to the same layer on subsequent frames to make animation easier.
- Layer parameters can be interpolated via "keyframes". By marking 2 frames, changing a layer on the second marked frame and pressing "lerp",
the parameters of the layers inbetween are recalculated so that the layer on the first frame "morphs" fluently into the layer on the second frame.
- "line" lerp mode will move the layer across a straight line.
- "trace" lerp mode will move the layer along the path you dragged it.
### Painting, lasso and color replacement.
- Image layers can be painted on. Only a simple line is currently implemented.
- Lasso - creates a new layer with just the selected area.
- Replace color - can be used to make the background transparent or just replace one color in the image with another one. This can be very
### Export
- The frames can be exported as an animated gif / folder with images (one for each frame) / mp4 video IF you have FFMPEG in your PATH.
- Single frame and layer can be exported separately as well.
### Installation
- Go to [releases](https://github.com/nonnameavailable/BIUK9000/releases), download the latest release, extract into a folder and run BIUK9000.exe


### External libraries and programs used
- Gifski by Kornel Lesiński - [Github](https://github.com/ImageOptim/gifski)
- Gifski.Net wrapper by Thomas (thoo0224) - [Github](https://github.com/thoo0224/Gifski.Net)
- AnimatedGif by Marc Rousavy - [Github](https://github.com/mrousavy/AnimatedGif)
- Gifsicle by Eddie Kohler - [Github](https://github.com/kohler/gifsicle), [webpage](https://www.lcdf.org/gifsicle/)
- SharpDX by Alexandre Mutel [Github](https://github.com/sharpdx/SharpDX)
