using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public void BreadthFirstSearchTraversesSevenVertices()
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

        [TestMethod]
        public void BreadthFirstSearchTraversesSevenVerticesWithjSteveKuosEdges()
        {
            Graph graph = new Graph(7);
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 5);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 1);

            List<int> verticesInTraversalOrder = graph.BreadthFirstSearch(2);
            CollectionAssert.AreEqual(verticesInTraversalOrder, new List<int> { 2,3,4,1,0,5 });
        }

        [TestMethod]
        public void DepthFirstSearchTraversesFourVertices()
        {
            Graph graph = new Graph(4);
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 0);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 3);

            int startingVertex = 2;
            List<int> verticesInTraversalOrder = graph.DepthFirstSearch(startingVertex);
            CollectionAssert.AreEqual(verticesInTraversalOrder, new List<int> { 2, 0, 1, 3 });
        }

        [TestMethod]
        public void DepthFirstSearchTraversesSevenVerticesWithjSteveKuosEdges()
        {
            Graph graph = new Graph(7);
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 5);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 4);
            graph.AddEdge(3, 4);
            graph.AddEdge(4, 1);

            List<int> verticesInTraversalOrder = graph.DepthFirstSearch(2);
            CollectionAssert.AreEqual(verticesInTraversalOrder, new List<int> { 2, 3, 4, 1, 0, 5 });
        }

        [TestMethod]
        public void ShortestPathsFoundFromSourceToEachOfNineVertices()
        {
            int[,] definedGraph =
            {
                {0,4,0,0,0,0,0,8,0},
                {4,0,8,0,0,0,0,11,0},
                {0,8,0,7,0,4,0,0,2},
                {0,0,7,0,9,14,0,0,0},
                {0,0,0,9,0,10,0,0,0},
                {0,0,4,14,10,0,2,0,0},
                {0,0,0,0,0,2,0,1,6},
                {8,11,0,0,0,0,1,0,7},
                {0,0,2,0,0,0,6,7,0}
            };

            Graph graph = new Graph(0);
            int[] distances = graph.Dijkstra(definedGraph, 0);
            Assert.AreEqual(0,distances[0]);
            Assert.AreEqual(4,distances[1]);
            Assert.AreEqual(12,distances[2]);
            Assert.AreEqual(19,distances[3]);
            Assert.AreEqual(21,distances[4]);
            Assert.AreEqual(11,distances[5]);
            Assert.AreEqual(9,distances[6]);
            Assert.AreEqual(8,distances[7]);
            Assert.AreEqual(14,distances[8]);
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
            private bool[] visited;

            public void AddEdge(int edgeStartVertexIndex, int edgeEndVertexIndex)
            {
                Vertices[edgeStartVertexIndex].AddLast(edgeEndVertexIndex);
            }

            public List<int> BreadthFirstSearch(int startingVertexIndex)
            {
                List<int> traversedVertices = new List<int>();
                
                visited = new bool[Vertices.Length];

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

            public List<int> DepthFirstSearch(int startingVertex)
            {
                List<int> traversedVertices = new List<int>();
                visited = new bool[Vertices.Length];

                visited[startingVertex] = true;

                DepthFirstSearchUtil(traversedVertices, startingVertex);

                return traversedVertices;
            }

            private void DepthFirstSearchUtil(List<int> traversedVertices, int startingVertex)
            {
                traversedVertices.Add(startingVertex);
                LinkedList<int>.Enumerator enumeratorOnCurrentVertex = Vertices[startingVertex].GetEnumerator();
                visited[startingVertex] = true;
                while (enumeratorOnCurrentVertex.MoveNext())
                {
                    int currentVertexIndex = enumeratorOnCurrentVertex.Current;
                    if (!visited[currentVertexIndex])
                    {
                        DepthFirstSearchUtil(traversedVertices, currentVertexIndex);
                    }
                }
            }

            public int[] Dijkstra(int[,] graph, int sourceVertex)
            {
                Boolean[] sptSet = new Boolean[graph.GetLength(0)];
                int[] distances = new int[graph.GetLength(0)];
                for (int index =0;index < graph.GetLength(0); index++)
                {
                    sptSet[index] = false;
                    distances[index] = int.MaxValue;
                }

                distances[sourceVertex] = 0;

                for (int count = 0; count < graph.GetLength(0) - 1; count++)
                {
                    int u = FindClosestNonTraversed(graph.GetLength(0), distances, sptSet);
                    sptSet[u] = true;

                    for (int v = 0; v < graph.GetLength(0); v++) {
                        if (!sptSet[v] && 
                            graph[u, v] != 0 &&
                            distances[u] + graph[u, v] < distances[v])
                        {
                            distances[v] = distances[u] + graph[u, v];
                        }
                    }
                }

                return distances;
            }

            private int FindClosestNonTraversed(int totalVertices, int[] distances, bool[] traversedVertices)
            {
                int minimumDistance = int.MaxValue;
                int indexOfClosestVertex = -1;

                for (int index = 0; index < totalVertices -1; index++) {
                    if (traversedVertices[index] == false && distances[index] <= minimumDistance)
                    {
                        minimumDistance = distances[index];
                        indexOfClosestVertex = index;
                    }
                }

                return indexOfClosestVertex;
            }
        }
    }
}
