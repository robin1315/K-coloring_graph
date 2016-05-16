using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI
{
    class Generate_cnf
    {
        public List<String> CnfList { get; set; }
        public List<String> firsC { get; set; }
        public List<String> secondC { get; set; }
        public List<String> thirdC { get; set; }

        public Generate_cnf() { 
        
        }

        public List<String> Run(int VertexCount, List<string> ListOfNeighbours){
            CnfList = new List<String>();

            CnfList.AddRange(firstCondition(VertexCount));
            CnfList.AddRange(secondCondition(VertexCount));
            CnfList.AddRange(thirdCondition(ListOfNeighbours));

            CnfList.Insert(0, "p cnf " + VertexCount*4 + " " + CnfList.Count);
            return CnfList;
        }
        public List<String> firstCondition(int countVertex) {
            List<int> tmp = new List<int>();
            tmp.AddRange(new int[4]);
            
            List<string> firstCond = new List<string>();

            
            for (int i = 1; i <= countVertex; i++)
            {
                tmp[0] = (4 * i) - 3;
                tmp[1] = (4 * i) - 2;
                tmp[2] = (4 * i) - 1;
                tmp[3] = 4 * i;

                firstCond.Add(tmp[0] + " " + tmp[1] + " " + tmp[2] + " " + tmp[3] + " 0");
            }
            firsC = firstCond;
            return firstCond;
        }
        public List<String> secondCondition(int VertexCount)
        {
            List<int> tmp = new List<int>(4);
            tmp.AddRange(new int[4]);
            List<string> secondCond = new List<string>();

            for (int i = 1; i <= VertexCount; i++)
            {
                for (int j = 2; j >= 0; j--)
                {
                    tmp[0] = (4 * i) - 3;
                    tmp[1] = (4 * i) - j;

                    secondCond.Add("-" + tmp[0] + " -" + tmp[1] + " 0");
                }

                for (int j = 1; j >= 0; j--)
                {
                    tmp[0] = (4 * i) - 2;
                    tmp[1] = (4 * i) - j;

                    secondCond.Add("-" + tmp[0] + " -" + tmp[1] + " 0");
                }

                tmp[0] = (4 * i) - 1;
                tmp[1] = 4 * i;

                secondCond.Add("-" + tmp[0] + " -" + tmp[1] + " 0");
            }
            secondC = secondCond;
            return secondCond;
        }
        public List<String> thirdCondition(List<string> neighbours) {
            //Todo poprawic ta clausule 
            List<int> tmp = new List<int>();
            tmp.AddRange(new int[2]);
            List<string> thirdCondition = new List<string>();

            foreach (string neighbour in neighbours)
            {
                if (string.IsNullOrEmpty(neighbour))
                {
                    continue;
                }

                int vertex1 = int.Parse(neighbour.Split(':')[0]);
                int vertex2 = int.Parse(neighbour.Split(':')[1]);

                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 3; j >= 0; j--)
                    {
                        tmp[0] = (4 * vertex1) - j;
                        tmp[1] = (4 * vertex2) - j;

                        thirdCondition.Add("-" + tmp[0] + " -" + tmp[1] + " 0");
                    }
                }
            }

            thirdC = thirdCondition;
            return thirdCondition;
        }
    }
}
