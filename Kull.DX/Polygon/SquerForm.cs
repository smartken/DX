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

namespace Kull.DX.Polygon
{
    public partial class SquerForm : BaseGrapForm
    {
        VertexBuffer vertexBuffer;

        public SquerForm()
        {
            InitializeComponent();
        }

        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            Device dev = (Device)sender;
            vertexBuffer = new VertexBuffer(typeof(CustomVertex.TransformedColored),18,dev,0,CustomVertex.TransformedColored.Format,Pool.Default);
            vertexBuffer.Created += new System.EventHandler(this.onVertexBufferCreate);
            this.onVertexBufferCreate(null, null);
        }

        public void onVertexBufferCreate(object sender, EventArgs e) { 
           CustomVertex.TransformedColored[] vetrs=(CustomVertex.TransformedColored[])vertexBuffer.Lock(0,0);
           vetrs[0].Position = new Vector4(100f,50.0f,0.5f,1.0f);
           vetrs[0].Color = Color.Red.ToArgb();
           vetrs[1].Position = new Vector4(200f, 50.0f, 0.5f, 1.0f);
           vetrs[1].Color = Color.Red.ToArgb();
           vetrs[2].Position = new Vector4(50f, 100.0f, 0.5f, 1.0f);
           vetrs[2].Color = Color.Red.ToArgb();
           vetrs[3].Position = new Vector4(50f, 100.0f, 0.5f, 1.0f);
           vetrs[3].Color = Color.Red.ToArgb();
           vetrs[4].Position = new Vector4(200f, 50.0f, 0.5f, 1.0f);
           vetrs[4].Color = Color.Red.ToArgb();
           vetrs[5].Position = new Vector4(150f, 100.0f, 0.5f, 1.0f);
           vetrs[5].Color = Color.Red.ToArgb();
           vetrs[6].Position = new Vector4(50f, 100.0f, 0.5f, 1.0f);
           vetrs[6].Color = Color.Green.ToArgb();
           vetrs[7].Position = new Vector4(150f, 100.0f, 0.5f, 1.0f);
           vetrs[7].Color = Color.Green.ToArgb();
           vetrs[8].Position = new Vector4(50f, 200.0f, 0.5f, 1.0f);
           vetrs[8].Color = Color.Green.ToArgb();
           vetrs[9].Position = new Vector4(50f, 200.0f, 0.5f, 1.0f);
           vetrs[9].Color = Color.Green.ToArgb();
           vetrs[10].Position = new Vector4(150f, 100.0f, 0.5f, 1.0f);
           vetrs[10].Color = Color.Green.ToArgb();
           vetrs[11].Position = new Vector4(150f, 200.0f, 0.5f, 1.0f);
           vetrs[11].Color = Color.Green.ToArgb();
           vetrs[12].Position = new Vector4(150f, 100.0f, 0.5f, 1.0f);
           vetrs[12].Color = Color.Yellow.ToArgb();
           vetrs[13].Position = new Vector4(200f, 50.0f, 0.5f, 1.0f);
           vetrs[13].Color = Color.Yellow.ToArgb();
           vetrs[14].Position = new Vector4(150f, 200.0f, 0.5f, 1.0f);
           vetrs[14].Color = Color.Yellow.ToArgb();
           vetrs[15].Position = new Vector4(150f, 200.0f, 0.5f, 1.0f);
           vetrs[15].Color = Color.Yellow.ToArgb();
           vetrs[16].Position = new Vector4(200f, 50.0f, 0.5f, 1.0f);
           vetrs[16].Color = Color.Yellow.ToArgb();
           vetrs[17].Position = new Vector4(200f, 150.0f, 0.5f, 1.0f);
           vetrs[17].Color = Color.Yellow.ToArgb();
           vertexBuffer.Unlock();
         
           
        }

        protected override void rendering()
        {
            base.rendering();
            _device.SetStreamSource(0, vertexBuffer, 0);
            _device.VertexFormat = CustomVertex.TransformedColored.Format;
            _device.DrawPrimitives(PrimitiveType.TriangleList, 0, 6);
        }
    }
}
