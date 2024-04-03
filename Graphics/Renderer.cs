﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tao.OpenGl;

//include GLM library
using GlmNet;


using System.IO;
using System.Diagnostics;

namespace Graphics
{
    class Renderer
    {
        Shader sh;
        
        uint square1BufferID;
        uint square2BufferID;
        uint cubeID;
        uint xyzAxesBufferID;

        //3D Drawing
        mat4 ModelMatrix;
        mat4 ViewMatrix;
        mat4 ProjectionMatrix;
        
        int ShaderModelMatrixID;
        int ShaderViewMatrixID;
        int ShaderProjectionMatrixID;

        const float rotationSpeed = 1f;
        float rotationAngle = 0;

        public float translationX=0, 
                     translationY=0, 
                     translationZ=0;

        Stopwatch timer = Stopwatch.StartNew();

        vec3 square1Center;
        vec3 square2Center;

        public void Initialize()
        {
            string projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            sh = new Shader(projectPath + "\\Shaders\\SimpleVertexShader.vertexshader", projectPath + "\\Shaders\\SimpleFragmentShader.fragmentshader");
            Gl.glClearColor(0, 0, 0.4f, 1);
            
            float[] square1Vertices = { 
		        // T1
		        -10.0f,  -10.0f, 0.0f, 1.0f, 0.0f, 1.0f,
	            -10.0f, 10.0f, 0.0f, 1.0f, 1.0f, 0.0f,
                10.0f,  10.0f, 0.0f, 0.0f, 1.0f, 0.0f,  //B
		        10.0f,  -10.0f, 0.0f, 0.0f, 1.0f, 1.0f,  //B


            }; // Triangle Center = (10, 7, -5)
            
            square1Center = new vec3(0.0f, 0.0f, 0.0f);

            float[] square2Vertices = { 
		        // T1
		        -10.0f,  -10.0f, -20.0f, 1.0f, 0.0f, 1.0f,
                -10.0f, 10.0f, -20.0f, 1.0f, 1.0f, 0.0f,
                10.0f,  10.0f, -20.0f, 0.0f, 1.0f, 0.0f,  //B
		        10.0f,  -10.0f, -20.0f, 0.0f, 1.0f, 1.0f,  //B


            }; // Triangle Center = (10, 7, -5)

            square2Center = new vec3(0.0f, 0.0f, 0.0f);

            float[] cubeVertices =
            {
                10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, 10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, 10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                -10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, 10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,

                10.0f, -10.0f, -10.0f,
                0.5f, 0.4f, 0.7f,
                            };

            float[] xyzAxesVertices = {
		        //x
		        -100.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, //R
		        100.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, //R
		        //y
	            0.0f, -100.0f, 0.0f, 0.0f, 1.0f, 0.0f, //G
		        0.0f, 100.0f, 0.0f, 0.0f, 1.0f, 0.0f, //G
		        //z
	            0.0f, 0.0f, -100.0f, 0.0f, 0.0f, 1.0f,  //B
		        0.0f, 0.0f, 100.0f, 0.0f, 0.0f, 1.0f,  //B
            };


            square1BufferID = GPU.GenerateBuffer(square1Vertices);
            square2BufferID = GPU.GenerateBuffer(square2Vertices);
            cubeID = GPU.GenerateBuffer(cubeVertices);
            xyzAxesBufferID = GPU.GenerateBuffer(xyzAxesVertices);
            
            // View matrix 
            ViewMatrix = glm.lookAt(
                        new vec3(50, 50, 50), // Camera is at (0,5,5), in World Space
                        new vec3(0, 0, 0), // and looks at the origin
                        new vec3(0, 0, 1)  // Head is up (set to 0,-1,0 to look upside-down)
                );
            // Model Matrix Initialization
            ModelMatrix = new mat4(1);

            //ProjectionMatrix = glm.perspective(FOV, Width / Height, Near, Far);
            ProjectionMatrix = glm.perspective(45.0f, 4.0f / 3.0f, 0.1f, 100.0f);
            
            // Our MVP matrix which is a multiplication of our 3 matrices 
            sh.UseShader();


            //Get a handle for our "MVP" uniform (the holder we created in the vertex shader)
            ShaderModelMatrixID = Gl.glGetUniformLocation(sh.ID, "modelMatrix");
            ShaderViewMatrixID = Gl.glGetUniformLocation(sh.ID, "viewMatrix");
            ShaderProjectionMatrixID = Gl.glGetUniformLocation(sh.ID, "projectionMatrix");

            Gl.glUniformMatrix4fv(ShaderViewMatrixID, 1, Gl.GL_FALSE, ViewMatrix.to_array());
            Gl.glUniformMatrix4fv(ShaderProjectionMatrixID, 1, Gl.GL_FALSE, ProjectionMatrix.to_array());

            timer.Start();
        }

        public void Draw()
        {
            sh.UseShader();
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            #region XYZ axis
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, xyzAxesBufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, new mat4(1).to_array()); // Identity

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));
             
            Gl.glDrawArrays(Gl.GL_LINES, 0, 6);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);

            #endregion

            #region Animated Square 2
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, square2BufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion

            #region Animated Square1
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, square1BufferID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_POLYGON, 0, 4);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion
            #region Weird cube
            Gl.glBindBuffer(Gl.GL_ARRAY_BUFFER, cubeID);

            Gl.glUniformMatrix4fv(ShaderModelMatrixID, 1, Gl.GL_FALSE, ModelMatrix.to_array());

            Gl.glEnableVertexAttribArray(0);
            Gl.glVertexAttribPointer(0, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)0);
            Gl.glEnableVertexAttribArray(1);
            Gl.glVertexAttribPointer(1, 3, Gl.GL_FLOAT, Gl.GL_FALSE, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            Gl.glDrawArrays(Gl.GL_QUADS, 0, 30);

            Gl.glDisableVertexAttribArray(0);
            Gl.glDisableVertexAttribArray(1);
            #endregion



        }

        public void Update()
        {
            timer.Stop();
            var deltaTime = timer.ElapsedMilliseconds/1000.0f;
            rotationAngle += deltaTime * rotationSpeed;

            List<mat4> transformations = new List<mat4>();
            transformations.Add(glm.translate(new mat4(1), -1 * square1Center));
            transformations.Add(glm.rotate(rotationAngle, new vec3(0, 0, 1)));
            transformations.Add(glm.translate(new mat4(1),  square1Center));
            transformations.Add(glm.translate(new mat4(1), new vec3(translationX, translationY, translationZ)));

            ModelMatrix =  MathHelper.MultiplyMatrices(transformations);
            
            timer.Reset();
            timer.Start();
        }
        
        public void CleanUp()
        {
            sh.DestroyShader();
        }
    }
}
