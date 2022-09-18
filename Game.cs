using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Proyecto1
{
    public class Game : GameWindow
    {
       
        
        public Triangulo triangulo, triangulo1;
        //-----------------------------------------------------------------------------------------------------------------
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) { }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color4.Black);
            /*  triangulo1 = new Triangulo(new Punto(20, 10, 0), 10, 10 ,0);
              triangulo = new Triangulo(new Punto(-20,10,0), 10, 10, 0);
              XmlSerializer xmlSerializer = new XmlSerializer(typeof(Triangulo));
              Stream myStream= new FileStream("myDoc.xml",FileMode.Create,FileAccess.Write);
              xmlSerializer.Serialize(myStream,triangulo);
              myStream.Close();*/
            DeserializeXmlStringToObject("myDoc.xml");
            base.OnLoad(e);     
        }

        private Object  DeserializeXmlStringToObject(string directorio)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Triangulo));
            FileStream fStream = File.Open(directorio, FileMode.Open);
            Object objetoDeserializar = serializer.Deserialize(fStream);
            fStream.Close();
            fStream.Dispose();
            return objetoDeserializar;
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            //GL.DeleteBuffer(VertexBufferObject);
            base.OnUnload(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //GL.DepthMask(true);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.LoadIdentity();
            //-----------------------
            this.triangulo.Dibujar();
            this.triangulo1.Dibujar();
            //-----------------------
            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnResize(EventArgs e)
        {
            float d = 30;
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-d, d, -d, d, -d, d);
            //GL.Frustum(-80, 80, -80, 80, 4, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            base.OnResize(e);
        }

       

    }
}
