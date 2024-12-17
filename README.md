# Animated Gif Editor
- Written in C#, UI made with Windows Forms
### External libraries and programs used
- AnimatedGif by Marc Rousavy - [Github](https://github.com/mrousavy/AnimatedGif)
- Gifsicle by Eddie Kohler - [Github](https://github.com/kohler/gifsicle), [webpage](https://www.lcdf.org/gifsicle/)
## Features
### Layer system
- You can add images, animated gifs, text or shapes via a layer system similar to other image-processing programs. Simply drag an image file onto the window to start.
- Layers can be manipulated separately on each frame.
- All layers can be moved, rotated, resized and have the saturation, brightness and transparency altered.
- Text layers have more options like font, border, color etc.
### Layer manipulation + basic controls
- Moving (LMB)
- Rotating (RMB)
- Resizing with locked aspect ratio (MMB)
- Resizing with unlocked aspect ratio (Shift + MMB)
- Cropping (Ctrl + MMB) - this just resizes the frame
- Moving all layers at once (CTRL + LMB)
- Add a text layer (T)
- Add a shape layer (B)
- Go to next frame (D)
- Go to previous frame (A)
### Painting, lasso and color replacement.
- Image layers can be painted on. Only a simple line is currently implemented.
- Lasso - creates a new layer with just the selected area.
- Replace color - can be used to make the background transparent or just replace one color in the image with another one.
### Animation
- Any changes to a layer propagate to the same layer on subsequent frames to make animation easier.
- Layer parameters can be interpolated. By marking 2 frames, changing a layer on the second marked frame and pressing "lerp",
the parameters of the layers inbetween are recalculated so that the layer on the first frame "morphs" fluently into the layer on the second frame.
- "line" lerp mode will move the layer across a straight line.
- "trace" lerp mode will move the layer along the path you dragged it.
### Export
- The frames can be exported as an animated gif and / or as a folder with image files (one for each frame).
- Single frame and layer can be exported separately as well.
- Gifsicle can be used to compress the gif.
### Installation
- Go to [releases](https://github.com/nonnameavailable/BIUK9000/releases), download the latest release, extract into a folder and run BIUK9000.exe
