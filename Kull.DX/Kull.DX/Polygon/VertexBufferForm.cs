using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX.Direct3D;

namespace Kull.DX.Polygon
{
    public partial class VertexBufferForm :  BaseGrapForm
    {
        VertexBuffer vertexBuffer = null;

        public VertexBufferForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            Device dev = (Device)sender;
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.TransformedColored), 3, dev, 0, CustomVertex.TransformedColored.Format, Pool.Default);
            vertexBuffer.Created+=new System.EventHandler(this.onVertexBufferCreate);
            this.onVertexBufferCreate(vertexBuffer,null);
        }

        protected virtual void onVertexBufferCreate(object sender, EventArgs args) {

            CustomVertex.TransformedColored[] verts = (CustomVertex.TransformedColored[])vertexBuffer.Lock(0, 0);
            verts[0].X = 150;
            verts[0].Y = 50;
            verts[0].Z = 0.5f;
            verts[0].Rhw = 1;
            verts[0].Color = Color.Aqua.ToArgb();
            verts[1].X = 250;
            verts[1].Y = 250;
            verts[1].Z = 0.5f;
            verts[1].Rhw = 1;
            verts[1].Color = Color.Brown.ToArgb();
            verts[2].X = 50;
            verts[2].Y = 250;
            verts[2].Z = 0.5f;
            verts[2].Rhw = 1;
            verts[2].Color = Color.LightPink.ToArgb();
            vertexBuffer.Unlock();
        }

        protected override void rendering()
        {
            base.rendering();
            _device.SetStreamSource(0,vertexBuffer,0);
            _device.VertexFormat = CustomVertex.TransformedColored.Format;
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
        }
    }
}
