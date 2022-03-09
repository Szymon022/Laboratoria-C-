namespace PL_Lab05
{
    static class GraphProcessor
    {
        public static ((int, int), (int, int))FindMinAndMaxDegree(Graph graph)
        {
            (int, int) max_degree;
            (int, int) min_degree;
            
            for(int i = 0; i < graph.)
        }

        public static int[] FindAdjacentVertices(Graph graph, int weight)
        {

        }

        public static Edge[] SortEdges(Edge[] edges)
        {
            for(int i = 0; i < edges.Length - 1; i++)
            {
                for(int j = i; j < edges.Length - 1 - i; j++)
                {
                    if(edges[j].GetWeight() > edges[j+1].GetWeight())
                    {
                        Edge tmp_edge = new Edge(edges[j+1]);
                        edges[j + 1] = edges[j];
                        edges[j] = tmp_edge;
                    }
                }
            }
            return edges;
        }
    }
}