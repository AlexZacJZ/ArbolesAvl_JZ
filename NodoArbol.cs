namespace ArbolBinario
{
    class NodoArbol<T>
    {
        public T data { get; set; }
        public int fe { get; set; }
        public NodoArbol<T> izq { get; set; }
        public NodoArbol<T> der { get; set; }
    }
}
