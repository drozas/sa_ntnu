using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace task03
{
        
  
    public class helicopterOld
    {
        //Numero de cuadros que contiene la animacion
        public int NumeroDeCuadros;
        //ancho de cada cuadro se calcula dividiendo
        //la longitud sobre el numero de cuadros
        public int AnchoDeCuadro;

        public Texture2D Textura;
        //angulo en radianes
        public float Angulo;
        //nivel de profundidad del sprite 0f a 1f
        public float Profundidad;

        public Random r;

                //posicion del esprite como vector bidimencional
        public Vector2 Posicion;
        //el numero de cuadro actual
        public int CuadroActual;
        //el efecto del sprite
        //Reflejar horizontalmente o verticalmente
        public SpriteEffects Efecto;

        public Vector2 velocity;


        public helicopterOld()
        {
            this.NumeroDeCuadros = 0;
            this.AnchoDeCuadro = 130;
            this.Angulo = 0;
            this.Profundidad = 0f;
            this.r = new Random();
            this.Efecto = SpriteEffects.None;
            this.velocity = new Vector2(1, 1);
        }
        
    }
}
