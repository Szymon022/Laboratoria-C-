namespace PL_Lab05
{
    class Edge
    {
        public int start;
        public int end;
        private int weight;

        public Edge(int start, int end, int weight = 0)
        {
            if(start <= end)
            {
                this.start = start;
                this.end = end;
            }
            else
            {
                this.start = end;
                this.end = start;
            }
            if(weight >= 0)
            {
                this.weight = weight;
            }
        }

        public Edge(int start, int end, float weight)
        {
            if (start <= end)
            {
                this.start = start;
                this.end = end;
            }
            else
            {
                this.start = end;
                this.end = start;
            }
            if (weight > 0)
            {
                this.weight = (int)weight;
            }
        }

        public Edge(Edge edge)
        {
            start = edge.start;
            end = edge.end;
            weight = edge.weight;
        }

        public Edge((int, int, int) touple)
        {
            if (touple.Item1 <= touple.Item2)
            {
                this.start = touple.Item1;
                this.end = touple.Item2;
            }
            else
            {
                this.start = touple.Item2;
                this.end = touple.Item1;
            }
            if (touple.Item3 >= 0)
            {
                this.weight = touple.Item3;
            }
            else
            {
                this.weight = 0;
            }
        }   

        public int GetWeight()
        {
            return weight;
        }

        public void SetWeight(int weight)
        {
            if(weight >= 0)
            {
                this.weight = weight;
            }
        }
    }
}