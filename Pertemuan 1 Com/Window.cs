using System;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Mathematics;
using System.Collections.Generic;
using LearnOpenTK.Common;


namespace ConsoleApp2
{
    class Window : GameWindow /*gamewindow=class windownya openTK*/
    {

        //List<Vector3> _Vertices = new List<Vector3>();
        string Source = "D:/School/SMT5/Grafkom/Pertemuan 1 Com";
        Asset3d[] _object3d = new Asset3d[6];
        Asset2d[] _object2d = new Asset2d[3];
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        bool _firstMove = true;
        Vector2 _lastPos;
        Vector3 _objecPost = new Vector3(1.2f,1.0f,2.0f);
        float _rotationSpeed = 1f;
        Camera _camera;
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        float[] _verticesbez = { };
        uint[] _indicesbez = { };
        Vector3 a;

        float[] _vertices =
        {
            // Position
            -0.5f, -0.5f, -0.5f, // Front face
             0.5f, -0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
             0.5f,  0.5f, -0.5f,
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,

            -0.5f, -0.5f,  0.5f, // Back face
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,

            -0.5f,  0.5f,  0.5f, // Left face
            -0.5f,  0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f, -0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,

             0.5f,  0.5f,  0.5f, // Right face
             0.5f,  0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,

            -0.5f, -0.5f, -0.5f, // Bottom face
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f, -0.5f,

            -0.5f,  0.5f, -0.5f, // Top face
             0.5f,  0.5f, -0.5f,
             0.5f,  0.5f,  0.5f,
             0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f,  0.5f,
            -0.5f,  0.5f, -0.5f
        };


        int _vertexBufferLightObject;
        int _vaoLamp;
        Shader _lampShader;
        Vector3 _lightPos = new Vector3(1.0f, 0.0f, 2.0f);

        private readonly Vector3[] _pointLightPositions =
        {
            new Vector3(5f, 10f, 0f),
            new Vector3(-2.0f, 10.0f, 6.0f),
            new Vector3(14.0f, 10.0f, 6.0f),
            new Vector3(7.0f, 15.0f, -10.0f),
        };

        private readonly Vector3[] _cubePositions =
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 0.0f, -1.0f),
            new Vector3(0.0f, 1.0f, -1.0f),
            new Vector3(0.0f, 0.0f, -2.0f),
            new Vector3(0.0f, 1.0f, -2.0f),
            new Vector3(0.0f, 2.0f, -2.0f),
            new Vector3(0.0f, 0.0f, -3.0f),
            new Vector3(0.0f, 1.0f, -3.0f),
            new Vector3(0.0f, 2.0f, -3.0f),
            new Vector3(0.0f, 3.0f, -3.0f),
            new Vector3(0.0f, 1.0f, -4.0f),
            new Vector3(0.0f, 2.0f, -4.0f),
            new Vector3(0.0f, 3.0f, -4.0f),
            new Vector3(0.0f, 4.0f, -5.0f),
            new Vector3(0.0f, 5.0f, -6.0f),
            new Vector3(0.0f, 6.0f, -7.0f),
            new Vector3(0.0f, 7.0f, -8.0f),
            new Vector3(0.0f, 8.0f, -9.0f),
            new Vector3(0.0f, 9.0f, -10.0f),
            new Vector3(0.0f, 9.0f, -11.0f),////////////////////////////////////

            new Vector3(-1.0f, 0.0f, 0.0f),
            new Vector3(-1.0f, 0.0f, -1.0f),
            new Vector3(-1.0f, 1.0f, -1.0f),
            new Vector3(-1.0f, 0.0f, -2.0f),
            new Vector3(-1.0f, 1.0f, -2.0f),
            new Vector3(-1.0f, 2.0f, -2.0f),
            new Vector3(-1.0f, 0.0f, -3.0f),
            new Vector3(-1.0f, 1.0f, -3.0f),
            new Vector3(-1.0f, 2.0f, -3.0f),
            new Vector3(-1.0f, 3.0f, -3.0f),
            new Vector3(-1.0f, 1.0f, -4.0f),
            new Vector3(-1.0f, 2.0f, -4.0f),
            new Vector3(-1.0f, 3.0f, -4.0f),
            new Vector3(-1.0f, 4.0f, -5.0f),
            new Vector3(-1.0f, 5.0f, -6.0f),
            new Vector3(-1.0f, 6.0f, -7.0f),
            new Vector3(-1.0f, 7.0f, -8.0f),
            new Vector3(-1.0f, 8.0f, -9.0f),
            new Vector3(-1.0f, 9.0f, -10.0f),
            new Vector3(-1.0f, 9.0f, -11.0f),///////////////////////////////////////

            new Vector3(13.0f, 0.0f, 0.0f),
            new Vector3(13.0f, 0.0f, -1.0f),
            new Vector3(13.0f, 1.0f, -1.0f),
            new Vector3(13.0f, 0.0f, -2.0f),
            new Vector3(13.0f, 1.0f, -2.0f),
            new Vector3(13.0f, 2.0f, -2.0f),
            new Vector3(13.0f, 0.0f, -3.0f),
            new Vector3(13.0f, 1.0f, -3.0f),
            new Vector3(13.0f, 2.0f, -3.0f),
            new Vector3(13.0f, 3.0f, -3.0f),
            new Vector3(13.0f, 1.0f, -4.0f),
            new Vector3(13.0f, 2.0f, -4.0f),
            new Vector3(13.0f, 3.0f, -4.0f),
            new Vector3(13.0f, 4.0f, -5.0f),
            new Vector3(13.0f, 5.0f, -6.0f),
            new Vector3(13.0f, 6.0f, -7.0f),
            new Vector3(13.0f, 7.0f, -8.0f),
            new Vector3(13.0f, 8.0f, -9.0f),
            new Vector3(13.0f, 9.0f, -10.0f),
            new Vector3(13.0f, 9.0f, -11.0f),
        };

        private readonly Vector3[] _cubePos2 =
        {
            new Vector3(1.0f, 9.0f, -10.0f),
            new Vector3(2.0f, 9.0f, -10.0f),
            new Vector3(3.0f, 9.0f, -10.0f),
            new Vector3(4.0f, 9.0f, -10.0f),
            new Vector3(5.0f, 9.0f, -10.0f),
            new Vector3(6.0f, 9.0f, -10.0f),
            new Vector3(7.0f, 9.0f, -10.0f),
            new Vector3(8.0f, 9.0f, -10.0f),
            new Vector3(9.0f, 9.0f, -10.0f),
            new Vector3(10.0f, 9.0f, -10.0f),
            new Vector3(11.0f, 9.0f, -10.0f),
            new Vector3(12.0f, 9.0f, -10.0f),
            new Vector3(13.0f, 9.0f, -10.0f),

            new Vector3(1.0f, 9.0f, -11.0f),
            new Vector3(2.0f, 9.0f, -11.0f),
            new Vector3(3.0f, 9.0f, -11.0f),
            new Vector3(4.0f, 9.0f, -11.0f),
            new Vector3(5.0f, 9.0f, -11.0f),
            new Vector3(6.0f, 9.0f, -11.0f),
            new Vector3(7.0f, 9.0f, -11.0f),
            new Vector3(8.0f, 9.0f, -11.0f),
            new Vector3(9.0f, 9.0f, -11.0f),
            new Vector3(10.0f, 9.0f, -11.0f),
            new Vector3(11.0f, 9.0f, -11.0f),
            new Vector3(12.0f, 9.0f, -11.0f),
            new Vector3(13.0f, 9.0f, -11.0f),

        };

        private readonly Vector3[] _cuberumput =
       {

            new Vector3(0.0f, 0.0f, 1.0f),//petaksawah
            new Vector3(-1.0f, 0.0f, 1.0f),
            new Vector3(1.0f, 0.0f, 1.0f),
            new Vector3(2.0f, 0.0f, 1.0f),
            new Vector3(3.0f, 0.0f, 1.0f),
            new Vector3(4.0f, 0.0f, 1.0f),
            new Vector3(5.0f, 0.0f, 1.0f),
            new Vector3(6.0f, 0.0f, 1.0f),
            new Vector3(7.0f, 0.0f, 1.0f),
            new Vector3(8.0f, 0.0f, 1.0f),
            new Vector3(9.0f, 0.0f, 1.0f),
            new Vector3(10.0f, 0.0f, 1.0f),
            new Vector3(11.0f, 0.0f, 1.0f),
            new Vector3(12.0f, 0.0f, 1.0f),//
            new Vector3(-1.0f, 0.0f, 2.0f),
            new Vector3(0.0f, 0.0f, 2.0f),
            new Vector3(1.0f, 0.0f, 2.0f),
            new Vector3(2.0f, 0.0f, 2.0f),
            new Vector3(3.0f, 0.0f, 2.0f),
            new Vector3(4.0f, 0.0f, 2.0f),
            new Vector3(5.0f, 0.0f, 2.0f),
            new Vector3(6.0f, 0.0f, 2.0f),
            new Vector3(7.0f, 0.0f, 2.0f),
            new Vector3(8.0f, 0.0f, 2.0f),
            new Vector3(9.0f, 0.0f, 2.0f),
            new Vector3(10.0f, 0.0f, 2.0f),
            new Vector3(11.0f, 0.0f, 2.0f),
            new Vector3(12.0f, 0.0f, 2.0f),
            new Vector3(-1.0f, 0.0f, 3.0f),
            new Vector3(0.0f, 0.0f, 3.0f),
            new Vector3(1.0f, 0.0f, 3.0f),
            new Vector3(2.0f, 0.0f, 3.0f),
            new Vector3(3.0f, 0.0f, 3.0f),
            new Vector3(4.0f, 0.0f, 3.0f),
            new Vector3(5.0f, 0.0f, 3.0f),
            new Vector3(6.0f, 0.0f, 3.0f),
            new Vector3(7.0f, 0.0f, 3.0f),
            new Vector3(8.0f, 0.0f, 3.0f),
            new Vector3(9.0f, 0.0f, 3.0f),
            new Vector3(10.0f, 0.0f, 3.0f),
            new Vector3(11.0f, 0.0f, 3.0f),
            new Vector3(12.0f, 0.0f, 3.0f),//3
            new Vector3(-1.0f, 0.0f, 4.0f),
            new Vector3(0.0f, 0.0f, 4.0f),
            new Vector3(1.0f, 0.0f, 4.0f),
            new Vector3(2.0f, 0.0f, 4.0f),
            new Vector3(3.0f, 0.0f, 4.0f),
            new Vector3(4.0f, 0.0f, 4.0f),
            new Vector3(5.0f, 0.0f, 4.0f),
            new Vector3(6.0f, 0.0f, 4.0f),
            new Vector3(7.0f, 0.0f, 4.0f),
            new Vector3(8.0f, 0.0f, 4.0f),
            new Vector3(9.0f, 0.0f, 4.0f),
            new Vector3(10.0f, 0.0f, 4.0f),
            new Vector3(11.0f, 0.0f, 4.0f),
            new Vector3(12.0f, 0.0f, 4.0f),//4
            new Vector3(-1.0f, 0.0f, 5.0f),
            new Vector3(0.0f, 0.0f, 5.0f),
            new Vector3(1.0f, 0.0f, 5.0f),
            new Vector3(2.0f, 0.0f, 5.0f),
            new Vector3(3.0f, 0.0f, 5.0f),
            new Vector3(4.0f, 0.0f, 5.0f),
            new Vector3(5.0f, 0.0f, 5.0f),
            new Vector3(6.0f, 0.0f, 5.0f),
            new Vector3(7.0f, 0.0f, 5.0f),
            new Vector3(8.0f, 0.0f, 5.0f),
            new Vector3(9.0f, 0.0f, 5.0f),
            new Vector3(10.0f, 0.0f, 5.0f),
            new Vector3(11.0f, 0.0f, 5.0f),
            new Vector3(12.0f, 0.0f, 5.0f),//5
            new Vector3(-1.0f, 0.0f, 6.0f),
            new Vector3(0.0f, 0.0f, 6.0f),
            new Vector3(1.0f, 0.0f, 6.0f),
            new Vector3(2.0f, 0.0f, 6.0f),
            new Vector3(3.0f, 0.0f, 6.0f),
            new Vector3(4.0f, 0.0f, 6.0f),
            new Vector3(5.0f, 0.0f, 6.0f),
            new Vector3(6.0f, 0.0f, 6.0f),
            new Vector3(7.0f, 0.0f, 6.0f),
            new Vector3(8.0f, 0.0f, 6.0f),
            new Vector3(9.0f, 0.0f, 6.0f),
            new Vector3(10.0f, 0.0f, 6.0f),
            new Vector3(11.0f, 0.0f, 6.0f),
            new Vector3(12.0f, 0.0f, 6.0f),
            new Vector3(-1.0f, 0.0f, 7.0f),
            new Vector3(0.0f, 0.0f, 7.0f),
            new Vector3(1.0f, 0.0f, 7.0f),
            new Vector3(2.0f, 0.0f, 7.0f),
            new Vector3(3.0f, 0.0f, 7.0f),
            new Vector3(4.0f, 0.0f, 7.0f),
            new Vector3(5.0f, 0.0f, 7.0f),
            new Vector3(6.0f, 0.0f, 7.0f),
            new Vector3(7.0f, 0.0f, 7.0f),
            new Vector3(8.0f, 0.0f, 7.0f),
            new Vector3(9.0f, 0.0f, 7.0f),
            new Vector3(10.0f, 0.0f, 7.0f),
            new Vector3(11.0f, 0.0f, 7.0f),
            new Vector3(12.0f, 0.0f, 7.0f),
            new Vector3(-1.0f, 0.0f, 8.0f),
            new Vector3(0.0f, 0.0f, 8.0f),
            new Vector3(-1.0f, 0.0f, 9.0f),
            new Vector3(0.0f, 0.0f, 9.0f),
            new Vector3(1.0f, 0.0f, 8.0f),
            new Vector3(2.0f, 0.0f, 8.0f),
            new Vector3(3.0f, 0.0f, 8.0f),
            new Vector3(4.0f, 0.0f, 8.0f),
            new Vector3(5.0f, 0.0f, 8.0f),
            new Vector3(6.0f, 0.0f, 8.0f),
            new Vector3(7.0f, 0.0f, 8.0f),
            new Vector3(8.0f, 0.0f, 8.0f),
            new Vector3(9.0f, 0.0f, 8.0f),
            new Vector3(10.0f, 0.0f, 8.0f),
            new Vector3(11.0f, 0.0f, 8.0f),
            new Vector3(12.0f, 0.0f, 8.0f),
            new Vector3(1.0f, 0.0f, 9.0f),
            new Vector3(2.0f, 0.0f, 9.0f),
            new Vector3(3.0f, 0.0f, 9.0f),
            new Vector3(4.0f, 0.0f, 9.0f),
            new Vector3(5.0f, 0.0f, 9.0f),
            new Vector3(6.0f, 0.0f, 9.0f),
            new Vector3(7.0f, 0.0f, 9.0f),
            new Vector3(8.0f, 0.0f, 9.0f),
            new Vector3(9.0f, 0.0f, 9.0f),
            new Vector3(10.0f, 0.0f, 9.0f),
            new Vector3(11.0f, 0.0f, 9.0f),
            new Vector3(12.0f, 0.0f, 9.0f),
            new Vector3(-1.0f, 0.0f, 10.0f),
            new Vector3(0.0f, 0.0f, 10.0f),
            new Vector3(1.0f, 0.0f, 10.0f),
            new Vector3(2.0f, 0.0f, 10.0f),
            new Vector3(3.0f, 0.0f, 10.0f),
            new Vector3(4.0f, 0.0f, 10.0f),
            new Vector3(5.0f, 0.0f, 10.0f),
            new Vector3(6.0f, 0.0f, 10.0f),
            new Vector3(7.0f, 0.0f, 10.0f),
            new Vector3(8.0f, 0.0f, 10.0f),
            new Vector3(9.0f, 0.0f, 10.0f),
            new Vector3(10.0f, 0.0f, 10.0f),
            new Vector3(11.0f, 0.0f, 10.0f),
            new Vector3(12.0f, 0.0f, 10.0f),
            new Vector3(1.0f, 0.0f, 11.0f),
            new Vector3(2.0f, 0.0f, 11.0f),
            new Vector3(3.0f, 0.0f, 11.0f),
            new Vector3(4.0f, 0.0f, 11.0f),
            new Vector3(5.0f, 0.0f, 11.0f),
            new Vector3(6.0f, 0.0f, 11.0f),
            new Vector3(7.0f, 0.0f, 11.0f),
            new Vector3(8.0f, 0.0f, 11.0f),
            new Vector3(9.0f, 0.0f, 11.0f),
            new Vector3(10.0f, 0.0f, 11.0f),
            new Vector3(11.0f, 0.0f, 11.0f),
            new Vector3(12.0f, 0.0f, 11.0f),
            new Vector3(1.0f, 0.0f, 12.0f),
            new Vector3(2.0f, 0.0f, 12.0f),
            new Vector3(3.0f, 0.0f, 12.0f),
            new Vector3(4.0f, 0.0f, 12.0f),
            new Vector3(5.0f, 0.0f, 12.0f),
            new Vector3(6.0f, 0.0f, 12.0f),
            new Vector3(7.0f, 0.0f, 12.0f),
            new Vector3(8.0f, 0.0f, 12.0f),
            new Vector3(9.0f, 0.0f, 12.0f),
            new Vector3(10.0f, 0.0f, 12.0f),
            new Vector3(11.0f, 0.0f, 12.0f),
            new Vector3(12.0f, 0.0f, 12.0f),
            new Vector3(-1.0f, 0.0f, 11.0f),
            new Vector3(0.0f, 0.0f, 11.0f),
            new Vector3(-1.0f, 0.0f, 12.0f),
            new Vector3(0.0f, 0.0f, 12.0f),//petak sawah
            new Vector3(-2.0f, 0.0f, 1.0f),//pojokan
            new Vector3(-2.0f, 0.0f, 2.0f),
            new Vector3(-2.0f, 0.0f, 3.0f),
            new Vector3(-2.0f, 0.0f, 4.0f),
            new Vector3(-2.0f, 0.0f, 5.0f),
            new Vector3(-2.0f, 0.0f, 6.0f),
            new Vector3(-2.0f, 0.0f, 7.0f),
            new Vector3(-2.0f, 0.0f, 8.0f),
            new Vector3(-2.0f, 0.0f, 9.0f),
            new Vector3(-2.0f, 0.0f, 10.0f),
            new Vector3(-2.0f, 0.0f, 11.0f),
            new Vector3(-2.0f, 0.0f, 12.0f),
            new Vector3(13.0f, 0.0f, 1.0f),
            new Vector3(13.0f, 0.0f, 2.0f),
            new Vector3(13.0f, 0.0f, 3.0f),
            new Vector3(13.0f, 0.0f, 4.0f),
            new Vector3(13.0f, 0.0f, 5.0f),
            new Vector3(13.0f, 0.0f, 6.0f),
            new Vector3(13.0f, 0.0f, 7.0f),
            new Vector3(13.0f, 0.0f, 8.0f),
            new Vector3(13.0f, 0.0f, 9.0f),
            new Vector3(13.0f, 0.0f, 10.0f),
            new Vector3(13.0f, 0.0f, 11.0f),
            new Vector3(13.0f, 0.0f, 12.0f),//end pojokan
            new Vector3(14.0f, 0.0f, 1.0f),
            new Vector3(14.0f, 0.0f, 2.0f),
            new Vector3(14.0f, 0.0f, 3.0f),
            new Vector3(14.0f, 0.0f, 4.0f),
            new Vector3(14.0f, 0.0f, 5.0f),
            new Vector3(14.0f, 0.0f, 6.0f),
            new Vector3(14.0f, 0.0f, 7.0f),
            new Vector3(14.0f, 0.0f, 8.0f),
            new Vector3(14.0f, 0.0f, 9.0f),
            new Vector3(14.0f, 0.0f, 10.0f),
            new Vector3(14.0f, 0.0f, 11.0f),
            new Vector3(14.0f, 0.0f, 12.0f),
        };

        private readonly Vector3[] _rumputgelap = {
        new Vector3(-2.0f, 1.0f, 1.0f),
        new Vector3(-2.0f, 1.0f, 2.0f),
        new Vector3(-2.0f, 1.0f, 3.0f),
        new Vector3(-2.0f, 1.0f, 4.0f),
        new Vector3(-2.0f, 1.0f, 5.0f),
        new Vector3(-2.0f, 1.0f, 6.0f),
        new Vector3(-2.0f, 1.0f, 7.0f),
        new Vector3(-2.0f, 1.0f, 8.0f),
        new Vector3(-2.0f, 1.0f, 9.0f),
        new Vector3(-2.0f, 1.0f, 10.0f),
        new Vector3(-2.0f, 1.0f, 11.0f),
        new Vector3(-2.0f, 1.0f, 12.0f),//batas sawah kiri

        new Vector3(14.0f, 1.0f, 1.0f),
        new Vector3(14.0f, 1.0f, 2.0f),
        new Vector3(14.0f, 1.0f, 3.0f),
        new Vector3(14.0f, 1.0f, 4.0f),
        new Vector3(14.0f, 1.0f, 5.0f),
        new Vector3(14.0f, 1.0f, 6.0f),
        new Vector3(14.0f, 1.0f, 7.0f),
        new Vector3(14.0f, 1.0f, 8.0f),
        new Vector3(14.0f, 1.0f, 9.0f),
        new Vector3(14.0f, 1.0f, 10.0f),
        new Vector3(14.0f, 1.0f, 11.0f),
        new Vector3(14.0f, 1.0f, 12.0f),//batas sawah kanan
        new Vector3(13.0f, 1.0f, 12.0f),
        new Vector3(12.0f, 1.0f, 12.0f),
        new Vector3(11.0f, 1.0f, 12.0f),
        new Vector3(10.0f, 1.0f, 12.0f),
        new Vector3(9.0f, 1.0f, 12.0f),
        new Vector3(8.0f, 1.0f, 12.0f),
        new Vector3(7.0f, 1.0f, 12.0f),
        new Vector3(6.0f, 1.0f, 12.0f),
        new Vector3(5.0f, 1.0f, 12.0f),
        new Vector3(4.0f, 1.0f, 12.0f),

        new Vector3(8.0f, 1.0f, 11.0f),//maze tgh
        new Vector3(8.0f, 1.0f, 10.0f),
        new Vector3(8.0f, 1.0f, 9.0f),
        new Vector3(8.0f, 1.0f, 8.0f),
        new Vector3(8.0f, 1.0f, 7.0f),
        new Vector3(8.0f, 1.0f, 6.0f),
        new Vector3(10.0f, 1.0f, 7.0f),
        new Vector3(9.0f, 1.0f, 7.0f),
        new Vector3(7.0f, 1.0f, 7.0f),
        new Vector3(6.0f, 1.0f, 7.0f),
        new Vector3(6.0f, 1.0f, 6.0f),
        new Vector3(6.0f, 1.0f, 5.0f),
        new Vector3(6.0f, 1.0f, 4.0f),
        new Vector3(7.0f, 1.0f, 4.0f),
        new Vector3(8.0f, 1.0f, 4.0f),
        new Vector3(9.0f, 1.0f, 4.0f),
        new Vector3(10.0f, 1.0f, 4.0f),

        new Vector3(1.0f, 1.0f, 1.0f),//rumput utara
        new Vector3(2.0f, 1.0f, 1.0f),
        new Vector3(3.0f, 1.0f, 1.0f),
        new Vector3(4.0f, 1.0f, 1.0f),
        new Vector3(5.0f, 1.0f, 1.0f),
        new Vector3(6.0f, 1.0f, 1.0f),
        new Vector3(7.0f, 1.0f, 1.0f),
        new Vector3(8.0f, 1.0f, 1.0f),
        new Vector3(9.0f, 1.0f, 1.0f),
        new Vector3(10.0f, 1.0f, 1.0f),
        new Vector3(11.0f, 1.0f, 1.0f),
        new Vector3(12.0f, 1.0f, 1.0f),//endof rumput utara

        new Vector3(1.0f,1.0f,2.0f),//gerbang kiri
        new Vector3(1.0f, 1.0f, 3.0f),
        new Vector3(1.0f, 1.0f, 4.0f),
        new Vector3(1.0f, 1.0f, 5.0f),
        new Vector3(1.0f, 1.0f, 6.0f),
        new Vector3(1.0f, 1.0f, 7.0f),
        };

        private readonly Vector3[] danau = {
        new Vector3(1.0f, 0.0f, -1.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, -2.0f),
        new Vector3(1.0f, 0.0f, -3.0f),
        new Vector3(1.0f, 0.0f, -4.0f),
        new Vector3(1.0f, 0.0f, -5.0f),
        new Vector3(2.0f, 0.0f, 0.0f),
        new Vector3(2.0f, 0.0f, -1.0f),
        new Vector3(2.0f, 0.0f, -2.0f),
        new Vector3(2.0f, 0.0f, -3.0f),
        new Vector3(2.0f, 0.0f, -4.0f),
        new Vector3(2.0f, 0.0f, -5.0f),
        new Vector3(3.0f, 0.0f, 0.0f),
        new Vector3(3.0f, 0.0f, -1.0f),
        new Vector3(3.0f, 0.0f, -2.0f),
        new Vector3(3.0f, 0.0f, -3.0f),
        new Vector3(3.0f, 0.0f, -4.0f),
        new Vector3(3.0f, 0.0f, -5.0f),

        new Vector3(4.0f, 0.0f, 0.0f),
        new Vector3(4.0f, 0.0f, -1.0f),
        new Vector3(4.0f, 0.0f, -2.0f),
        new Vector3(4.0f, 0.0f, -3.0f),
        new Vector3(4.0f, 0.0f, -4.0f),
        new Vector3(4.0f, 0.0f, -5.0f),

        new Vector3(5.0f, 0.0f, 0.0f),
        new Vector3(5.0f, 0.0f, -1.0f),
        new Vector3(5.0f, 0.0f, -2.0f),
        new Vector3(5.0f, 0.0f, -3.0f),
        new Vector3(5.0f, 0.0f, -4.0f),
        new Vector3(5.0f, 0.0f, -5.0f),
        new Vector3(6.0f, 0.0f, 0.0f),
        new Vector3(6.0f, 0.0f, -1.0f),
        new Vector3(6.0f, 0.0f, -2.0f),
        new Vector3(6.0f, 0.0f, -3.0f),
        new Vector3(6.0f, 0.0f, -4.0f),
        new Vector3(6.0f, 0.0f, -5.0f),

        new Vector3(7.0f, 0.0f, 0.0f),
        new Vector3(7.0f, 0.0f, -1.0f),
        new Vector3(7.0f, 0.0f, -2.0f),
        new Vector3(7.0f, 0.0f, -3.0f),
        new Vector3(7.0f, 0.0f, -4.0f),
        new Vector3(7.0f, 0.0f, -5.0f),

        new Vector3(8.0f, 0.0f, 0.0f),
        new Vector3(8.0f, 0.0f, -1.0f),
        new Vector3(8.0f, 0.0f, -2.0f),
        new Vector3(8.0f, 0.0f, -3.0f),
        new Vector3(8.0f, 0.0f, -4.0f),
        new Vector3(8.0f, 0.0f, -5.0f),

        new Vector3(9.0f, 0.0f, 0.0f),
        new Vector3(9.0f, 0.0f, -1.0f),
        new Vector3(9.0f, 0.0f, -2.0f),
        new Vector3(9.0f, 0.0f, -3.0f),
        new Vector3(9.0f, 0.0f, -4.0f),
        new Vector3(9.0f, 0.0f, -5.0f),

        new Vector3(10.0f, 0.0f, 0.0f),
        new Vector3(10.0f, 0.0f, -1.0f),
        new Vector3(10.0f, 0.0f, -2.0f),
        new Vector3(10.0f, 0.0f, -3.0f),
        new Vector3(10.0f, 0.0f, -4.0f),
        new Vector3(10.0f, 0.0f, -5.0f),

        new Vector3(11.0f, 0.0f, 0.0f),
        new Vector3(11.0f, 0.0f, -1.0f),
        new Vector3(11.0f, 0.0f, -2.0f),
        new Vector3(11.0f, 0.0f, -3.0f),
        new Vector3(11.0f, 0.0f, -4.0f),
        new Vector3(11.0f, 0.0f, -5.0f),
        new Vector3(12.0f, 0.0f, 0.0f),
        new Vector3(12.0f, 0.0f, -1.0f),
        new Vector3(12.0f, 0.0f, -2.0f),
        new Vector3(12.0f, 0.0f, -3.0f),
        new Vector3(12.0f, 0.0f, -4.0f),
        new Vector3(12.0f, 0.0f, -5.0f),
        };

        private readonly Vector3[] _perhutani = {
        new Vector3(-2.0f, 2.0f, 1.0f),
        new Vector3(-2.0f, 2.0f, 2.0f),
        new Vector3(-2.0f, 2.0f, 3.0f),
        new Vector3(-2.0f, 2.0f, 4.0f),
        new Vector3(-2.0f, 2.0f, 5.0f),
        new Vector3(-2.0f, 2.0f, 6.0f),
        new Vector3(-2.0f, 2.0f, 7.0f),
        new Vector3(-2.0f, 2.0f, 8.0f),
        new Vector3(-2.0f, 2.0f, 9.0f),
        new Vector3(-2.0f, 2.0f, 10.0f),
        new Vector3(-2.0f, 2.0f, 11.0f),
        new Vector3(-2.0f, 2.0f, 12.0f),

        new Vector3(-2.0f, 3.0f, 1.0f),
        new Vector3(-2.0f, 3.0f, 2.0f),
        new Vector3(-2.0f, 3.0f, 3.0f),
        new Vector3(-2.0f, 3.0f, 4.0f),
        new Vector3(-2.0f, 3.0f, 5.0f),
        new Vector3(-2.0f, 3.0f, 6.0f),
        new Vector3(-2.0f, 3.0f, 7.0f),
        new Vector3(-2.0f, 3.0f, 8.0f),
        new Vector3(-2.0f, 3.0f, 9.0f),
        new Vector3(-2.0f, 3.0f, 10.0f),
        new Vector3(-2.0f, 3.0f, 11.0f),
        new Vector3(-2.0f, 3.0f, 12.0f),

        new Vector3(-2.0f, 4.0f, 1.0f),
        new Vector3(-2.0f, 4.0f, 2.0f),
        new Vector3(-2.0f, 4.0f, 3.0f),
        new Vector3(-2.0f, 4.0f, 4.0f),
        new Vector3(-2.0f, 4.0f, 5.0f),
        new Vector3(-2.0f, 4.0f, 6.0f),
        new Vector3(-2.0f, 4.0f, 7.0f),
        new Vector3(-2.0f, 4.0f, 8.0f),
        new Vector3(-2.0f, 4.0f, 9.0f),
        new Vector3(-2.0f, 4.0f, 10.0f),
        new Vector3(-2.0f, 4.0f, 11.0f),
        new Vector3(-2.0f, 4.0f, 12.0f), 

        new Vector3(14.0f, 1.0f, 1.0f),
        new Vector3(14.0f, 1.0f, 2.0f),
        new Vector3(14.0f, 1.0f, 3.0f),
        new Vector3(14.0f, 1.0f, 4.0f),
        new Vector3(14.0f, 1.0f, 5.0f),
        new Vector3(14.0f, 1.0f, 6.0f),
        new Vector3(14.0f, 1.0f, 7.0f),
        new Vector3(14.0f, 1.0f, 8.0f),
        new Vector3(14.0f, 1.0f, 9.0f),
        new Vector3(14.0f, 1.0f, 10.0f),
        new Vector3(14.0f, 1.0f, 11.0f),
        new Vector3(14.0f, 1.0f, 12.0f),

        new Vector3(14.0f, 2.0f, 1.0f),
        new Vector3(14.0f, 2.0f, 2.0f),
        new Vector3(14.0f, 2.0f, 3.0f),
        new Vector3(14.0f, 2.0f, 4.0f),
        new Vector3(14.0f, 2.0f, 5.0f),
        new Vector3(14.0f, 2.0f, 6.0f),
        new Vector3(14.0f, 2.0f, 7.0f),
        new Vector3(14.0f, 2.0f, 8.0f),
        new Vector3(14.0f, 2.0f, 9.0f),
        new Vector3(14.0f, 2.0f, 10.0f),
        new Vector3(14.0f, 2.0f, 11.0f),
        new Vector3(14.0f, 2.0f, 12.0f),
    
        new Vector3(14.0f, 3.0f, 1.0f),
        new Vector3(14.0f, 3.0f, 2.0f),
        new Vector3(14.0f, 3.0f, 3.0f),
        new Vector3(14.0f, 3.0f, 4.0f),
        new Vector3(14.0f, 3.0f, 5.0f),
        new Vector3(14.0f, 3.0f, 6.0f),
        new Vector3(14.0f, 3.0f, 7.0f),
        new Vector3(14.0f, 3.0f, 8.0f),
        new Vector3(14.0f, 3.0f, 9.0f),
        new Vector3(14.0f, 3.0f, 10.0f),
        new Vector3(14.0f, 3.0f, 11.0f),
        new Vector3(14.0f, 3.0f, 12.0f),

        new Vector3(14.0f, 4.0f, 1.0f),
        new Vector3(14.0f, 4.0f, 2.0f),
        new Vector3(14.0f, 4.0f, 3.0f),
        new Vector3(14.0f, 4.0f, 4.0f),
        new Vector3(14.0f, 4.0f, 5.0f),
        new Vector3(14.0f, 4.0f, 6.0f),
        new Vector3(14.0f, 4.0f, 7.0f),
        new Vector3(14.0f, 4.0f, 8.0f),
        new Vector3(14.0f, 4.0f, 9.0f),
        new Vector3(14.0f, 4.0f, 10.0f),
        new Vector3(14.0f, 4.0f, 11.0f),
        new Vector3(14.0f, 4.0f, 12.0f),

        new Vector3(14.0f, 1.0f, 1.0f),
        new Vector3(14.0f, 1.0f, 2.0f),
        new Vector3(14.0f, 1.0f, 3.0f),
        new Vector3(14.0f, 1.0f, 4.0f),
        new Vector3(14.0f, 1.0f, 5.0f),
        new Vector3(14.0f, 1.0f, 6.0f),
        new Vector3(14.0f, 1.0f, 7.0f),
        new Vector3(14.0f, 1.0f, 8.0f),
        new Vector3(14.0f, 1.0f, 9.0f),
        new Vector3(14.0f, 1.0f, 10.0f),
        new Vector3(14.0f, 1.0f, 11.0f),
        new Vector3(14.0f, 1.0f, 12.0f),
    
        new Vector3(14.0f, 2.0f, 1.0f),
        new Vector3(14.0f, 2.0f, 2.0f),
        new Vector3(14.0f, 2.0f, 3.0f),
        new Vector3(14.0f, 2.0f, 4.0f),
        new Vector3(14.0f, 2.0f, 5.0f),
        new Vector3(14.0f, 2.0f, 6.0f),
        new Vector3(14.0f, 2.0f, 7.0f),
        new Vector3(14.0f, 2.0f, 8.0f),
        new Vector3(14.0f, 2.0f, 9.0f),
        new Vector3(14.0f, 2.0f, 10.0f),
        new Vector3(14.0f, 2.0f, 11.0f),
        new Vector3(14.0f, 2.0f, 12.0f),

        new Vector3(14.0f, 3.0f, 1.0f),
        new Vector3(14.0f, 3.0f, 2.0f),
        new Vector3(14.0f, 3.0f, 3.0f),
        new Vector3(14.0f, 3.0f, 4.0f),
        new Vector3(14.0f, 3.0f, 5.0f),
        new Vector3(14.0f, 3.0f, 6.0f),
        new Vector3(14.0f, 3.0f, 7.0f),
        new Vector3(14.0f, 3.0f, 8.0f),
        new Vector3(14.0f, 3.0f, 9.0f),
        new Vector3(14.0f, 3.0f, 10.0f),
        new Vector3(14.0f, 3.0f, 11.0f),
        new Vector3(14.0f, 3.0f, 12.0f),
    
        new Vector3(14.0f, 4.0f, 1.0f),
        new Vector3(14.0f, 4.0f, 2.0f),
        new Vector3(14.0f, 4.0f, 3.0f),
        new Vector3(14.0f, 4.0f, 4.0f),
        new Vector3(14.0f, 4.0f, 5.0f),
        new Vector3(14.0f, 4.0f, 6.0f),
        new Vector3(14.0f, 4.0f, 7.0f),
        new Vector3(14.0f, 4.0f, 8.0f),
        new Vector3(14.0f, 4.0f, 9.0f),
        new Vector3(14.0f, 4.0f, 10.0f),
        new Vector3(14.0f, 4.0f, 11.0f),
        new Vector3(14.0f, 4.0f, 12.0f),
    
        
        };

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            _object3d[0] = new Asset3d();
            _object3d[1] = new Asset3d();
            _object3d[2] = new Asset3d();
            _object3d[3] = new Asset3d();
            _object3d[4] = new Asset3d(); //danau
            _object3d[5] = new Asset3d(); //hutan
        }

        float Cx = -0.5f;
        float Cy = 2.5f;
        float Cz = 0f;

        protected override void OnLoad()
        {
            base.OnLoad();

            //Ganti warna background
            GL.ClearColor(0.5f, 0.5f, 1.0f, 1);
            //GL.ClearColor(0.0f, 0.0f, 0.0f, 1); MALAM
            GL.Enable(EnableCap.DepthTest);
            CursorGrabbed = true;

            //_object3d[0].createBoxVertices(0.0f, 0.0f, -1.0f, 1.0f);
            //_object3d[0].addChildCubes(0.3f, 0.3f, 0.0f, 0.25f,"");

            _object3d[0].load2(Size.X, Size.Y, "cobblestone.png", "container2_specular.png");
            _object3d[1].load2(Size.X, Size.Y, "container2.png", "container2_specular.png");
            _object3d[2].load2(Size.X, Size.Y, "azalea_leaves.png", "container2_specular.png");
            _object3d[3].load2(Size.X, Size.Y, "mossy_cobblestone.png", "container2_specular.png");
            _object3d[4].load2(Size.X, Size.Y, "stripped_warped_stem.png", "container2_specular.png");
            _object3d[5].load2(Size.X, Size.Y, "flowering_azalea_leaves.png", "container2_specular.png");
            _camera = new Camera(new Vector3(Cx,Cy,Cz), Size.X / Size.Y);

            // inisialisai light object
            _vertexBufferLightObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferLightObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            _lampShader = new Shader(Source + "/Shaders/shader.vert", Source + "/Shaders/shader.frag");
            _vaoLamp = GL.GenVertexArray();
            GL.BindVertexArray(_vaoLamp);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

           GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); //agar frame tidak bertumpuk saat ganti frame

            for (int i = 0; i < _cubePositions.Length; i++)
            {
                _object3d[0].render(4, _camera.GetViewMatrix(), _camera.GetProjectionMatrix(), _lightPos, _camera.Position, _camera.Front, _pointLightPositions, _cubePositions[i], i);
            }

            for (int i = 0; i < _cubePos2.Length; i++)
            {
                _object3d[1].render(4, _camera.GetViewMatrix(), _camera.GetProjectionMatrix(), _lightPos, _camera.Position, _camera.Front, _pointLightPositions, _cubePos2[i], i);
            }

            for (int i = 0; i < _cuberumput.Length; i++)
            {
                _object3d[2].render(4, _camera.GetViewMatrix(), _camera.GetProjectionMatrix(), _lightPos, _camera.Position, _camera.Front, _pointLightPositions, _cuberumput[i], i);
            }
            for (int i = 0; i < _rumputgelap.Length; i++)
            {
                _object3d[3].render(4, _camera.GetViewMatrix(), _camera.GetProjectionMatrix(), _lightPos, _camera.Position, _camera.Front, _pointLightPositions, _rumputgelap[i], i);
            }

            for (int i = 0; i < danau.Length; i++)
            {
                _object3d[4].render(4, _camera.GetViewMatrix(), _camera.GetProjectionMatrix(), _lightPos, _camera.Position, _camera.Front, _pointLightPositions, danau[i], i);
            }

            for (int i = 0; i < _perhutani.Length; i++)
            {
                _object3d[5].render(4, _camera.GetViewMatrix(), _camera.GetProjectionMatrix(), _lightPos, _camera.Position, _camera.Front, _pointLightPositions, _perhutani[i], i);
            }

            //lighthing
            _lampShader.Use();
            
           
            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            for (int i = 0; i < _pointLightPositions.Length; i++)
            {
                Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
                lampMatrix = lampMatrix * Matrix4.CreateTranslation(_pointLightPositions[i]);
                _lampShader.SetMatrix4("transform", lampMatrix);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
            }

            GL.BindVertexArray(_vaoLamp);
            

            //_object3d[0].resetEuler();
            SwapBuffers();
        }
        int counter = 0;
        //KEYBOARD FUNCTION

        float R = 0.5f;
        float G = 0.5f;
        float B = 1.0f;

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            var input = KeyboardState;
            float cameraspeed = 0.25f;
            var sensitivty = 0.98f;
            // 10000 - 500 = 9500 - 100

            if (counter < 250)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
                _camera.Position += _camera.Up * cameraspeed * 5 * (float)args.Time;

                R = R - 0.0005f;
                G = G - 0.0005f;
                B = B - 0.00085f;

                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("naik");
            }
            else if (counter < 400)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;

                R = R - 0.0005f;
                G = G - 0.0005f;
                B = B - 0.00085f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("jalan");
            }
            else if (counter < 1250)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
                _camera.Position += _camera.Up * cameraspeed * 5 * (float)args.Time;

                R = R - 0.0005f;
                G = G - 0.0005f;
                B = B - 0.00085f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("naik");
            }
            else if (counter < 1400)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;

                R = R - 0.0005f;
                G = G - 0.0005f;
                B = B - 0.00085f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("jalan");
            }
            else if (counter < 1401)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Yaw += 90f;
                Console.WriteLine("muter");
                R = R - 0.0005f;
                G = G - 0.0005f;
                B = B - 0.00085f;
                GL.ClearColor(R, G, B, 1);

            }
            else if (counter < 1421)
            {
                _camera.Position += _camera.Right * cameraspeed * 3 * (float)args.Time;
                _camera.Position -= _camera.Up * cameraspeed * 5 * (float)args.Time;
                Console.WriteLine("kalibrasi");
                R = R - 0.0005f;
                G = G - 0.0005f;
                B = B - 0.00085f;
                GL.ClearColor(R, G, B, 1);
            }
            // tangga
            else if (counter < 3250)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;

                R = R - 0.0005f;
                G = G - 0.0005f;
                B = B - 0.00085f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("jalan");
            }
            // jembatan selesai
            // mulai ke oren
            else if (counter < 3251)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Yaw += 90f;
                Console.WriteLine("muter");
                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
            }
            else if (counter < 3252)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Pitch -= 15f;
                Console.WriteLine("lihat atas");
                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
            }
            else if (counter < 3272)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;

                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("jalan");
            }
            else if (counter < 4102)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
                _camera.Position -= _camera.Up * cameraspeed * 3f * (float)args.Time;

                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("turun");
            }
            else if (counter < 4103)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Pitch += 15f;
                Console.WriteLine("lihat atas");
                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
            }

            //////////////////////////////////////////////////
            else if (counter < 4353)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;

                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("jalan");
            }
            else if (counter < 4354)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Pitch -= 15f;
                Console.WriteLine("lihat bawah");
                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
            }
            else if (counter < 4704)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
                _camera.Position -= _camera.Up * cameraspeed * 3f * (float)args.Time;

                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("turun");
            }
            else if (counter < 4705)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Pitch += 15f;
                Console.WriteLine("lihat atas");
                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
            }
            else if (counter < 5004)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;

                R = R + 0.00117f;
                G = G + 0.00076f;
                B = B + 0.000352f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("jalan");
            }
            else if (counter < 5005)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;

                R = 1f;
                G = 0.65f;
                B = 0.3f;
                GL.ClearColor(R, G, B, 1);
                Console.WriteLine("jalan");
            }

            else if (counter < 5006)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Yaw += 90f;
                Console.WriteLine("muter");
                R = R - 0.00016f;
                G = G - 0.000046f;
                B = B + 0.000218f;
                GL.ClearColor(R, G, B, 1);

            }
            else if (counter < 6106)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
                R = R - 0.00016f;
                G = G - 0.000046f;
                B = B + 0.000218f;
                GL.ClearColor(R, G, B, 1);

                Console.WriteLine("jalan");
            }
            else if (counter < 6107)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Yaw -= 90f;
                Console.WriteLine("muter");
                R = R - 0.00016f;
                G = G - 0.000046f;
                B = B + 0.000218f;
                GL.ClearColor(R, G, B, 1);

            }
            else if (counter < 6807)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
                R = R - 0.00016f;
                G = G - 0.000046f;
                B = B + 0.000218f;
                GL.ClearColor(R, G, B, 1);

                Console.WriteLine("jalan");
            }
            else if (counter < 6808)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Yaw += 90f;
                Console.WriteLine("muter");
                R = R - 0.00016f;
                G = G - 0.000046f;
                B = B + 0.000218f;
                GL.ClearColor(R, G, B, 1);
            }
            else if (counter < 7208)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
                R = R - 0.00016f;
                G = G - 0.000046f;
                B = B + 0.000218f;
                GL.ClearColor(R, G, B, 1);

                Console.WriteLine("jalan");
            }
            else if (counter < 7209)
            {
                Console.WriteLine(counter);
                //var deltaX =  -1f - _lastPos.X;
                _camera.Yaw += 90f;
                Console.WriteLine("muter");
                R = R - 0.00016f;
                G = G - 0.000046f;
                B = B + 0.000218f;
                GL.ClearColor(R, G, B, 1);
            }
            else if (counter < 8209)
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
                R = R - 0.00016f;
                G = G - 0.000046f;
                B = B + 0.000218f;
                GL.ClearColor(R, G, B, 1);

                Console.WriteLine("jalan");
            }
            else if (counter < 8210)
            {
                float Cx = -0.5f;
                float Cy = 2.5f;
                float Cz = 0f;
                _camera = new Camera(new Vector3(Cx, Cy, Cz), Size.X / Size.Y);
            }
            else
            {
                counter = -1;
            }
            counter++;

            //exit with escape keyboard
            if (input.IsKeyDown(Keys.Escape))
            {
                Close();
            };

            
            if (input.IsKeyDown(Keys.Up))
            {
                _camera.Position += _camera.Front * cameraspeed * 5 * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.Down))
            {
                _camera.Position -= _camera.Front * cameraspeed * 5 * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.Left))
            {
                _camera.Position -= _camera.Right * cameraspeed * 5 * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.Right))
            {
                _camera.Position += _camera.Right * cameraspeed * 5 * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * (cameraspeed*5.0f) * (float)args.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraspeed * (float)args.Time;
            }


            var mouse = MouseState;
            
            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _camera.Yaw += deltaX * sensitivty;
                _camera.Pitch -= deltaY * sensitivty;
            }
            if(KeyboardState.IsKeyDown(Keys.N))
            {
                var axis = new Vector3(0,0.05f,0);
                _camera.Position -= _objecPost;
                _camera.Yaw -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position, generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                _camera.Position += _objecPost;
                _camera._front = -Vector3.Normalize(_camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                var axis = new Vector3(0, 0.05f, 0);
                _camera.Position -= _objecPost;
                _camera.Yaw += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position, generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                _camera.Position += _objecPost;
                _camera._front = -Vector3.Normalize(_camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(0.05f, 0, 0);
                _camera.Position -= _objecPost;
                _camera.Pitch += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position, generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                _camera.Position += _objecPost;
                _camera._front = -Vector3.Normalize(_camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(0.05f, 0, 0);
                _camera.Position -= _objecPost;
                _camera.Pitch += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position, generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                _camera.Position += _objecPost;
                _camera._front = -Vector3.Normalize(_camera.Position - _objecPost);
            }

        }

        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }

        //Untuk Resize Window
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            _camera.AspectRatio = Size.X / (float)Size.Y;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                float _x = (MousePosition.X - Size.X / 2) / (Size.X / 2);
                float _y = -(MousePosition.Y - Size.Y / 2) / (Size.Y / 2);

            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            _camera.Fov -= e.OffsetY;
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Keys.A)
            {
                _object3d[0].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[1], 1);
            }
            if (e.Key == Keys.D)
            {
                _object3d[0].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[1], -1);
            }
            if (e.Key == Keys.W)
            {
                //_object3d[0].Child[0].rotate(_object3d[0].Child[0]._centerPosition,_object3d[0].Child[0]._euler[0],1);
                _object3d[0].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[0], 1);
            }
            if (e.Key == Keys.S)
            {
                //_object3d[0].Child[0].rotate(_object3d[0].Child[0]._centerPosition,_object3d[0].Child[0]._euler[0],1);
                _object3d[0].rotate(_object3d[0]._centerPosition, _object3d[0]._euler[0], -1);
            }
            


        }
    }

}
