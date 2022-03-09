namespace PL_Lab05
{
    abstract class Graph
    {
        protected int verticesCount;

        public Graph(int n)
        {
            if(n > 0)
            {
                verticesCount = n;
            }
            else
            {
                verticesCount = 0;
            }
        }

        abstract public void AddEdge(Edge edge);
        abstract public void RemoveEdge(Edge edge);
        abstract public Edge GetEdge(int start, int end);
        abstract public int GetEdgesCount();
        public int GetVerticesCount()
        {
            return verticesCount;
        }
    }
}