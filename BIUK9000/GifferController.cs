using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI;
using BIUK9000.UI.CustomControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000
{
    public class GifferController
    {
        private Giffer giffer;
        private GFL SavedLayerForApply;
        public int FrameCount { get => giffer.FrameCount; }
        public GifferController(Giffer giffer)
        {
            this.giffer = giffer;
        }
        public GifFrame GetFrame(int frameIndex)
        {
            if(frameIndex < giffer.Frames.Count && frameIndex >= 0)
            {
                return giffer.Frames[frameIndex];
            } else
            {
                return null;
            }
        }
        public GFL GetLayer(int frameIndex, int layerIndex)
        {
            GifFrame frame = GetFrame(frameIndex);
            if(frame != null)
            {
                if(layerIndex < frame.Layers.Count && layerIndex >= 0)
                {
                    return frame.Layers[layerIndex];
                }
            }
            return null;
        }
        public void MoveLayer(int frameIndex, int startLayerIndex, int targetLayerIndex)
        {
            for (int i = frameIndex; i < giffer.FrameCount; i++)
            {
                GifFrame cf = giffer.Frames[i];
                GFL gflToInsert = cf.Layers[startLayerIndex];
                cf.Layers.RemoveAt(startLayerIndex);
                cf.Layers.Insert(targetLayerIndex, gflToInsert);
            }
        }
        public void SaveLayerStateForApply(int frameIndex, int layerIndex)
        {
            SavedLayerForApply?.Dispose();
            SavedLayerForApply = GetLayer(frameIndex, layerIndex).Clone();
        }
        public void SetSavedLayerVisibility(bool visible)
        {
            if(SavedLayerForApply != null) SavedLayerForApply.Visible = visible;
        }
        public void LerpExecute(List<int> marks, int selectedLayerIndex, List<Point> mouseTrace = null)
        {
            if (marks.Count < 2)
            {
                MessageBox.Show("You must add at least 2 marks");
                return;
            }
            marks.Sort();
            for(int i = 0; i < marks.Count - 1; i++)
            {
                int startFrameIndex = marks[i];
                int endFrameIndex = marks[i + 1];
                GFL endLayer = GetLayer(endFrameIndex, selectedLayerIndex); 
                int lid = endLayer.LayerID;
                GFL startLayer = TryGetLayerById(startFrameIndex, lid);
                if (startLayer == null || endLayer == null)
                {
                    MessageBox.Show("Layers not found");
                    return;
                }
                int totalFrames = endFrameIndex - startFrameIndex;
                OVector difference = null;
                if (mouseTrace != null && mouseTrace.Count > 0)
                {
                    difference = startLayer.Position.Copy().Subtract(new OVector(mouseTrace[0]));
                }
                for (int j = startFrameIndex + 1; j < endFrameIndex; j++)
                {
                    double distance = 1 - (endFrameIndex - j) / (double)totalFrames;
                    GFL layerToLerp = TryGetLayerById(j, lid);
                    if (mouseTrace == null || mouseTrace.Count == 0)
                    {
                        //straight line lerp
                        layerToLerp?.Lerp(startLayer, endLayer, distance);
                    }
                    else
                    {
                        //mouse trace lerp
                        OVector tracePoint = new OVector(mouseTrace[(int)(distance * (mouseTrace.Count - 1))]);
                        layerToLerp?.Lerp(startLayer, endLayer, distance, tracePoint.Add(difference));
                    }
                }
            }
        }

        public void ApplyLayerParams(int currentFrameIndex, int currentLayerIndex, ApplyParamsMode apm)
        {
            if (apm == ApplyParamsMode.applyNone) return;
            int cli = currentLayerIndex;
            GFL startLayer = GetLayer(currentFrameIndex, cli);
            int lid = startLayer.LayerID;
            for (int i = currentFrameIndex + 1; i < giffer.FrameCount; i++)
            {
                GifFrame gf = giffer.Frames[i];
                if (cli >= 0 && cli < gf.Layers.Count)
                {
                    GFL layerToUpdate;
                    if (gf.Layers[cli].LayerID != lid)
                    {
                        layerToUpdate = TryGetLayerById(i, lid);
                    }
                    else
                    {
                        layerToUpdate = gf.Layers[cli];
                    }
                    if (layerToUpdate != null)
                    {
                        if (apm == ApplyParamsMode.applyChanged)
                        {
                            layerToUpdate.CopyDifferingParams(SavedLayerForApply, startLayer);
                        }
                        else
                        {
                            layerToUpdate.CopyParameters(startLayer);
                        }
                    }
                }
            }
        }
        public void AddLayerFromKey(Keys keyData)
        {
            int nextId = giffer.NextLayerID();
            GFL nextLayer = null;
            if (keyData == Keys.T)
            {
                nextLayer = new TextGFL(nextId);
            }
            else if (keyData == Keys.B)
            {
                nextLayer = new ShapeGFL(nextId);
            }
            if (nextLayer == null) return;
            foreach (GifFrame gf in giffer.Frames)
            {
                gf.AddLayer(nextLayer.Clone());
            }
        }
        public void AddLayer(GFL gfl)
        {
            foreach (GifFrame gf in giffer.Frames)
            {
                gf.AddLayer(gfl.Clone());
            }
        }
        public Point MousePositionOnLayer(int frameIndex, int layerIndex, Point mousePosition)
        {
            GFL gflt = GetLayer(frameIndex, layerIndex);
            if (gflt is BitmapGFL gfl)
            {
                OVector mousePositionOnFrame = new OVector(mousePosition);
                OVector resultWithoutRotation = mousePositionOnFrame.Copy().Subtract(gfl.Position).Div2(gfl.HRatio, gfl.VRatio);
                //OVector arm = resultWithoutRotation.Copy().Subtract(gfl.AbsoluteCenter());
                OVector arm = resultWithoutRotation.Copy().Subtract(gfl.AbsoluteCenter());
                arm.Mult2(gfl.HRatio, gfl.VRatio);
                arm.Rotate(-gfl.Rotation);
                arm.Div2(gfl.HRatio, gfl.VRatio);
                //OVector result = arm.Copy().Add(gfl.AbsoluteCenter());
                OVector result = arm.Copy().Add(gfl.AbsoluteCenter());
                return result.ToPoint();
            }
            else
            {
                OVector mousePositionOnFrame = new OVector(mousePosition);
                OVector resultWithoutRotation = mousePositionOnFrame.Copy().Subtract(gflt.Position);
                //OVector arm = resultWithoutRotation.Copy().Subtract(gfl.AbsoluteCenter());
                OVector arm = resultWithoutRotation.Copy().Subtract(gflt.AbsoluteCenter());
                arm.Rotate(-gflt.Rotation);
                //OVector result = arm.Copy().Add(gfl.AbsoluteCenter());
                OVector result = arm.Copy().Add(gflt.AbsoluteCenter());
                return result.ToPoint();
            }
        }
        public GFL TryGetLayerById(int frameIndex, int layerId)
        {
            return GetFrame(frameIndex).Layers.Find(layer => layer.LayerID == layerId);
        }
        public int TryGetLayerIndexById(int frameIndex, int layerId)
        {
            return GetFrame(frameIndex).Layers.IndexOf(TryGetLayerById(frameIndex, layerId));
        }
        public void DeleteLayerByID(int layerID)
        {
            foreach (GifFrame gf in giffer.Frames)
            {
                if(gf.Layers.Count > 1)
                {
                    GFL layerToDelete = gf.Layers.Find(layer => layer.LayerID == layerID);
                    if (layerToDelete != null)
                    {
                        gf.RemoveLayer(layerToDelete);
                    }
                }
            }
        }
        public void DeleteLayerByIndex(int frameIndex, int layerIndex)
        {
            int idToDelete = GetLayer(frameIndex, layerIndex).LayerID;
            DeleteLayerByID(idToDelete);
        }
        public void AddGifferAsLayers(Giffer newGiffer, bool spread)
        {
            int nextLayerID = giffer.NextLayerID();
            int sizeDif = Math.Max(newGiffer.Width - giffer.Width, newGiffer.Height - giffer.Height);
            if (newGiffer.FrameCount == 1)
            {
                using Bitmap bmp = newGiffer.FrameAsBitmap(0, false, InterpolationMode.Default);
                foreach (GifFrame frame in giffer.Frames)
                {
                    BitmapGFL gfl = new BitmapGFL(bmp, nextLayerID);
                    ResizeLayerToFit(gfl);
                    frame.AddLayer(gfl);
                }
                return;
            }
            for (int i = 0; i < giffer.FrameCount; i++)
            {
                int newGifferIndex;
                if (spread)
                {
                    newGifferIndex = (int)(i / (double)giffer.FrameCount * newGiffer.FrameCount);
                }
                else
                {
                    newGifferIndex = i % newGiffer.FrameCount;
                }
                GifFrame cgf = giffer.Frames[i];
                using Bitmap bitmap = newGiffer.FrameAsBitmap(newGifferIndex, false, InterpolationMode.Default);
                BitmapGFL gfl = new BitmapGFL(bitmap, nextLayerID);
                ResizeLayerToFit(gfl);
                cgf.AddLayer(gfl);
            }
        }
        private void ResizeLayerToFit(GFL gfl)
        {
            if (gfl.Width <= giffer.Width && gfl.Height <= giffer.Height) return;
            double hRatio = gfl.Width / (double)giffer.Width;
            double vRatio = gfl.Height / (double)giffer.Height;
            if (hRatio > vRatio)
            {
                gfl.Width = (int)(gfl.Width / hRatio);
                gfl.Height = (int)(gfl.Height / hRatio);
            }
            else
            {
                gfl.Width = (int)(gfl.Width / vRatio);
                gfl.Height = (int)(gfl.Height / vRatio);
            }
        }
        public void AddGifferAsFrames(Giffer newGiffer, int insertAt)
        {
            GFL firstLayer = giffer.Frames[0].Layers[0];
            GifFrame firstFrame = giffer.Frames[0];
            if (firstLayer is BitmapGFL)
            {
                foreach (GifFrame frame in newGiffer.Frames)
                {
                    frame.Layers[0].LayerID = firstLayer.LayerID;
                    for(int i = 1; i < firstFrame.Layers.Count; i++)
                    {
                        frame.AddLayer(firstFrame.Layers[i].Clone());
                    }
                }
                giffer.Frames.InsertRange(insertAt, newGiffer.Frames);
            } else
            {
                newGiffer.Dispose();
                MessageBox.Show("First layer on the first frame must be an image layer for this to work!");
            }
        }
        public void DeleteColor(int frameIndex, int layerIndex, Point p, int tolerance)
        {
            BitmapGFL currentLayer = GetLayer(frameIndex, layerIndex) as BitmapGFL;
            int layerID = currentLayer.LayerID;
            Bitmap obm = currentLayer.OriginalBitmap;
            if (p.X < 0 || p.X > obm.Width || p.Y < 0 || p.Y > obm.Height) return;
            Color c = obm.GetPixel(p.X, p.Y);
            foreach (GifFrame frame in giffer.Frames)
            {
                BitmapGFL gfl = frame.Layers.Find(layer => layer.LayerID == layerID) as BitmapGFL;
                using Bitmap deletedBitmap = Painter.DeleteColor(gfl.OriginalBitmap, c, tolerance);
                gfl.ReplaceOriginalBitmap(deletedBitmap);
            }
        }
        public void ReplaceColor(int frameIndex, int layerIndex, Point p, Color replacementColor, int tolerance)
        {
            BitmapGFL currentLayer = GetLayer(frameIndex, layerIndex) as BitmapGFL;
            int layerID = currentLayer.LayerID;
            Bitmap obm = currentLayer.OriginalBitmap;
            if (p.X < 0 || p.X > obm.Width || p.Y < 0 || p.Y > obm.Height) return;
            Color c = obm.GetPixel(p.X, p.Y);
            foreach (GifFrame frame in giffer.Frames)
            {
                BitmapGFL gfl = frame.Layers.Find(layer => layer.LayerID == layerID) as BitmapGFL;
                using Bitmap replacedBitmap = Painter.ReplaceColor(gfl.OriginalBitmap, c, replacementColor, tolerance);
                gfl.ReplaceOriginalBitmap(replacedBitmap);
            }
        }
        public void FloodFill(int frameIndex, int layerIndex, Point p, Color fillColor, int tolerance)
        {
            BitmapGFL currentLayer = GetLayer(frameIndex, layerIndex) as BitmapGFL;
            int layerID = currentLayer.LayerID;
            Bitmap obm = currentLayer.OriginalBitmap;
            if (p.X < 0 || p.X > obm.Width || p.Y < 0 || p.Y > obm.Height) return;
            Color c = obm.GetPixel(p.X, p.Y);
            currentLayer.ReplaceOriginalBitmap(Painter.FloodFill(obm, p, fillColor, tolerance));
        }
        public void Mirror()
        {
            foreach (GifFrame frame in giffer.Frames)
            {
                foreach (GFL gfl in frame.Layers)
                {
                    if (gfl is BitmapGFL)
                    {
                        BitmapGFL bgfl = gfl as BitmapGFL;
                        Bitmap obmp = bgfl.OriginalBitmap;
                        using Bitmap flipped = new Bitmap(obmp.Width, obmp.Height);
                        using Graphics g = Graphics.FromImage(flipped);
                        g.DrawImage(obmp, new Rectangle(0, 0, obmp.Width, obmp.Height),
                            new Rectangle(obmp.Width, 0, -obmp.Width, obmp.Height),
                            GraphicsUnit.Pixel);
                        bgfl.ReplaceOriginalBitmap(flipped);
                    }
                }
            }
        }
        public void DupeFrame(int frameIndex, int dupeCount)
        {
            GifFrame originalFrame = giffer.Frames[frameIndex];
            for(int i = 0; i < dupeCount; i++)
            {
                giffer.Frames.Insert(frameIndex, originalFrame.Clone());
            }
        }
        public void Lasso(int frameIndex, int layerIndex, Point[] lassoPoints, bool includeComplement, bool constrain, bool animateCutout, bool animateComplement)
        {
            if (lassoPoints.Length < 3) return;
            BitmapGFL bgfl = (BitmapGFL)GetLayer(frameIndex, layerIndex);

            if(!ClampPoints(lassoPoints, 0, bgfl.OriginalWidth, 0 , bgfl.OriginalHeight)) return;

            int cutoutLayerId = giffer.NextLayerID();
            if (animateCutout)
            {
                for(int i = frameIndex; i < FrameCount; i++)
                {
                    BitmapGFL currentBgfl = (BitmapGFL)TryGetLayerById(i, bgfl.LayerID);
                    if (currentBgfl == null) continue;
                    using Bitmap cutout = Painter.LassoCutout(currentBgfl.OriginalBitmap, lassoPoints, constrain);
                    BitmapGFL newBgfl = new BitmapGFL(cutout, cutoutLayerId);
                    //newBgfl.CopyParameters(bgfl);
                    CopyLassoParams(newBgfl, bgfl, constrain, lassoPoints);
                    GetFrame(i).AddLayer(newBgfl);
                }
            } else
            {
                using Bitmap cutoutBitmap = Painter.LassoCutout(bgfl.OriginalBitmap, lassoPoints, constrain);
                using BitmapGFL newBgfl = new BitmapGFL(cutoutBitmap, cutoutLayerId);
                //newBgfl.CopyParameters(bgfl);
                CopyLassoParams(newBgfl, bgfl, constrain, lassoPoints);
                AddLayer(newBgfl);
            }
            if (includeComplement)
            {
                int complementLayerId = giffer.NextLayerID();
                if (animateComplement)
                {
                    for (int i = frameIndex; i < FrameCount; i++)
                    {
                        BitmapGFL currentBgfl = (BitmapGFL)TryGetLayerById(i, bgfl.LayerID);
                        if (currentBgfl == null) continue;
                        using Bitmap complement = Painter.LassoComplement(currentBgfl.OriginalBitmap, lassoPoints);
                        BitmapGFL newBgfl = new BitmapGFL(complement, complementLayerId);
                        newBgfl.CopyParameters(bgfl);
                        //CopyLassoParams(newBgfl, bgfl, constrain, lassoPoints);
                        GetFrame(i).AddLayer(newBgfl);
                    }
                } else
                {
                    using Bitmap complementBitmap = Painter.LassoComplement(bgfl.OriginalBitmap, lassoPoints);
                    using BitmapGFL newBgfl = new BitmapGFL(complementBitmap, complementLayerId);
                    newBgfl.CopyParameters(bgfl);
                    //CopyLassoParams(newBgfl, bgfl, constrain, lassoPoints);
                    AddLayer(newBgfl);
                }
            }
        }
        private void CopyLassoParams(BitmapGFL newGFL, BitmapGFL modelGFL, bool constrain, Point[] lassoPoints)
        {
            if (constrain)
            {
                newGFL.Position = modelGFL.Position.Copy();
                newGFL.Rotation = modelGFL.Rotation;
                newGFL.Width = (int)Math.Round(newGFL.Width * modelGFL.HRatio, 0);
                newGFL.Height = (int)Math.Round(newGFL.Height * modelGFL.VRatio, 0);
                using GraphicsPath gp = new GraphicsPath();
                gp.AddPolygon(lassoPoints);
                RectangleF boundingBox = gp.GetBounds();
                OVector ratioVector = new OVector(modelGFL.HRatio, modelGFL.VRatio);
                OVector posAdjust = new OVector(boundingBox.X, boundingBox.Y).Mult2(ratioVector.X, ratioVector.Y);
                newGFL.Position = modelGFL.Position.Copy().Add(posAdjust);

                OVector arm = newGFL.Center().Copy().Subtract(modelGFL.Center());
                arm.Mult2(ratioVector.X, ratioVector.Y);
                arm.Rotate(modelGFL.Rotation);
                arm.Div2(ratioVector.X, ratioVector.Y);
                newGFL.Position = modelGFL.Center().Copy().Add(arm).Subtract(newGFL.Center().Copy().Subtract(newGFL.Position));
            }
            else
            {
                newGFL.CopyParameters(modelGFL);
            }
        }

        private bool ClampPoints(Point[] points, int xMin, int xMax, int yMin, int yMax)
        {
            bool result = false;
            for(int i = 0; i < points.Length; i++)
            {
                Point cp = points[i];
                int newX = Math.Clamp(cp.X, xMin, xMax);
                int newY = Math.Clamp(cp.Y, yMin, yMax);
                Point ncp = new Point(newX, newY);
                if (cp.Equals(ncp)) result = true;
                points[i] = new Point(newX, newY);
            }
            return result;
        }
        public void SnapLayerToFrame(int frameIndex, int layerIndex, ApplyParamsMode apm)
        {
            GFL layer = GetLayer(frameIndex, layerIndex);
            if (layer == null) return;
            SaveLayerStateForApply(frameIndex, layerIndex);
            layer.Width = giffer.Width;
            layer.Height = giffer.Height;
            layer.Rotation = 0;
            layer.Position = new OVector(0, 0);
            ApplyLayerParams(frameIndex, layerIndex, apm);
        }
        public void RestoreRatio(int frameIndex, int layerIndex, ApplyParamsMode apm)
        {
            GFL layer = GetLayer(frameIndex, layerIndex);
            if (layer == null) return;
            if(layer is BitmapGFL bgfl)
            {
                SaveLayerStateForApply(frameIndex, layerIndex);
                bgfl.Width = giffer.Width;
                bgfl.Height = (int)(bgfl.Width / bgfl.OriginalWidthToHeight);
                ApplyLayerParams(frameIndex, layerIndex, apm);
            }
        }
        public void OverrideLayerCenter(int frameIndex, int layerIndex, double xMult, double yMult)
        {
            GFL cgfl = GetLayer(frameIndex, layerIndex);
            for(int i = frameIndex; i < giffer.FrameCount; i++)
            {
                GFL layer = TryGetLayerById(i, cgfl.LayerID);
                layer?.OverrideCenter(xMult, yMult);
            }
        }
        public void ReverseFrames()
        {
            List<GifFrame> newFrames = new();
            for(int i = FrameCount - 1; i >= 0; i--)
            {
                newFrames.Add(giffer.Frames[i]);
            }
            giffer.Frames = newFrames;
        }
        public void AddReversedFrames()
        {
            for (int i = FrameCount - 1; i >= 0; i--)
            {
                giffer.Frames.Add(giffer.Frames[i].Clone());
            }
        }
        public void AddGifferAsReplaceSpread(Giffer newGiffer, int frameIndex, int layerIndex, List<int> marks)
        {
            GFL gfl = GetLayer(frameIndex, layerIndex);
            if(gfl is not BitmapGFL)
            {
                MessageBox.Show("You must select an image layer for this to work");
                return;
            }
            int lid = gfl.LayerID;
            if(marks.Count % 2 != 0 && marks.Count >= 2)
            {
                MessageBox.Show("Mark count must be even and larger or equal to 2");
                return;
            }
            marks.Sort();
            for(int i = 0; i < marks.Count; i += 2)
            {
                int startFrameIndex = marks[i];
                int endFrameIndex = marks[i + 1];
                for(int j = startFrameIndex; j <= endFrameIndex; j++)
                {
                    int newGifferFrameIndex = (int)((double)(j - startFrameIndex) / (endFrameIndex - startFrameIndex) * (newGiffer.FrameCount - 1));
                    BitmapGFL bgfl = (BitmapGFL)TryGetLayerById(j, lid);
                    bgfl.ReplaceOriginalBitmap(newGiffer.Frames[newGifferFrameIndex].CompleteBitmap(bgfl.Width, bgfl.Height, false, InterpolationMode.HighQualityBicubic));
                }
            }
        }
        public void AddGifferAsReplaceRepeat(Giffer newGiffer, int frameIndex, int layerIndex, List<int> marks)
        {
            GFL gfl = GetLayer(frameIndex, layerIndex);
            if(gfl is not BitmapGFL)
            {
                MessageBox.Show("You must select an image layer for this to work");
                return;
            }
            int lid = gfl.LayerID;
            if (marks.Count == 0) marks.Add(frameIndex);
            for (int j = 0; j < marks.Count; j++)
            {
                for (int i = 0; i < newGiffer.FrameCount; i++)
                {
                    BitmapGFL bgfl = (BitmapGFL)TryGetLayerById((marks[j] + i) % FrameCount, lid);
                    bgfl.ReplaceOriginalBitmap(newGiffer.Frames[i].CompleteBitmap(bgfl.Width, bgfl.Height, false, InterpolationMode.HighQualityBicubic));
                }
            }
        }
    }
}
