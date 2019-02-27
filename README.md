# BreadthFirstSearch

# coding challenge from: 
#    https://www.geeksforgeeks.org/top-10-algorithms-in-interview-questions/

# this following is psuedocode for this algorithm:
#  (from this site: https://www.hackerearth.com/practice/algorithms/graphs/breadth-first-search/tutorial/ )

BFS (G, s)                   //Where G is the graph and s is the source node
      let Q be queue.
      Q.enqueue( s ) //Inserting s in queue until all its neighbour vertices are marked.

      mark s as visited.
      while ( Q is not empty)
           //Removing that vertex from queue,whose neighbour will be visited now
           v  =  Q.dequeue( )

          //processing all the neighbours of v  
          for all neighbours w of v in Graph G
               if w is not visited 
                        Q.enqueue( w )             //Stores w in Q to further visit its neighbour
                        mark w as visited.