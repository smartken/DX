using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;

namespace Kull.DX
{
    public abstract class BaseGrapForm :Form
    {

        protected bool isRuning = true;
        protected Device _device;
        PresentParameters _presentParams = new PresentParameters();
        protected VertexBuffer vertexBuffer = null;
        protected float angle = 0, viewz = -6.0f;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _presentParams.Windowed = true;
                _presentParams.SwapEffect = SwapEffect.Discard;
                _presentParams.EnableAutoDepthStencil = true;
                _presentParams.AutoDepthStencilFormat = DepthFormat.D16;
                _device = new Device(0, DeviceType.Hardware, this, CreateFlags.SoftwareVertexProcessing, _presentParams);
                _device.DeviceReset += new System.EventHandler(this.onDeviceReset);
                this.onDeviceCreate(_device, null);
                this.onDeviceReset(_device, null);
                
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual void onDeviceReset(object sender, EventArgs e)
        {

        }

        protected virtual void onDeviceCreate(object sender, EventArgs e)
        {
        }

        protected virtual void beforeRender()
        {
            if (_device == null) return;
            _device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, Color.Blue, 1.0f, 0);
            _device.BeginScene();
            
        }

        protected virtual void afterRender()
        {
            _device.EndScene();
            _device.Present();
        }

        public virtual void draw() { 
           
            
            if (_device == null || !isRuning) return;
            beforeRender();
            rendering();
            afterRender();
       
        }

        protected virtual void rendering()
        {

        }




        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //isRuning = !((this.WindowState == FormWindowState.Minimized) || !this.Visible);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Left: angle += 0.1f; break;
                case Keys.Right: angle -= 0.1f; break;
                case Keys.Down: viewz += 0.1f; break;
                case Keys.Up: viewz -= 0.1f; break;
            }
        }
    }
}
