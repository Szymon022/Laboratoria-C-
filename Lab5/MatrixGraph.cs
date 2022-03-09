namespace PL_Lab05
{
    class MatrixGraph : Graph
    {
        private int[,] matrix;
        private int edgesCount = 0;
        private int rows = 0;

        public MatrixGraph(int n_verticies, Edge[] edges = null) : base(n_verticies)
        {
            matrix = new int[n_verticies, n_verticies];
            for(int i = 0; i < n_verticies; i++)
            {
                for(int j = 0; j < n_verticies; j++)
                {
                    matrix[i, j] = -1;
                }
            }

            if(edges != null)
            {
                for(int i = 0; i < edges.Length; i++)
                {
                    if(edges[i].start < n_verticies && edges[i].end < n_verticies)
                    {
                        if(matrix[edges[i].start, edges[i].end] == -1)
                        {
                            edgesCount++;
                        }                    
                        matrix[edges[i].start, edges[i].end] = edges[i].GetWeight();
                    }
                        
                }
            }
            rows = n_verticies;

        }
        public override void AddEdge(Edge edge)
        {
            if (edge.start > rows - 1 || edge.end > rows - 1)
            {
                return;
            }
            if(matrix[edge.start, edge.end] == -1)
            {
                edgesCount++;
            }
            matrix[edge.start, edge.end] = edge.GetWeight();
            
        }

        public override Edge GetEdge(int start, int end)
        {
            if (start >= rows - 1 || end >= rows - 1) return null;
            if (start > end)
            {
                int tmp = start;
                start = end;
                end = tmp;
            }

            if (matrix[start, end] != -1)
            {
                return new Edge(start, end, matrix[start, end]);
            }
            return null;
            
        }

        public override int GetEdgesCount()
        {
           return edgesCount;
        }

        public int getRows()
        {
            return rows;
        }

        public override void RemoveEdge(Edge edge)
        {
            if (edge.start >= rows - 1 || edge.end >= rows - 1) return;
            if (matrix[edge.start, edge.end] != -1)
            {
                matrix[edge.start, edge.end] = -1;
                edgesCount--;
            }
        }
    }
}