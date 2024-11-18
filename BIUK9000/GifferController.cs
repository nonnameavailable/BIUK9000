using BIUK9000.GifferComponents;
using BIUK9000.GifferComponents.GFLVariants;
using BIUK9000.UI;
using BIUK9000.UI.CustomControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000
{
    public class GifferController
    {
        private Giffer giffer;
        private GFL SavedLayerForApply;
        private GFL SavedLayerForLPC;
        int _previousLayerID;
        public int FrameCount { get => giffer.FrameCount; }
        public GifferController(Giffer giffer)
        {
            this.giffer = giffer;
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
            SavedLayerForApply = giffer.Frames[frameIndex].Layers[layerIndex].Clone();
        }
        public void SaveLayerForLPC(int frameIndex, int layerIndex)
        {
            SavedLayerForLPC = giffer.Frames[frameIndex].Layers[layerIndex];
        }
        public bool ShouldSwitchLPC(int frameIndex, int layerIndex)
        {
            GFL layer = giffer.Frames[frameIndex].Layers[layerIndex];
            if (layer == null || SavedLayerForLPC == null) return true;
            return !(layer.GetType().Name == SavedLayerForLPC.GetType().Name);
        }
        public int PreviousLayerID()
        {
            return SavedLayerForLPC.LayerID;
        }
        public void SetSavedLayerVisibility(bool visible)
        {
            if(SavedLayerForApply != null) SavedLayerForApply.Visible = visible;
        }
        public void LerpExecute(List<int> marks, int selectedLayerIndex, List<Point> mouseTrace = null)
        {
            if(marks.Count < 2)
            {
                MessageBox.Show("You must add at least 2 marks");
                return;
            }
            int startFrameIndex = Math.Min(marks[0], marks[1]);
            int endFrameIndex = Math.Max(marks[0], marks[1]);
            GFL startLayer = giffer.Frames[startFrameIndex].Layers[selectedLayerIndex];
            GFL endLayer = giffer.Frames[endFrameIndex].Layers.Find(layer => startLayer.LayerID == layer.LayerID);
            Debug.Print("startLayer rot" + startLayer.Rotation.ToString());
            Debug.Print("endLayer rot" + endLayer.Rotation.ToString());
            if (startLayer == null || endLayer == null)
            {
                MessageBox.Show("Layers not found");
                return;
            }
            int totalFrames = endFrameIndex - startFrameIndex;
            OVector difference = null;
            if(mouseTrace != null && mouseTrace.Count > 0)
            {
                difference = startLayer.Position.Copy().Subtract(new OVector(mouseTrace[0]));
            }
            for (int i = startFrameIndex + 1; i < endFrameIndex; i++)
            {
                double distance = 1 - (endFrameIndex - i) / (double)totalFrames;
                GifFrame cgf = giffer.Frames[i];
                GFL layerToLerp = cgf.Layers.Find(layer => layer.LayerID == startLayer.LayerID);
                if(mouseTrace == null || mouseTrace.Count == 0)
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

        public void ApplyLayerParams(int currentFrameIndex, int currentLayerIndex, ApplyParamsMode apm)
        {
            if (apm == ApplyParamsMode.applyNone) return;
            int cli = currentLayerIndex;
            GFL startLayer = giffer.Frames[currentFrameIndex].Layers[cli];
            for (int i = currentFrameIndex + 1; i < giffer.FrameCount; i++)
            {
                GifFrame gf = giffer.Frames[i];
                if (cli >= 0 && cli < gf.Layers.Count)
                {
                    GFL layerToUpdate;
                    if (gf.Layers[cli].LayerID != startLayer.LayerID)
                    {
                        layerToUpdate = gf.Layers.Find(gfl => gfl.LayerID == startLayer.LayerID);
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
        public void AddNewLayer(Keys keyData)
        {
            int nextId = giffer.NextLayerID();
            GFL nextLayer = null;
            if (keyData == Keys.T)
            {
                nextLayer = new TextGFL(nextId);
            }
            else if (keyData == Keys.B)
            {
                nextLayer = new PlainGFL(nextId);
            }
            if (nextLayer == null) return;
            foreach (GifFrame gf in giffer.Frames)
            {
                gf.AddLayer(nextLayer.Clone());
            }
        }
        public Point MousePositionOnLayer(int frameIndex, int layerIndex, Point mousePosition)
        {
            GFL gflt = giffer.Frames[frameIndex].Layers[layerIndex];
            if (gflt is BitmapGFL)
            {
                BitmapGFL gfl = gflt as BitmapGFL;
                OVector mousePositionOnFrame = new OVector(mousePosition);
                OVector resultWithoutRotation = mousePositionOnFrame.Copy().Subtract(gfl.Position).Div2(gfl.HRatio, gfl.VRatio);
                OVector arm = resultWithoutRotation.Copy().Subtract(gfl.AbsoluteCenter());
                arm.Mult2(gfl.HRatio, gfl.VRatio);
                arm.Rotate(-gfl.Rotation);
                arm.Div2(gfl.HRatio, gfl.VRatio);
                OVector result = arm.Copy().Add(gfl.AbsoluteCenter());
                return result.ToPoint();
            }
            else
            {
                return new Point(0, 0);
            }

        }
        public void DeleteLayerByID(int layerID)
        {
            foreach (GifFrame gf in giffer.Frames)
            {
                if(gf.Layers.Count > 1)
                {
                    GFL layerToDelete = gf.Layers.Find(layer => layer.LayerID == layerID);
                    if (layerToDelete != null) gf.Layers.Remove(layerToDelete);
                }
            }
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
            int[] layerIDs = new int[newGiffer.Frames[0].Layers.Count];
            for (int i = 0; i < layerIDs.Length; i++)
            {
                layerIDs[i] = giffer.NextLayerID();
            }
            foreach (GifFrame frame in newGiffer.Frames)
            {
                for (int i = 0; i < frame.Layers.Count; i++)
                {
                    frame.Layers[i].LayerID = layerIDs[i];
                }
            }
            giffer.Frames.InsertRange(insertAt, newGiffer.Frames);
        }
        public void DeleteColor(int frameIndex, int layerIndex, Point p, int tolerance)
        {
            BitmapGFL currentLayer = giffer.Frames[frameIndex].Layers[layerIndex] as BitmapGFL;
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
    }
}
