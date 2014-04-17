using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX;

namespace Kull.DX.Light
{
    public partial class Circle3dForm : BaseGrapForm
    {
        Material mtrl;

        public Circle3dForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionNormal), 100, _device, Usage.WriteOnly, CustomVertex.PositionNormal.Format, Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.onVertexBufferCreate);
            this.onVertexBufferCreate(vertexBuffer, null);
            mtrl = new Material();
            mtrl.Diffuse = Color.Red;
            mtrl.Ambient = Color.White;
        }


        protected virtual void onVertexBufferCreate(object sender,EventArgs e){
            CustomVertex.PositionNormal[] verts = (CustomVertex.PositionNormal[])vertexBuffer.Lock(0, 0);
            for (int i = 0; i < 50; i++) {
                float theta = (float)(2 * Math.PI * i) / 49;
                verts[2 * i].Position = new Vector3((float)Math.Sin(theta), -1, (float)Math.Cos(theta));
                verts[2 * i].Normal = new Vector3((float)Math.Sin(theta),0,(float)Math.Cos(theta));
                verts[2 * i+1].Position = new Vector3((float)Math.Sin(theta), 1, (float)Math.Cos(theta));
                verts[2 * i + 1].Normal = new Vector3((float)Math.Sin(theta), 0, (float)Math.Cos(theta));
            }
                vertexBuffer.Unlock();

        }

        protected void setupMatrices() {
            _device.Transform.World = Matrix.RotationAxis(new Vector3((float)Math.Cos(Environment.TickCount/250.0f),1,(float)Math.Sin(Environment.TickCount/250.0f)),Environment.TickCount/3000.0f);
           // _device.Transform.World = Matrix.RotationY(0);
            _device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 3.0f, -5.0f), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, 1.0f, 1.0f, 100.0f);
        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.CullMode = Cull.Clockwise;
            _device.RenderState.ZBufferEnable = true;
            _device.RenderState.Lighting = true;
        }

        protected virtual void setupLight() {
            _device.Material = mtrl;
            _device.Lights[0].Type = LightType.Directional;
            _device.Lights[0].Diffuse = Color.White;
            _device.Lights[0].Direction = new Vector3((float)Math.Cos(Environment.TickCount/250.0f),1.0f,(float)Math.Sin(Environment.TickCount/250.0f));
            _device.Lights[0].Enabled = true;
            _device.RenderState.Ambient = Color.FromArgb(0x404040);
        }

        protected override void rendering()
        {
            base.rendering();
            setupLight();
            setupMatrices();
            _device.SetStreamSource(0, vertexBuffer, 0);
            _device.VertexFormat = CustomVertex.PositionNormal.Format;
            _device.DrawPrimitives(PrimitiveType.TriangleStrip,0,(4*25)-2);
 
        }
    }
}
