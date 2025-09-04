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
using BIUK9000.MyGraphics;
using BIUK9000.MyGraphics.Dithering;
using BIUK9000.Helpers;
using BIUK9000.GifferComponents;

namespace BIUK9000.GifferManipulation
{
    public class GifferController
    {
        private Giffer giffer;
        private GFL SavedLayerForApply;
        public int FrameCount { get => giffer.FrameCount; }
        private int _sfi, _sli;
        public int SFI {
            get => _sfi;
            set
            {
                _sfi = Math.Clamp(value, 0, FrameCount - 1);
                giffer.SFI = _sfi;
            }
        }
        public int SLI {
            get => _sli;
            set
            {
                _sli = Math.Clamp(value, 0, SelectedFrame.Layers.Count - 1);
            }
        }
        public int Width { get => giffer.Width; }
        public int Height { get => giffer.Height; }
        public GifFrame SelectedFrame { get => giffer.Frames[SFI]; }
        public GFL SelectedLayer { get => SelectedFrame.Layers[SLI]; }
        public GifferController(Giffer giffer)
        {
            this.giffer = giffer;
        }
        public GifFrame GetFrame(int frameIndex)
        {
            if (frameIndex < giffer.Frames.Count && frameIndex >= 0)
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
            if (frame != null)
            {
                if (layerIndex < frame.Layers.Count && layerIndex >= 0)
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
            if (SavedLayerForApply != null) SavedLayerForApply.Visible = visible;
        }
        public void LerpExecute(List<int> marks, int selectedLayerIndex, List<Point> mouseTrace = null)
        {
            if (marks.Count < 2)
            {
                MessageBox.Show("You must add at least 2 marks");
                return;
            }
            marks.Sort();
            for (int i = 0; i < marks.Count - 1; i++)
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
        public int NextLayerID()
        {
            return giffer.NextLayerID();
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
                if (gf.Layers.Count > 1)
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
        public void AddGifferAsLayers(Giffer newGiffer, bool spread, int spreadCount)
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
                    newGifferIndex = (int)(i * spreadCount / (double)giffer.FrameCount * newGiffer.FrameCount) % newGiffer.FrameCount;
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
                    for (int i = 1; i < firstFrame.Layers.Count; i++)
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
        public void ReplaceColor(int sfi, int sli, Point p, PaintParams pm, bool subsequent)
        {
            BitmapGFL currentLayer = GetLayer(sfi, sli) as BitmapGFL;
            int layerID = currentLayer.LayerID;
            Bitmap obm = currentLayer.OriginalBitmap;
            if (p.X < 0 || p.X > obm.Width || p.Y < 0 || p.Y > obm.Height) return;
            Color c = obm.GetPixel(p.X, p.Y);
            int endFrameIndex = subsequent ? FrameCount : sfi + 1;
            for (int i = sfi; i < endFrameIndex; i++)
            {
                BitmapGFL gfl = (BitmapGFL)TryGetLayerById(i, layerID);
                using Bitmap newBitmap = Painter.ReplaceColor(gfl.OriginalBitmap, c, pm.Color, pm.Tolerance);
                gfl.ReplaceOriginalBitmap(newBitmap);
            }
        }
        public void FloodFill(int sfi, int sli, Point p, PaintParams pm, bool subsequent)
        {
            int lid = GetLayer(sfi, sli).LayerID;
            int endFrameIndex = subsequent ? FrameCount : sfi + 1;
            Color firstColor = ((BitmapGFL)TryGetLayerById(sfi, lid)).OriginalBitmap.GetPixel(p.X, p.Y);
            for (int i = sfi; i < endFrameIndex; i++)
            {
                BitmapGFL currentLayer = (BitmapGFL)TryGetLayerById(i, lid);
                Bitmap obm = currentLayer.OriginalBitmap;
                if (p.X < 0 || p.X > obm.Width || p.Y < 0 || p.Y > obm.Height) return;
                Color c = obm.GetPixel(p.X, p.Y);
                if (c.Equals(firstColor))
                {
                    using Bitmap newBitmap = Painter.FloodFill(obm, p, pm.Color, pm.Tolerance);
                    currentLayer.ReplaceOriginalBitmap(newBitmap);
                }
            }
        }
        public void Mirror(int sfi, int sli, bool subsequent)
        {
            int layerID = GetLayer(sfi, sli).LayerID;
            int endFrameIndex = subsequent ? FrameCount : sfi + 1;
            for (int i = sfi; i < endFrameIndex; i++)
            {
                GFL layer = TryGetLayerById(i, layerID);
                if (layer is BitmapGFL bgfl)
                {
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
        public void DupeFrames(List<int> marks, int sfi, int dupeCount)
        {
            if(marks.Count < 2)
            {
                GifFrame originalFrame = giffer.Frames[sfi];
                if (marks.Count == 1)
                {
                    originalFrame = giffer.Frames[marks[0]];
                }
                for (int i = 0; i < dupeCount; i++)
                {
                    giffer.Frames.Insert(sfi, originalFrame.Clone());
                }
            } else
            {
                List<GifFrame> framesToDupe = new();
                for (int i = marks[0]; i <= marks[1]; i++)
                {
                    framesToDupe.Add(giffer.Frames[i]);
                }
                for(int i = 0; i < dupeCount; i++)
                {
                    for (int j = framesToDupe.Count - 1; j >= 0; j--)
                    {
                        GifFrame gf = framesToDupe[j];
                        giffer.Frames.Insert(sfi + 1, gf.Clone());
                    }
                }
            }
        }
        public void Lasso(int frameIndex, int layerIndex, Point[] lassoPoints, LassoParams lp)
        {
            if (lassoPoints.Length < 3) return;
            BitmapGFL bgfl = (BitmapGFL)GetLayer(frameIndex, layerIndex);

            if (!ClampPoints(lassoPoints, 0, bgfl.OriginalWidth, 0, bgfl.OriginalHeight)) return;

            int cutoutLayerId = giffer.NextLayerID();
            if (lp.AnimateCutout)
            {
                for (int i = frameIndex; i < FrameCount; i++)
                {
                    BitmapGFL currentBgfl = (BitmapGFL)TryGetLayerById(i, bgfl.LayerID);
                    if (currentBgfl == null) continue;
                    using Bitmap cutout = Painter.LassoCutout(currentBgfl.OriginalBitmap, lassoPoints, lp.ConstrainBounds);
                    BitmapGFL newBgfl = new BitmapGFL(cutout, cutoutLayerId);
                    //newBgfl.CopyParameters(bgfl);
                    CopyLassoParams(newBgfl, bgfl, lp.ConstrainBounds, lassoPoints);
                    GetFrame(i).AddLayer(newBgfl);
                }
            } else
            {
                using Bitmap cutoutBitmap = Painter.LassoCutout(bgfl.OriginalBitmap, lassoPoints, lp.ConstrainBounds);
                using BitmapGFL newBgfl = new BitmapGFL(cutoutBitmap, cutoutLayerId);
                //newBgfl.CopyParameters(bgfl);
                CopyLassoParams(newBgfl, bgfl, lp.ConstrainBounds, lassoPoints);
                AddLayer(newBgfl);
            }
            if (lp.IncludeComplement)
            {
                int complementLayerId = giffer.NextLayerID();
                if (lp.AnimateComplement)
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
            for (int i = 0; i < points.Length; i++)
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
            if (layer is BitmapGFL bgfl)
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
            for (int i = frameIndex; i < giffer.FrameCount; i++)
            {
                GFL layer = TryGetLayerById(i, cgfl.LayerID);
                layer?.OverrideCenter(xMult, yMult);
            }
        }
        public void ReverseFrames(List<int> marks)
        {
            int startIndex = 0;
            int endIndex = FrameCount - 1;
            marks.Sort();
            if(marks.Count >= 2)
            {
                startIndex = marks[0];
                endIndex = marks[1];
            }
            giffer.Frames.Reverse(startIndex, endIndex - startIndex + 1);
        }
        public void AddReversedFrames()
        {
            if (FrameCount < 3) return;
            for (int i = FrameCount - 2; i > 0; i--)
            {
                giffer.Frames.Add(giffer.Frames[i].Clone());
            }
        }
        public void AddGifferAsReplaceSpread(Giffer newGiffer, int frameIndex, int layerIndex, List<int> marks)
        {
            GFL gfl = GetLayer(frameIndex, layerIndex);
            if (gfl is not BitmapGFL)
            {
                MessageBox.Show("You must select an image layer for this to work");
                return;
            }
            int lid = gfl.LayerID;
            if (marks.Count % 2 != 0 && marks.Count >= 2)
            {
                MessageBox.Show("Mark count must be even and larger or equal to 2");
                return;
            }
            marks.Sort();
            for (int i = 0; i < marks.Count; i += 2)
            {
                int startFrameIndex = marks[i];
                int endFrameIndex = marks[i + 1];
                for (int j = startFrameIndex; j <= endFrameIndex; j++)
                {
                    int newGifferFrameIndex = (int)((double)(j - startFrameIndex) / (endFrameIndex - startFrameIndex) * (newGiffer.FrameCount - 1));
                    BitmapGFL bgfl = (BitmapGFL)TryGetLayerById(j, lid);
                    using Bitmap newBitmap = newGiffer.Frames[newGifferFrameIndex].CompleteBitmap(newGiffer.Width, newGiffer.Height, false, InterpolationMode.HighQualityBicubic);
                    bgfl.ReplaceOriginalBitmap(newBitmap);
                }
            }
        }
        public void AddGifferAsReplaceRepeat(Giffer newGiffer, int frameIndex, int layerIndex, List<int> marks)
        {
            GFL gfl = GetLayer(frameIndex, layerIndex);
            if (gfl is not BitmapGFL)
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
                    using Bitmap newBitmap = newGiffer.Frames[i].CompleteBitmap(newGiffer.Width, newGiffer.Height, false, InterpolationMode.HighQualityBicubic);
                    bgfl.ReplaceOriginalBitmap(newBitmap);
                }
            }
        }
        public void Flatten()
        {
            int nextId = giffer.NextLayerID();
            foreach (GifFrame gf in giffer.Frames)
            {
                BitmapGFL flatGfl = new BitmapGFL(gf.CompleteBitmap(giffer.Width, giffer.Height, false, InterpolationMode.HighQualityBicubic), nextId);
                gf.Layers.ForEach(layer => layer.Dispose());
                gf.Layers.Clear();
                gf.AddLayer(flatGfl);
            }
        }

        public void ShiftLayer(int frameIndex, int layerIndex, int shiftValue)
        {
            if (shiftValue == 0) return;
            if (shiftValue < 0) shiftValue += FrameCount;
            int layerID = GetLayer(frameIndex, layerIndex).LayerID;
            List<GFL> newLayerOrderList = new List<GFL>();
            List<int> originalLayerPositions = new List<int>();
            for (int i = 0; i < FrameCount; i++)
            {
                originalLayerPositions.Add(TryGetLayerIndexById(i, layerID));
            }
            for (int i = shiftValue; i < FrameCount + shiftValue; i++)
            {
                GFL layer = TryGetLayerById(i % FrameCount, layerID);
                newLayerOrderList.Add(layer);
                GetFrame(i % FrameCount).Layers.Remove(layer);
            }
            for (int i = 0; i < FrameCount; i++)
            {
                int oi = originalLayerPositions[i];
                GetFrame(i).Layers.Insert(oi, newLayerOrderList[i]);
            }
        }
        public void MakePreviousLayersInvisible(int frameIndex, int layerIndex)
        {
            int layerID = GetLayer(frameIndex, layerIndex).LayerID;
            for (int i = 0; i < frameIndex; i++)
            {
                GFL layer = TryGetLayerById(i, layerID);
                if (layer != null) layer.Visible = false;
            }
        }
        public bool DeleteFramesBetweenMarks(List<int> marks, bool ask)
        {
            if (marks.Count != 2)
            {
                MessageBox.Show("Exactly 2 frames must be marked!");
                return false;
            }
            if (ask)
            {
                if (MessageBox.Show("Do you really want to delete these frames?", "Careful!", MessageBoxButtons.YesNo) == DialogResult.No) return false;
            }
            giffer.RemoveFrames(marks);
            return true;
        }
        public bool DeleteFramesOutsideOfMarks(List<int> marks, bool ask)
        {
            if (marks.Count != 2)
            {
                MessageBox.Show("Exactly 2 frames must be marked!");
                return false;
            }
            if (ask)
            {
                if (MessageBox.Show("Do you really want to delete these frames?", "Careful!", MessageBoxButtons.YesNo) == DialogResult.No) return false;
            }
            int secondIndex = Math.Clamp(Math.Max(marks[1], marks[0]) + 1, 0, FrameCount - 1);
            int firstIndex = Math.Clamp(Math.Min(marks[0], marks[1]) - 1, 0, FrameCount - 1);
            if (secondIndex < FrameCount - 1) DeleteFramesBetweenMarks([secondIndex, FrameCount - 1], false);
            if (firstIndex > 0) DeleteFramesBetweenMarks([0, firstIndex], false);
            if(SFI >= FrameCount) SFI = FrameCount - 1;
            return true;
        }
        public void ConvertLayerToBitmap(int sfi, int sli)
        {
            GFL gfl = GetLayer(sfi, sli);
            if (gfl == null || gfl is BitmapGFL) return;
            int lid = giffer.NextLayerID();
            GFL bgfl = new BitmapGFL(gfl.MorphedBitmap(InterpolationMode.HighQualityBicubic), lid);
            bgfl.Rotation = gfl.Rotation;
            bgfl.Position = gfl.Position;
            AddLayer(bgfl);
            DeleteLayerByID(gfl.LayerID);
        }

        public void DeleteDuplicateFrames()
        {
            if (FrameCount < 3) return;
            int originalFrameCount = FrameCount;
            FastBitmap compareFbm = new(GetFrame(FrameCount - 1).CompleteBitmap(giffer.Width, giffer.Height, false, InterpolationMode.NearestNeighbor));
            for (int i = FrameCount - 2; i >= 0; i--)
            {
                FastBitmap currentFbm = new(GetFrame(i).CompleteBitmap(giffer.Width, giffer.Height, false, InterpolationMode.NearestNeighbor));
                if (compareFbm.Equals(currentFbm))
                {
                    giffer.RemoveFrames([i]);
                    currentFbm.Dispose();
                } else
                {
                    compareFbm.Dispose();
                    compareFbm = currentFbm;
                }
                if (i == 0) currentFbm?.Dispose();
            }
            compareFbm?.Dispose();
            try
            {
                MultiplyFrameTimings(1d / ((double)FrameCount / originalFrameCount));
            } catch(Exception ex)
            {
                MessageBox.Show("Frame timing adjustment failed. Error: " + Environment.NewLine + ex.Message);
            }
            if (SFI >= FrameCount) SFI = FrameCount - 1;
        }
        public void DrawLineOnSubsequentFrames(int sfi, int sli, List<Point> points, PaintParams pm)
        {
            GFL gfl = GetLayer(sfi, sli);
            if (gfl is BitmapGFL bgfl)
            {
                int lid = bgfl.LayerID;
                for (int i = sfi + 1; i < FrameCount; i++)
                {
                    using Graphics g = Graphics.FromImage(((BitmapGFL)TryGetLayerById(i, lid)).OriginalBitmap);
                    Painter.DrawLinesFromPoints(g, points, pm);
                }
            }
            else return;

        }
        public void MultiplyFrameTimings(double val)
        {
            if (val == 1 || val == 0) return;

            foreach(GifFrame gf in giffer.Frames)
            {
                gf.FrameDelay = (int)(gf.FrameDelay * val);
            }
        }

        public void MoveFromOBR(int x, int y)
        {
            giffer.MoveFromOBR(x, y);
        }
        public void ResizeFrame(int x, int y)
        {
            giffer.Resize(x, y);
        }
    }
}
