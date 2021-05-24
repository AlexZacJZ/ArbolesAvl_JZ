using System;

namespace ArbolBinario
{
    class ArbolBinarioBusqueda<T>
    {
        public NodoArbol<T> raiz { get; set; }

        public ArbolBinarioBusqueda()
        {
            raiz = null;
        }

        public void Insertar(T valor,
            Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            raiz = Insertar(raiz, valor, MenorQue, MayorQue);
        }

        public NodoArbol<T> Insertar(NodoArbol<T> raizSub, T valor,
            Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            if (raizSub == null)
            {
                raizSub = new NodoArbol<T>
                { data = valor, izq = null, der = null };
            }
            else if (MenorQue(valor, raizSub.data))
            {
                raizSub.izq = Insertar(raizSub.izq, valor, MenorQue, MayorQue);
            }
            else if (MayorQue(valor, raizSub.data))
            {
                raizSub.der = Insertar(raizSub.der, valor, MenorQue, MayorQue);
            }
            else throw new Exception("Nodo duplicado");

            /*Actualizarfe(raizSub);
            if (Actualizarfe(raizSub) == 2)
            {
                if (MenorQue(valor, raizSub.der.data))
                    raizSub = RotacionDobleDer(raizSub);
                else
                    raizSub = RotacionSimpleDer(raizSub);
            }
            if (Actualizarfe(raizSub) == -2)
            {
                if (MayorQue(valor, raizSub.izq.data))
                    raizSub = RotacionDobleIzq(raizSub);
                else
                    raizSub = RotacionSimpleIzq(raizSub);
            }
            return raizSub;*/
            raizSub = Balancear(raizSub);
            return raizSub;
        }

        public void MostrarArbol(NodoArbol<T> raiz, String espacio)
        {
            if (raiz == null)
            {
                Console.WriteLine(espacio + "null");
            }
            else
            {
                MostrarArbol(raiz.der, espacio + "         ");
                Console.WriteLine(espacio + "      /");
                Console.WriteLine(espacio + raiz.data);
                Console.WriteLine(espacio + "      \\");
                MostrarArbol(raiz.izq, espacio + "         ");
            }
        }

        //METODO ENCONTRAR PADRE DEL NODO
        public NodoArbol<T> BuscarPadre(NodoArbol<T> Subraiz, T valor,
            Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            NodoArbol<T> temp = null;
            if (Subraiz == null)
            {
                return null;
            }
            //Verifico si soy el padre
            if (Subraiz.izq != null)
            {
                if (ComparaNodo(Subraiz.izq.data, valor) == true)
                {
                    return Subraiz;
                }
            }
            if (Subraiz.der != null)
            {
                if (ComparaNodo(Subraiz.der.data, valor) == true)
                {
                    return Subraiz;
                }
            }
            if (Subraiz.izq != null && MenorQue(valor, Subraiz.data))
            {
                temp = BuscarPadre(Subraiz.izq, valor, MenorQue, MayorQue);
            }
            if (Subraiz.der != null && MayorQue(valor, Subraiz.data))
            {
                temp = BuscarPadre(Subraiz.der, valor, MenorQue, MayorQue);
            }
            return temp;
        }
        //METODO COMPARA SI LOS NODOS SON IGUALES
        public bool ComparaNodo(T Subraiz, T valor)
        {
            string info = Convert.ToString(valor);
            string info2 = Convert.ToString(Subraiz);
            if (info == info2)
            {
                return true;
            }
            return false;
        }
        //METODO MOSTRAR NODO DE UN ARBOL (SOLO PARA MOSTRAR DEL METODO BUSCARPADRE)
        public String MuestraNodo(NodoArbol<T> Subraiz, T valor,
            Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            NodoArbol<T> imagen = BuscarPadre(Subraiz, valor, MenorQue, MayorQue);

            string info = Convert.ToString(imagen.data);
            return info;
        }

        //METODO BORRAR NODO
        public void Eliminar(T valor,
       Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            raiz = eliminarN(raiz, valor, MenorQue, MayorQue);
        }

        public NodoArbol<T> eliminarN(NodoArbol<T> raizSub, T valor,
             Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            NodoArbol<T> padre = BuscarPadre(raizSub, valor, MenorQue, MayorQue);
            if (raizSub == null)
            {
                return null;
            }

            else if (MenorQue(valor, raizSub.data))
            {
                raizSub.izq = eliminarN(raizSub.izq, valor, MenorQue, MayorQue);

            }
            else if (MayorQue(valor, raizSub.data))
            {
                raizSub.der = eliminarN(raizSub.der, valor, MenorQue, MayorQue);
            }
            else
            {
                //CASO SIN HIJOS
                if (raizSub.izq == null && raizSub.der == null)
                {
                    raizSub = null;
                    return raizSub;
                }
                //CASO 1 HIJO DERECHO
                else if (raizSub.izq == null)
                {
                    padre = raizSub.der;
                    return raizSub;
                }
                //CASO 1 HIJO IZQUIERDO
                else if (raizSub.der == null)
                {
                    padre.izq = raizSub.izq;
                    return raizSub;
                }
                //CASO 2 HIJOS
                else
                {
                    NodoArbol<T> minimo = raizSub.izq;
                    raizSub.data = minimo.data;
                    raizSub.izq = null;
                    raizSub.der = eliminarN(raizSub.der, minimo.data, MenorQue, MayorQue);
                }
            }
            return raizSub = Balancear(raizSub);

        }


        public NodoArbol<T> Busca(T valor,
       Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            return BuscarNdo(raiz, valor, MenorQue, MayorQue);

        }
        public NodoArbol<T> BuscarNdo(NodoArbol<T> raizSub, T valor,
             Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {

            if (raizSub == null)
            {
                return null;
            }

            else if (MenorQue(valor, raizSub.data))
            {
                return raizSub.izq = BuscarNdo(raizSub.izq, valor, MenorQue, MayorQue);

            }
            else if (MayorQue(valor, raizSub.data))
            {
                return raizSub.der = BuscarNdo(raizSub.der, valor, MenorQue, MayorQue);
            }
            else
            {
                InfoNodo(raizSub.data);

            }
            return raizSub;

        }
        public void InfoNodo(T raizSub)
        {
            string info = Convert.ToString(raizSub);
            Console.WriteLine("El nodo buscado es: " + info);
        }

        //OBTENER FACTOR DE EQULIBRIO
        public int Actualizarfe(NodoArbol<T> raizSub)
        {
            int altura = 0;
            raizSub.fe = 0;
            if (raizSub == null)
            {
                return 0;
            }
            else
            {
                if (raizSub.izq != null)
                {
                    raizSub.fe = Actualizarfe(raizSub.izq);
                }
                if (raizSub.der != null)
                {
                    raizSub.fe = Actualizarfe(raizSub.der);
                }
                //EVALUACION SIN HIJOS
                if (raizSub.izq == null && raizSub.der == null)
                {
                    raizSub.fe = 0;
                }
                else
                {//EVALUA HIJO IZQUIERDO
                    if (raizSub.der == null)
                    {
                        altura = 0 - Altura(raizSub.izq);
                        raizSub.fe = altura;
                    }
                    else if (raizSub.izq == null)
                    {
                        altura = Altura(raizSub.der) - 0;
                        raizSub.fe = altura;
                    }
                    else
                    {
                        altura = Altura(raizSub.der) - Altura(raizSub.izq);
                        raizSub.fe = altura;
                    }

                }

            }
            return raizSub.fe;
        }
        public int Altura(NodoArbol<T> raizSub)
        {
            if (raizSub == null)
                return 0;
            else
                return 1 + Math.Max(Altura(raizSub.izq), Altura(raizSub.der)); ;
        }

        public NodoArbol<T> RotacionSimpleIzq(NodoArbol<T> raizSub)
        {
            NodoArbol<T> aux = raizSub;
            aux = raizSub.izq;
            raizSub.izq = aux.der;
            aux.der = raizSub;
            raizSub.fe = Math.Max(Altura(raizSub.izq), Altura(raizSub.der));
            aux.fe = Actualizarfe(aux);
            return aux;
        }
        public NodoArbol<T> RotacionSimpleDer(NodoArbol<T> raizSub)
        {
            NodoArbol<T> aux = raizSub;
            aux = raizSub.der;
            raizSub.der = aux.izq;
            aux.izq = raizSub;
            raizSub.fe = Math.Max(Altura(raizSub.izq), Altura(raizSub.der));
            aux.fe = Actualizarfe(aux);
            return aux;
        }
        public NodoArbol<T> RotacionDobleIzq(NodoArbol<T> raizSub)
        {
            raizSub.izq = RotacionSimpleDer(raizSub.izq);
            return RotacionSimpleIzq(raizSub);
        }
        public NodoArbol<T> RotacionDobleDer(NodoArbol<T> raizSub)
        {
            raizSub.der = RotacionSimpleIzq(raizSub.der);
            return RotacionSimpleDer(raizSub);
        }
        public NodoArbol<T> Balancear(NodoArbol<T> raizSub)
        {
            if (Actualizarfe(raizSub) == 2)
            {
                if (Actualizarfe(raizSub.der) > 0)
                    raizSub = RotacionSimpleDer(raizSub);
                else
                    raizSub = RotacionDobleDer(raizSub);
                return raizSub;
            }
            if (Actualizarfe(raizSub) == -2)
            {
                if (Actualizarfe(raiz.izq) < 0)
                    raizSub = RotacionSimpleIzq(raizSub);
                else
                    raizSub = RotacionDobleIzq(raizSub);
                return raizSub;
            }
            return raizSub;
        }

        /*
        //Rotacion simple a la izquierda
        public NodoArbol<T> RotacionIzq(NodoArbol<T> raizSub)
        {
            NodoArbol<T> aux = raizSub.izq;
            raizSub.izq = aux.der;
            aux.der = raizSub;
            raizSub.fe = Math.Max(Obtenerfe(raizSub.izq), Obtenerfe(raizSub.der))+1;
            aux.fe= Math.Max(Obtenerfe(aux.izq), Obtenerfe(aux.der)) + 1;
            return aux;
        }
        //Rotacion Simple Derecha
        public NodoArbol<T> RotacionDer(NodoArbol<T> raizSub)
        {
            NodoArbol<T> aux = raizSub.der;
            raizSub.der = aux.izq;
            aux.izq = raizSub;
            raizSub.fe = Math.Max(Obtenerfe(raizSub.izq), Obtenerfe(raizSub.der)) + 1;
            aux.fe = Math.Max(Obtenerfe(aux.izq), Obtenerfe(aux.der)) + 1;
            return aux;
        }
        //Rotacion doble a la derecha
        public NodoArbol<T> RotacionDobleIZQ(NodoArbol<T> raizSub)
        {
            NodoArbol<T> temp;
            raizSub.izq = RotacionDer(raizSub.izq);
            temp = RotacionIzq(raizSub);
            return temp;
        }
        //Rotacion Doble a la izquierda
        public NodoArbol<T> RotacionDobleDD(NodoArbol<T> raizSub)
        {
            NodoArbol<T> temp;
            raizSub.der = RotacionIzq(raizSub.der);
            temp = RotacionDer(raizSub);
            return temp;
        }*/




    }
}
