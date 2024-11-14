using BIUK9000.GifferComponents;
using BIUK9000.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIUK9000
{
    public class GifferController
    {
        private Giffer giffer;
        private GFL SavedLayerForLerp;
        private GFL SavedLayerForLPC;
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
        public void SaveLayerStateForLerp(int frameIndex, int layerIndex)
        {
            SavedLayerForLerp = giffer.Frames[frameIndex].Layers[layerIndex].Clone();
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
            if(SavedLayerForLerp != null) SavedLayerForLerp.Visible = visible;
        }
        public void LerpExecute(int startFrameIndex, int endFrameIndex, int layerID)
        {
            GFL startLayer = giffer.Frames[startFrameIndex].Layers.Find(layer => layerID == layer.LayerID);
            GFL endLayer = giffer.Frames[endFrameIndex].Layers.Find(layer => layerID == layer.LayerID);
            if (startLayer == null || endLayer == null)
            {
                MessageBox.Show("You must start the lerp first");
                return;
            }
            int totalFrames = endFrameIndex - startFrameIndex;
            for (int i = startFrameIndex + 1; i <= endFrameIndex; i++)
            {
                double distance = 1 - (endFrameIndex - i) / (double)totalFrames;
                GifFrame cgf = giffer.Frames[i];
                GFL layerToLerp = cgf.Layers.Find(layer => layer.LayerID == startLayer.LayerID);
                layerToLerp?.Lerp(startLayer, endLayer, distance);
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
                            layerToUpdate.CopyDifferingParams(SavedLayerForLerp, startLayer);
                        }
                        else
                        {
                            layerToUpdate.CopyParameters(startLayer);
                        }
                    }
                }
            }
        }
    }
}
