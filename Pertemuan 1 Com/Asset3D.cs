using System;
using System.Collections.Generic;
using System.IO;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ConsoleApp2
{
    class Asset3d
    {
        string Source = "D:/School/SMT5/Grafkom/Pertemuan 1 Com";

        List<Vector3> _vertices = new List<Vector3>();
        List<Vector3> _textureVertices = new List<Vector3>();
        List<Vector3> _normals = new List<Vector3>();

        List<uint> _indices = new List<uint>();
        String color;
        int _vertexBufferObject;//buffer obj (handle variabel vertex spy bs di vgacard
        int _vertexArrayObject;//VAO  mengurus terkait array vertex yg kita kirim
        int _elementBufferObject;
        Shader _shader; //mengurus apa yg ditampilkan ke layar kita.
        int indeks = 0; //utk gambar lingkaran.
        int[] _pascal = { };

        Matrix4 transform = Matrix4.Identity;

        public Vector3 _centerPosition = new Vector3(0, 0, 0);
        public List<Vector3> _euler = new List<Vector3>();


        public List<Asset3d> Child = new List<Asset3d>();//biar bisa pny child masing2

       // float[] _vertices2 =
       //{
       //      // Position          Normal
       //     -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
       //      0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
       //      0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
       //      0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
       //     -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
       //     -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

       //     -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
       //      0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
       //      0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
       //      0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
       //     -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
       //     -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

       //     -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
       //     -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
       //     -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
       //     -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
       //     -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
       //     -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

       //      0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
       //      0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
       //      0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
       //      0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
       //      0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
       //      0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

       //     -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
       //      0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
       //      0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
       //      0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
       //     -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
       //     -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

       //     -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
       //      0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
       //      0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
       //      0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
       //     -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
       //     -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
       // };

        float[] _vertices2 =
        {
            // Positions          Normals              Texture coords
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
        };

        Texture _diffuseMap;
        Texture _specularMap;

        public Asset3d(String color = "")
        {
            this.color = color;
            _euler.Add(new Vector3(1, 0, 0));
            _euler.Add(new Vector3(0, 1, 0));
            _euler.Add(new Vector3(0, 0, 1));
        }

        public void load(int size_x, int size_y)
        {
            //SETTINGAN BUFFER
            _vertexBufferObject = GL.GenBuffer(); //create buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject); //setting target dari buffer yang dituju
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes, _vertices.ToArray(), BufferUsageHint.StaticDraw);

            //SETTINGAN VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            //SETINGAN CARA BACA BINARY
            //DEFAULT
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            //Menyalakan var index[0] (layout location=0) yg ada pd shader.vert DEFAULT
            GL.EnableVertexAttribArray(0);

            ////menyalakan WARNA dari WINDOW.CS di index ke 1 yg ada pd shader.vert (layout location=1) RGB
            //GL.EnableVertexAttribArray(1);


            if (_indices.Count != 0)
            {
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count * sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);
            }

            //SETINGAN SHADER
            _shader = new Shader(Source + "/shaders/shader.vert",
                                 Source + "/shaders/lighting.frag");
            _shader.Use();

            //_shader = new Shader("D:/grafkom/C#/ConsoleApp2/shaders/shader.vert",
            //                     "D:/grafkom/C#/ConsoleApp2/shaders/shader1.frag");
            //_shader.Use();


            foreach (var item in Child)
            { //load semua anak
                item.load(size_x, size_y);
            }
        }

        public void load2(int size_x, int size_y, string skin, string spec)
        {
            //SETTINGAN BUFFER
            _vertexBufferObject = GL.GenBuffer(); //create buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject); //setting target dari buffer yang dituju
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices2.Length * sizeof(float), _vertices2, BufferUsageHint.StaticDraw);

            //SETTINGAN VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            //SETINGAN CARA BACA BINARY
            //DEFAULT
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

            //Menyalakan var index[0] (layout location=0) yg ada pd shader.vert DEFAULT
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2,2,VertexAttribPointerType.Float,false, 8 * sizeof(float), 6 * sizeof(float));
            GL.EnableVertexAttribArray(2);
            
            ////menyalakan WARNA dari WINDOW.CS di index ke 1 yg ada pd shader.vert (layout location=1) RGB
            //GL.EnableVertexAttribArray(1);


            if (_indices.Count != 0)
            {
                _elementBufferObject = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count * sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);
            }

            //SETINGAN SHADER
            _shader = new Shader(Source + "/Shaders/shader.vert",
                                 Source + "/Shaders/lighting.frag");
            _shader.Use();

            
            _diffuseMap = Texture.LoadFromFile(Source + "/Resources/" + skin);
            _specularMap = Texture.LoadFromFile(Source + "/Resources/" + spec);
            //_shader = new Shader("D:/grafkom/C#/ConsoleApp2/shaders/shader.vert",
            //                     "D:/grafkom/C#/ConsoleApp2/shaders/shader1.frag");
            //_shader.Use();


            foreach (var item in Child)
            { //load semua anak
                item.load(size_x, size_y);
            }
        }

        public void render(int _lines, Matrix4 camera_view, Matrix4 camera_projection, Vector3 _lightPos, Vector3 _cameraPos, Vector3 _cameraFront, Vector3[] pointlightpos, Vector3 cubeposition, int index)
        {
         
            _diffuseMap.Use(TextureUnit.Texture0);
            _specularMap.Use(TextureUnit.Texture1);

            _shader.Use();

            transform = Matrix4.CreateTranslation(cubeposition);
            //float angle = 20.0f * index;
            //transform = transform * Matrix4.CreateFromAxisAngle(new Vector3(1.0f,0.3f,0.5f),angle);

            _shader.SetMatrix4("transform", transform);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);
            _shader.SetVector3("viewPos", _cameraPos);

            // Material light.frag
            _shader.SetVector3("material.ambient", new Vector3(1.0f, 0.5f, 0.31f));
            //_shader.SetVector3("material.diffuse", new Vector3(1.0f, 0.5f, 0.31f));
            //_shader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _shader.SetVector3("material.diffuse", new Vector3(1.0f, 0.5f, 0.31f));
            _shader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
            _shader.SetFloat("material.shininess", 32); //metalic doff

            // Light light.frag
            // new vec3(0.2f) = new vec3(0.2f,0.2f,0.2f)
            // Directional light
            _shader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
            _shader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
            _shader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
            _shader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

            // Point lights
            for (int i = 0; i < pointlightpos.Length; i++)
            {
                _shader.SetVector3($"pointLights[{i}].position", pointlightpos[i]);
                _shader.SetVector3($"pointLights[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _shader.SetVector3($"pointLights[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _shader.SetVector3($"pointLights[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _shader.SetFloat($"pointLights[{i}].constant", 1.0f);
                _shader.SetFloat($"pointLights[{i}].linear", 0.09f);
                _shader.SetFloat($"pointLights[{i}].quadratic", 0.032f);
            }

            // Spot light
            _shader.SetVector3("spotLight.position", _cameraPos);
            _shader.SetVector3("spotLight.direction", _cameraFront);
            _shader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
            _shader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
            _shader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
            _shader.SetFloat("spotLight.constant", 1.0f);
            _shader.SetFloat("spotLight.linear", 0.09f);
            _shader.SetFloat("spotLight.quadratic", 0.032f);
            _shader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            _shader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));

            GL.BindVertexArray(_vertexArrayObject);

            if (_indices.Count != 0)
            {
                GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
            }
            else
            {
                if (_lines == 0) 
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Count);
                }
                else if (_lines == 1) 
                {
                    GL.DrawArrays(PrimitiveType.LineStrip, 0, _vertices.Count);
                }
                else if (_lines == 2) 
                {
                    GL.DrawArrays(PrimitiveType.LineLoop, 0, _vertices.Count);
                }
                else if (_lines == 3)
                {
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, _vertices.Count);
                }
                else if (_lines == 4)
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
                }
            }

            foreach (var item in Child)
            {
                item.render(_lines, camera_view, camera_projection,_lightPos,_cameraPos,_cameraFront, pointlightpos, cubeposition, index);
            }

        } 

        public void setVertices(List<Vector3> vertices)
        {
            _vertices = vertices;
        }

        public bool getVerticesLength()
        {
            if (_vertices.Count == 0)
            {
                return false;
            }
            if (_vertices.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void createBoxVertices(float x, float y, float z, float length)//ttik pusat dari box dimana
        {//length panjang dari titik kubus

            _centerPosition.X = x; //jgn lupa selalu tambahkan ini
            _centerPosition.Y = y;
            _centerPosition.Z = z;

            Vector3 temp_vector;

            //TITIK 1
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 2
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 3
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 4
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z - length / 2.0f;
            _vertices.Add(temp_vector);

            //TITIK 5
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 6
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y + length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 7
            temp_vector.X = x - length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);
            //TITIK 8
            temp_vector.X = x + length / 2.0f;
            temp_vector.Y = y - length / 2.0f;
            temp_vector.Z = z + length / 2.0f;
            _vertices.Add(temp_vector);

            _indices = new List<uint>
            {
                //SEGITIGA DEPAN 1
                0,1,2,
                //SEGITIGA DEPAN 2
                1,2,3,
                //SEGITIGA ATAS 1
                0,4,5,
                //SEGITIGA ATAS 2
                0,1,5,
                //SEGITIGA KANAN 1
                1,3,5,
                //SEGITIGA KANAN 2
                3,5,7,
                //SEGITIGA KIRI 1
                0,2,4,
                //SEGITIGA KIRI 2
                2,4,6,
                //SEGITIGA BELAKANG 1
                4,5,6,
                //SEGITIGA BELAKANG 2
                5,6,7,
                //SEGITIGA BAWAH 1
                2,3,6,
                //SEGITIGA BAWAH 2
                3,6,7
            };
        }


        public void createElipsoid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, String color)
        {
            this.color = color;
            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = -pi; u <= pi; u += pi / 300)//u range di tabel
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 300) //v range di tabel, pembagi semakin besar, garis semakin banyak
                {
                    temp_vector.X = _x + (float)Math.Cos(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (float)Math.Cos(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Sin(v) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createElipsoidV2(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int sectorcount, int stackcount)
        {
            float pi = (float)Math.PI;
            Vector3 temp_vector;

            float sectorStep = 2 * (float)pi / sectorcount;
            float stackStep = (float)pi / stackcount;
            float sectorAngle, stackAngle, x, y, z;

            for (int i = 0; i <= stackcount; ++i)
            {
                stackAngle = pi / 2 - i * stackStep;
                x = radiusX * (float)Math.Cos(stackAngle);
                y = radiusY * (float)Math.Cos(stackAngle);
                z = radiusZ * (float)Math.Sin(stackAngle);

                for (int j = 0; j <= sectorcount; ++j)
                {
                    sectorAngle = j * sectorStep;
                    temp_vector.X = x * (float)Math.Cos(sectorAngle);
                    temp_vector.Y = y * (float)Math.Sin(sectorAngle);
                    temp_vector.Z = z;
                    _vertices.Add(temp_vector);
                }
            }

            uint k1, k2;
            for (int i = 0; i < stackcount; ++i)
            {
                k1 = (uint)(i * (sectorcount + 1));
                k2 = (uint)(k1 * sectorcount + 1);
                for (int j = 0; j < sectorcount; ++j, ++k1, ++k2)
                {
                    if (i != 0)
                    {
                        _indices.Add(k1);
                        _indices.Add(k2);
                        _indices.Add(k1 + 1);
                    }
                    if (i != (stackcount - 1))
                    {
                        _indices.Add(k1 + 1);
                        _indices.Add(k2);
                        _indices.Add(k2 + 1);
                    }
                }
            }
        }

        public void createHyperboloidSatuSisi(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, String m_color)
        {
            float pi = (float)Math.PI;
            Vector3 temp_vector;

            this.color = m_color;
            for (float u = -pi; u <= pi; u += pi / 300)//u range di tabel
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 300) //v range di tabel, pembagi semakin besar, garis semakin banyak
                {
                    temp_vector.X = _x + (1 / (float)Math.Cos(v)) * (float)Math.Cos(u) * radiusX;
                    temp_vector.Y = _y + (1 / (float)Math.Cos(v)) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Z = _z + (float)Math.Tan(v) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createHyperboloidDuaSisi(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = -pi / 2; u <= pi / 2; u += pi / 30)//u range di tabel
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 30) //v range di tabel, pembagi semakin besar, garis semakin banyak
                {
                    temp_vector.Z = _x + (float)Math.Tan(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.X = _y + (float)Math.Tan(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Y = _z + (1 / (float)Math.Cos(v)) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
            for (float u = pi / 2; u <= 3 * (pi / 2); u += pi / 30)//u range di tabel
            {
                for (float v = -pi / 2; v <= pi / 2; v += pi / 30) //v range di tabel, pembagi semakin besar, garis semakin banyak
                {
                    temp_vector.Z = _x + (float)Math.Tan(v) * (float)Math.Cos(u) * radiusX;
                    temp_vector.X = _y + (float)Math.Tan(v) * (float)Math.Sin(u) * radiusY;
                    temp_vector.Y = _z + (1 / (float)Math.Cos(v)) * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createCone(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = -pi; u <= pi; u += pi / 30)//u range di tabel
            {
                for (float v = 0; v <= 100; v += pi / 30) //v range di tabel, pembagi semakin besar, garis semakin banyak
                {
                    temp_vector.X = _x + (float)Math.Cos(u) * (radiusX * v);
                    temp_vector.Y = _y + ((float)Math.Sin(u) * (radiusY * v));
                    temp_vector.Z = _z + (float)v * radiusZ;
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createEllipticParaboloid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = -pi; u <= pi; u += pi / 30)//u range di tabel
            {
                for (float v = 30; v >= 0; v -= pi / 30) //v range di tabel, pembagi semakin besar, garis semakin banyak
                {
                    temp_vector.Y = _x + (float)Math.Cos(u) * (radiusX * v);
                    temp_vector.Z = _y + ((float)Math.Sin(u) * (radiusY * v));
                    temp_vector.X = _z + (float)Math.Pow(v, 2);
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createHyperboloidParaboloid(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z)
        {
            float pi = (float)Math.PI;
            Vector3 temp_vector;

            for (float u = -pi; u <= pi; u += pi / 30)//u range di tabel
            {
                for (float v = 30; v >= 0; v -= pi / 30) //v range di tabel, pembagi semakin besar, garis semakin banyak
                {
                    temp_vector.Y = _x + (float)Math.Tan(u) * (radiusX * v);
                    temp_vector.Z = _y + (1 / (float)Math.Cos(u)) * (radiusY * v);
                    temp_vector.X = _z + (float)Math.Pow(v, 2);
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void loadObjFile(string path, String m_color)
        {
            this.color = m_color;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to open");
            }

            using (StreamReader streamreader = new StreamReader(path))
            {
                while (!streamreader.EndOfStream)
                {//vt= verteks tekstur, vn=lighting/shading, f=face indices
                    List<string> words = new List<string>(streamreader.ReadLine().ToLower().Split(" "));
                    words.RemoveAll(s => s == string.Empty);
                    if (words.Count == 0)
                    {
                        continue;
                    }
                    string type = words[0];
                    words.RemoveAt(0);

                    switch (type)
                    {//ngecilin obj /10
                        case "v":
                            _vertices.Add(new Vector3(float.Parse(words[0]) / 5, float.Parse(words[1]) / 5, float.Parse(words[2]) / 5));
                            break;
                        case "vt":
                            _textureVertices.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]), words.Count < 3 ? 0 : float.Parse(words[2])));
                            break;
                        case "vn":
                            _normals.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2])));
                            break;
                        case "f":
                            foreach (string w in words)
                            {
                                if (w.Length == 0)
                                {
                                    continue;
                                }
                                string[] comps = w.Split("/");
                                _indices.Add(uint.Parse(comps[0]) - 1);
                                //f tgh
                                //f blkng
                            }
                            break;
                    }
                }
            }
        }

        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            //pivot -> mau rotate di titik mana
            //vector -> mau rotate di sumbu apa? (x,y,z)
            //angle -> rotatenya berapa derajat?
            var real_angle = angle;
            angle = MathHelper.DegreesToRadians(angle);

            //mulai ngerotasi
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = getRotationResult(pivot, vector, angle, _vertices[i]);
            }
            //rotate the euler direction
            for (int i = 0; i < 3; i++)
            {
                _euler[i] = getRotationResult(pivot, vector, angle, _euler[i], true);

                //NORMALIZE
                //LANGKAH - LANGKAH
                //length = akar(x^2+y^2+z^2)
                float length = (float)Math.Pow(Math.Pow(_euler[i].X, 2.0f) + Math.Pow(_euler[i].Y, 2.0f) + Math.Pow(_euler[i].Z, 2.0f), 0.5f);
                Vector3 temporary = new Vector3(0, 0, 0);
                temporary.X = _euler[i].X / length;
                temporary.Y = _euler[i].Y / length;
                temporary.Z = _euler[i].Z / length;
                _euler[i] = temporary;
            }
            _centerPosition = getRotationResult(pivot, vector, angle, _centerPosition); //nyimpen titik tengah obj

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes,
                _vertices.ToArray(), BufferUsageHint.StaticDraw);

            foreach (var item in Child)
            {
                item.rotate(pivot, vector, real_angle);
            }
        }

        Vector3 getRotationResult(Vector3 pivot, Vector3 vector, float angle, Vector3 point, bool isEuler = false)
        {
            Vector3 temp, newPosition;
            if (isEuler)
            {
                temp = point;
            }
            else
            {
                temp = point - pivot;
            }

            newPosition.X =
                (float)temp.X * (float)(Math.Cos(angle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(angle))) +
                (float)temp.Y * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) - vector.Z * Math.Sin(angle)) +
                (float)temp.Z * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) + vector.Y * Math.Sin(angle));
            newPosition.Y =
                (float)temp.X * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) + vector.Z * Math.Sin(angle)) +
                (float)temp.Y * (float)(Math.Cos(angle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(angle))) +
                (float)temp.Z * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) - vector.X * Math.Sin(angle));
            newPosition.Z =
                (float)temp.X * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) - vector.Y * Math.Sin(angle)) +
                (float)temp.Y * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) + vector.X * Math.Sin(angle)) +
                (float)temp.Z * (float)(Math.Cos(angle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(angle)));

            if (isEuler)
            {
                temp = newPosition;
            }
            else
            {
                temp = newPosition + pivot;
            }
            return temp;
        }

        public void resetEuler()
        {
            _euler[0] = new Vector3(1, 0, 0);
            _euler[1] = new Vector3(0, 1, 0);
            _euler[2] = new Vector3(0, 0, 1);
        }

        public void addChildCubes(float x, float y, float z, float length, String color)
        {
            Asset3d newChild = new Asset3d(color);
            newChild.createBoxVertices(x, y, z, length);
            Child.Add(newChild);
        }

        public void addChildBall(float rX, float rY, float rZ, float _x, float _y, float _z, String m_color)
        {
            Asset3d newChild = new Asset3d(color);
            newChild.createElipsoid(rX, rY, rZ, _x, _y, _z, m_color);
            Child.Add(newChild);
        }

        public void addChildTabung(float _positionX = 0.4f,
        float _positionY = 0.4f,
        float _positionZ = 0.4f,
        float _radius = 0.3f, float _height = 0.2f, float _extended = 0.5f)
        {
            Asset3d newChild = new Asset3d(color);
            newChild.createTabung(_positionX, _positionY, _positionZ, _radius, _height, _extended);
            Child.Add(newChild);
        }

        public void trans(float x, float y, float z)
        {
            transform = transform * Matrix4.CreateTranslation(x, y, z);
        }

        //untuk mengatur ukuran objek
        public void scale(float x)
        {

            transform = transform * Matrix4.CreateTranslation(-1 * (_centerPosition)) * Matrix4.CreateScale(x) * Matrix4.CreateTranslation((_centerPosition));
        }

        public void createTabung(float _positionX = 0.4f,
        float _positionY = 0.4f,
        float _positionZ = 0.4f,
        float _radius = 0.3f, float _height = 0.2f, float _extended = 0.5f)
        {
            _centerPosition.X = _positionX; //jgn lupa selalu tambahkan ini
            _centerPosition.Y = _positionY;
            _centerPosition.Z = _positionZ;

            Vector3 temp_vector;
            float _pi = (float)Math.PI;


            for (float v = -_height / 2; v <= (_height / 2); v += 0.0001f)
            {
                Vector3 p = setBeizer((v + (_height / 2)) / _height);
                for (float u = -_pi; u <= _pi; u += (_pi / 30))
                {

                    temp_vector.X = p.X + _radius * (float)Math.Cos(u);
                    temp_vector.Y = p.Y + _radius * (float)Math.Sin(u);
                    temp_vector.Z = _positionZ + v;

                    _vertices.Add(temp_vector);

                }
            }



        }

        Vector3 setBeizer(float t)
        {
            //Console.WriteLine(t);
            Vector3 p = new Vector3(0f, 0f, 0f);
            float[] k = new float[3];

            k[0] = (float)Math.Pow((1 - t), 3 - 1 - 0) * (float)Math.Pow(t, 0) * 1;
            k[1] = (float)Math.Pow((1 - t), 3 - 1 - 1) * (float)Math.Pow(t, 1) * 2;
            k[2] = (float)Math.Pow((1 - t), 3 - 1 - 2) * (float)Math.Pow(t, 2) * 1;


            //titik 1
            p.X += k[0] * _centerPosition.X;
            p.Y += k[0] * _centerPosition.Y;

            //titik 2
            p.X += k[1] * (_centerPosition.X);
            p.Y += k[1] * _centerPosition.Y;

            //titik 3
            p.X += k[2] * _centerPosition.X;
            p.Y += k[2] * _centerPosition.Y;

            //Console.WriteLine(p.X + " "+ p.Y);

            return p;

        }
    }
}
