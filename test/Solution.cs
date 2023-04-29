using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace test
{
    public class Node
    {
        public string Name { get; set; }
        public int? Index { get; set; }
        public Dictionary<string, Node> Child { get; set; } = new();
    }

    public class Solution 
    {
        public static int[] GetSolution(string[] A, string[] B)
        {
            var aNodes = CreateHostTree(A);

            var forbiddenIndexes = GetForbiddenHostIndexes(aNodes, B).ToHashSet();

            var result = new List<int>();
            for(var index = 0; index < A.Length; index++)
                if(!forbiddenIndexes.Contains(index))
                    result.Add(index);

            return result.ToArray();
        } 

        private static IDictionary<string, Node> CreateHostTree(string[] A)
        {
            var aNodes = new Dictionary<string, Node>();
            for (var lineIndex = 0; lineIndex < A.Length; lineIndex++)
            {
                var nodes = A[lineIndex].Split('.');
                var childNodes = aNodes;
                for (var index = nodes.Length - 1; index >= 0; index--)
                {
                    var nodeName = nodes[index];
                    if (!childNodes.ContainsKey(nodeName))
                    {
                        var newNode = new Node() { Name = nodeName, Index = index == 0 ? lineIndex : null };
                        childNodes[nodeName] = newNode;
                    }
                    childNodes = childNodes[nodeName].Child;
                }
            }

            return aNodes;
        }

        private static IEnumerable<int> GetForbiddenHostIndexes(IDictionary<string, Node> aNodes, string[] B)
        {
            var forbiddenIndexes = new List<int>();
            foreach (var b in B)
            {
                var nodes = b.Split('.');
                var childNodes = aNodes;
                for (var index = nodes.Length - 1; index >= 0; index--)
                {
                    var nodeName = nodes[index];
                    if (childNodes.ContainsKey(nodeName))
                    {
                        if (index == 0)
                        {
                            forbiddenIndexes.AddRange(GetIndexes(new[] { childNodes[nodeName] }));
                            break;
                        }
                        childNodes = childNodes[nodeName].Child;
                    }
                    else
                        break;
                }
            }

            return forbiddenIndexes;
        }

        private static IEnumerable<int> GetIndexes(IEnumerable<Node> nodes)
        {
            var result = new List<int>();

            foreach(var node in nodes)
            {
                if(node.Index.HasValue)
                    result.Add(node.Index.Value);

                result.AddRange(GetIndexes(node.Child.Values.ToList()));
            }

            return result;
        }
    }
}
