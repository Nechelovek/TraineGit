using System;
using System.Linq;
using System.Collections.Generic;

namespace AlgorithmAndDataStruct
{
    public class Algorithm
    {
        //Looking for smaller value in array
        static int SmallestIndex(int[] arr)
        {
            int smallestValue = arr[0];
            int smallestIndex = 0;
            
            for(int i = 0; i < arr.Length;i++)
                if (arr[i] < smallestValue)
                {
                    smallestValue = arr[i];
                    smallestIndex = i;
                }

            return smallestIndex;
        }
        public static void PrintArray(int[] arr)
        {
            foreach (var i in arr)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();
        }

        public static int Factorial(int n)    
        {
            if (n == 1)
                return 1;
            return n * Factorial(n - 1);
        }
        public static int[] SelectionSort(int[] arr)
        {
            int[] sortArray = new int[arr.Length];
            
            for (int i = 0; i < sortArray.Length; i++)
            {
                int smallestIndex = SmallestIndex(arr);
                sortArray[i] = arr[smallestIndex];
                
                //Delete smallest element in input array                
                var arrToList = arr.ToList();
                arrToList.RemoveAt(smallestIndex);
                arr = arrToList.ToArray();

            }

            return sortArray;
        }
        public static int Binary_Search(int[] arr, int guess)
        {
            int low = 0;
            int high = arr.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                
                if (arr[mid] == guess)
                    return mid;
                
                if (guess < arr[mid])
                    high = mid - 1;
                
                if (guess > arr[mid])
                    low = mid + 1;
            }
            return 0;
        }
        //It's not very good realization because we don't have a good pivot element
        public static int[] QuickSort(int[] arr)
        {
            if (arr.Length < 2)
                return arr;
            else
            {
                int pivot = arr[0];
                var arrToList = arr.ToList();
                arrToList.RemoveAt(0);
                arr = arrToList.ToArray();
                
                List<int> less = new List<int>();
                List<int> bigger = new List<int>();
                foreach (var i in arr)
                {
                    if(i < pivot)
                        less.Add(i);
                    else
                        bigger.Add(i);
                }
                //PrintArray(bigger.ToArray());
                
                var result = QuickSort(less.ToArray()).ToList();
                result.Add(pivot);
                result.AddRange(QuickSort(bigger.ToArray()));
                
                return result.ToArray();
            }
        }
        //You need to think about how output index from list
        public static int RecursiveBinarySearch(List<int> list, int guess)
        {
            if (list.Count == 1)
                return list[0] == guess ? 0 : -1;
            else
            {
                int mid = list.Count / 2;
                if (list[mid] == guess)
                    return guess;
                if (list[mid] > guess)
                {
                    list.RemoveRange(mid,list.Count / 2 - 1);
                    return RecursiveBinarySearch(list,guess);
                }
                else
                {
                    list.RemoveRange(0,mid);
                    return RecursiveBinarySearch(list, guess);
                }
            }
        }
        //Search in graph structure
        public static string Search(Dictionary<string, string[]> graph, string name)
        {
            Queue<string> queueForFriends = new Queue<string>();
            List<string> listForCheckedFriends = new List<string>();
            
            queueForFriends.Enqueue(name);
            
            while (queueForFriends.Any())
            {
                string person = queueForFriends.Dequeue();
                if (!listForCheckedFriends.Contains(person))
                {
                    //It's a simple test for search. I try to imagine good task...
                    if (person == "Eva")
                    {
                        Console.WriteLine("Seller was found");
                        return person;
                    }
                    else
                    {
                        listForCheckedFriends.Add(person);
                    
                        foreach (var i in graph[person])
                        {
                            queueForFriends.Enqueue(i);
                        }
                    }
                }
            }
            return "No one wasn't found";
        }
        //Algorithm Deikstra
        public static void Deikstra(Dictionary<string, Dictionary<string, int>> graph)
        {
            Dictionary<string, int> costs = new Dictionary<string, int>();
            Dictionary<string, string> parents = new Dictionary<string, string>();
            
            //Input date for parents list
            foreach (var i in graph.Keys)
            {
                if(graph[i].Count == 0)
                    parents.Add(i, null);
                foreach (var j in graph[i].Keys)
                {
                    if(!parents.ContainsKey(i))
                    {
                        if (graph[i].Count == 0)
                        {
                            Console.WriteLine("Here");
                            parents.Add(j, null);
                        }
                        else
                            parents.Add(j,i);
                        
                    }
                    
                }
            }
            //Input weight line
            foreach (var i in graph.Keys)
            {
                if (graph[i].Count == 0)
                {
                    if(!costs.ContainsKey(i))
                        costs.Add(i, Int32.MaxValue);
                    else
                        costs[i] = Int32.MaxValue;
                }
                foreach (var j in graph[i].Keys)
                {
                    if (!costs.ContainsKey(j))
                    {
                        costs.Add(j, graph[i][j]);
                    }
                }
            }
            
            List<string> processed = new List<string>();

            var node = FindLowestCostNode(costs, processed);
            
            while (node != null)
            {
                int cost = costs[node];
                var neighbors = graph[node];
                foreach (var n in neighbors.Keys)
                {
                    int newCost = cost + neighbors[n];
                    if (costs[n] > newCost)
                    {
                        costs[n] = newCost;
                        parents[n] = node;
                    } 
                    processed.Add(node);
                    node = FindLowestCostNode(costs, processed);
                }  
            }
        }

        private static string FindLowestCostNode(Dictionary<string, int> costs, List<string> proccessed)
        {
            int lowestCost = Int32.MaxValue;
            string lowestCostNode = null;
            foreach (var n in costs)
            {
                var cost = n.Value;
                if (cost < lowestCost && proccessed.Contains(n.Key))
                {
                    lowestCost = cost;
                    lowestCostNode = n.Key;
                }
            }
            return lowestCostNode;
        }

        //For practice recursive
        public static int Sum(int[] arr)
        {
            if (arr.Length == 0)
                return 0;
            else
            {
                int temp = arr[0]; 
                var tempArr = arr.ToList();
                tempArr.RemoveAt(0);
                return temp + Sum(tempArr.ToArray());
            }
        }
        public static int CountList(List<int> list)
        {
            if (list.Count == 0)
                return 0;
            else
            {
                list.RemoveAt(0);
                return 1 + CountList(list);
            }
        }
        public static int MaxElement(List<int> list)
        {
            if (list.Count == 1)
                return list[0];
            else
            {
                int tmp = list[0];
                list.RemoveAt(0);
                return tmp > MaxElement(list) ? tmp : MaxElement(list);
            }
        }
        //You need to think about how output index from list
        
    }
}