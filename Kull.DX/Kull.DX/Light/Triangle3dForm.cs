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
    public partial class Triangle3dForm : BaseGrapForm
    {

        Material mtrl;

        public Triangle3dForm()
        {
            InitializeComponent();
        }


        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionNormal), 3, _device, Usage.WriteOnly, CustomVertex.PositionNormal.Format, Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.onVertexBufferCreate);
            this.onVertexBufferCreate(vertexBuffer,null);
            mtrl = new Material();
            mtrl.Diffuse = Color.Yellow;
            mtrl.Ambient = Color.Red;
        }

        protected virtual void onVertexBufferCreate(object sender, EventArgs e) {
            CustomVertex.PositionNormal[] vetrs = (CustomVertex.PositionNormal[])vertexBuffer.Lock(0,0);
            vetrs[0].Position = new Vector3(-1.0f,-1.0f,0.0f);
            vetrs[0].Normal = new Vector3(0,0,-1);
            vetrs[1].Position = new Vector3(0.0f, 1.0f, 0.0f);
            vetrs[1].Normal = new Vector3(0, 0, -1);
            vetrs[2].Position = new Vector3(1.0f, -1.0f, 0.0f);
            vetrs[2].Normal = new Vector3(0, 0, -1);
            vertexBuffer.Unlock();
        }

        private void SetupMatrices() {
            _device.Transform.World = Matrix.RotationY(angle);
            _device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 3.0f, viewz), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, 1.0f, 1.0f, 100.0f);
            
        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.CullMode = Cull.None;
            _device.RenderState.ZBufferEnable = true;
            _device.RenderState.Lighting = true;
            setupLights();
        }

        private void setupLights() {
            _device.Material = mtrl;
            _device.Lights[0].Type = LightType.Directional;
            _device.Lights[0].Diffuse = Color.White;
            _device.Lights[0].Direction = new Vector3(0, 0, 1);
            _device.Lights[0].Update();
            _device.Lights[0].Enabled = true;
            _device.RenderState.Ambient = Color.FromArgb(0x808080);
        }

        protected override void rendering()
        {
            base.rendering();
            SetupMatrices();
            _device.SetStreamSource(0, vertexBuffer, 0);
            _device.VertexFormat = CustomVertex.PositionNormal.Format;
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
        }
    }
}
