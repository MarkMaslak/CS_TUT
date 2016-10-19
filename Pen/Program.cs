using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen
{
    class Program
    {

        static Pen pen;

        static void Main(string[] args)
        {
            var car = new Coche();
            car.NueumaticoCambiado += Objeto_AlgoCambiado;

            pen = new Pen();
            pen.CanaCambiada += Objeto_AlgoCambiado;
            

            for (int i = 0; i < 4; i++)
            {
                pen.Cana = i + 8;
                //car.CambioNeumatico();
            }

            Console.Read();

        }

       

        private static void Objeto_AlgoCambiado(object sender, EventArgs e)
        {
            if (e is PenEventArgs)
            {
                var arg = (PenEventArgs)e;

                //if (pen.Cana > 2)
                //    pen.Cana = 2;

                Console.WriteLine("Cana ha cambiado " + arg.Cana);
            }
            else
            {
                Console.WriteLine("Algo ha cambiado");
            }

        }
    }


    public static class Extensions
    {
        public static void Raise<T>(this System.EventHandler<T> evento, object sender, T args) where T : EventArgs
        {
            var handler = evento;

            if (handler != null)
                handler(sender, args);
        }
    }

    /// <summary>
    /// Pen event arguments. Inherits from <see cref="EventArgs"/> class.
    /// </summary>
    public class PenEventArgs : EventArgs
    {
        /// <summary>
        /// Pen event arguments. Inherits from <see cref="EventArgs"/> class.
        /// </summary>
        /// <param name="cana"></param>
        public PenEventArgs(int cana)
        {
            Cana = cana;
        }

        public readonly int Cana;
    }


    public class Coche
    {
        public event EventHandler<EventArgs> NueumaticoCambiado;

        public void CambioNeumatico()
        {
            NueumaticoCambiado.Raise(this, new EventArgs());
        }
    }


    public class Pen
    {

        /// <summary>
        /// Event informs that cana changed. Event of <see cref="Pen"/> class.
        /// </summary>
        public event EventHandler<PenEventArgs> CanaCambiada;


        protected virtual void OnCanaCambiada(int valorCana)
        {
            CanaCambiada.Raise(this, new PenEventArgs(valorCana));
        }



        private int _cana;
        public int Cana
        {
            get { return _cana; }
            set
            {
                if (value == _cana)
                    return;

                _cana = value;
                OnCanaCambiada(_cana);
            }
        }


    }

}
