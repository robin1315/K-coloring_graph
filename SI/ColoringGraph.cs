using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SI
{
    class ColoringGraph
    {
        Dictionary<int, int> VertexColor;
        List<Vertex> ListOfVertex;
        
        public ColoringGraph(Dictionary<int, int> VertexColorDict, List<Vertex> graph ) {
            this.VertexColor = VertexColorDict;
            this.ListOfVertex = graph;
        }

        public List<Vertex> Coloring() {
            foreach (Vertex v in ListOfVertex) {
                foreach (KeyValuePair<int, int> n in VertexColor)
                {
                    if (v.ID == n.Key)
                    {/*
                        Random gen = new Random(n.Value); 
                        v.setColor(Color.FromArgb(((n.Value * 158) + gen.Next()) % 256,
                            ((n.Value * 124) + gen.Next()) % 256,
                            ((n.Value * 248) + gen.Next()) % 256));
                   */
                        v.setColor(Color.FromArgb((n.Key*1334 )%256, (n.Key * 2583)% 256, (n.Key* 4231)%256));
                      }
                }
            }
            
            
            return ListOfVertex;
        }
    }
}
