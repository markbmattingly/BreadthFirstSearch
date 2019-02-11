using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BreadthFirstTests
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void ConstuctorCreatesLinkedListWithDesignatedNumberOfVertices()
        {
            var numberOfVertices = 4;
            Graph graph = new Graph(numberOfVertices);
            Assert.IsInstanceOfType(graph.Vertices, typeof(LinkedList<int>[]));
            Assert.AreEqual(numberOfVertices, graph.Vertices.Length);
            Assert.IsInstanceOfType(graph.Vertices[0], typeof(LinkedList<int>));
        }

        [TestMethod]
        public void AddEdgesCreatesTheCorrectEdges()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 3);

            Assert.AreEqual(graph.Vertices[0].First.Value, 1);
            Assert.AreEqual(graph.Vertices[2].First.Value, 3);
            Assert.AreEqual(graph.Vertices[3].First.Value, 3);
        }

        [TestMethod]
        public void BreadthFirstSearchTraversesFourVertices()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 3);

            int startingVertex = 2;
            List<int> verticesInTraversalOrder = graph.BreadthFirstSearch(startingVertex);
            CollectionAssert.AreEqual(verticesInTraversalOrder, new List<int> {2,0,3,1});
        }

        [TestMethod]
        public void BreadthFirstSearchDoesInFactSearchBreadthFirst()
        {
            Graph graph = new Graph(7);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 2);
            graph.AddEdge(4, 5);
            graph.AddEdge(5, 6);
            
            List<int> verticesInTraversalOrder = graph.BreadthFirstSearch(0);
            CollectionAssert.AreEqual(verticesInTraversalOrder, new List<int> { 0,1,2,3,4,5,6 });

            verticesInTraversalOrder = graph.BreadthFirstSearch(3);
            CollectionAssert.AreEqual(verticesInTraversalOrder, new List<int> { 3, 2, 0, 4, 5, 6, 1 });

        }

        public class Graph
        {
            public Graph(int numberOfVertices)
            {
                Vertices = new LinkedList<int>[numberOfVertices];
                for (int index = 0; index < numberOfVertices; index++)
                {
                    Vertices[index] = new LinkedList<int>();
                }
            }

            public LinkedList<int>[] Vertices { get; set; }

            public void AddEdge(int edgeStartVertexIndex, int edgeEndVertexIndex)
            {
                Vertices[edgeStartVertexIndex].AddLast(edgeEndVertexIndex);
            }

            public List<int> BreadthFirstSearch(int startingVertexIndex)
            {
                List<int> traversedVertices = new List<int>();

                bool[] visited = new bool[Vertices.Length];

                LinkedList<int> queue = new LinkedList<int>();

                visited[startingVertexIndex] = true;
                queue.AddLast(startingVertexIndex);
                traversedVertices.Add(startingVertexIndex);

                while (queue.Count > 0)
                {
                    int currentVertex = queue.Last();
                    queue.RemoveLast();

                    LinkedList<int>.Enumerator enumeratorOnCurrentVertex = Vertices[currentVertex].GetEnumerator();

                    while(enumeratorOnCurrentVertex.MoveNext())
                    {
                        
                        if (!visited[enumeratorOnCurrentVertex.Current])
                        {
                            queue.AddLast(enumeratorOnCurrentVertex.Current);
                            visited[enumeratorOnCurrentVertex.Current] = true;
                            traversedVertices.Add(enumeratorOnCurrentVertex.Current);
                        }
                    }
                }

                return traversedVertices;
            }
        }
    }
}
