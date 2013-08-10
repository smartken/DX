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

namespace Kull.DX.Meshs
{
    public partial class TeapotForm : BaseGrapForm
    {
        Mesh mesh = null;
        Material meshMaterials;
        public TeapotForm()
        {
            InitializeComponent();
        }


        protected override void onDeviceCreate(object sender, EventArgs e)
        {
            base.onDeviceCreate(sender, e);
            mesh = Mesh.Teapot(_device);
            
            meshMaterials = new Material();
            meshMaterials.Ambient = Color.White;
            meshMaterials.Diffuse = Color.White;

        }

        protected override void onDeviceReset(object sender, EventArgs e)
        {
            base.onDeviceReset(sender, e);
            _device.RenderState.ZBufferEnable = true;
            _device.RenderState.Ambient = Color.Black;
            _device.Lights[0].Type = LightType.Directional;
            _device.Lights[0].Diffuse = Color.White;
            _device.Lights[0].Direction = new Vector3(0,-1,0);
            _device.Lights[0].Update();
            _device.Lights[0].Enabled = true;
            _device.Material = meshMaterials;
        }

        protected virtual void setMatrices() {
            _device.Transform.World = Matrix.RotationYawPitchRoll(angle,angle,0);
            _device.Transform.View = Matrix.LookAtLH(new Vector3(0.0f, 1.0f, viewz), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 1.0f, 0.0f));
            _device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI/4,1.33f,1.0f,100.0f);
        }

        protected override void rendering()
        {
            base.rendering();
            setMatrices();
            mesh.DrawSubset(0);
            _device.Transform.World = Matrix.RotationYawPitchRoll(angle, angle, 0) * Matrix.Translation(3, 0, 4);
            mesh.DrawSubset(0);
        }
    }
}
