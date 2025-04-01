using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Geo_worker.Models;

namespace Geo_worker.Controllers
{
    public class LayerDrawer
    {
        private Canvas _canvas;
        public LayerDrawer(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void DrawLayer(Layer layer)
        {
            foreach (var part in layer.Parts)
            {
                if(_canvas.Children.Contains((System.Windows.UIElement)part))
                {
                    Debug.WriteLine("Error: this layer is already on the canvas.");
                    return;
                }
                _canvas.Children.Add((System.Windows.UIElement)part);
            }
        }

        public void HideLayer(Layer layer)
        {
            foreach (var part in layer.Parts)
            {
                if (_canvas.Children.Contains((System.Windows.UIElement)part))
                {
                    _canvas.Children.Remove((System.Windows.UIElement)part);
                }
                else
                {
                    Debug.WriteLine("Error: this layer is not on the canvas.");
                    return;
                }
            }
        }

        public void ReDrawLayer(Layer layer)
        {
            HideLayer(layer);
            DrawLayer(layer);
        }

        public void ClearCanvas()
        {
            _canvas.Children.Clear();
        }
    }
}
