using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SI
{
    class kColoringGraph
    {
        Dictionary<int, int> VertexColor;
        List<Vertex> ListOfVertex;
        
        public kColoringGraph(Dictionary<int, int> VertexColorDict, List<Vertex> graph ) {
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
                        v.setColor(Color.FromArgb((n.Value*1334 )%256, (n.Value * 2583)% 256, (n.Value* 4231)%256));
                      }
                }
            }
            
            
            return ListOfVertex;
        }
    }
}
