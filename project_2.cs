using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proj2
{
    class Program2
    {
        public class Transition
        {
            public string startState;
            public char edge; 
            public string endState;

            public Transition(string startState, char edge, string endState)
            {
                this.startState = startState;
                this.edge = edge;
                this.endState = endState;
            }
        }  
        public class DeterminFA
        {
            public List<string> states = new List<string>();
            public List<char> alphabets = new List<char>();
            public List<Transition> delta = new List<Transition>();
            public string q0;
            public List<string> finals = new List<string>();

            public DeterminFA(IEnumerable<string> states,IEnumerable<char> alphabets,IEnumerable<string> finals,IEnumerable<Transition> delta,string q0)
            {
                this.states = states.ToList();
                this.alphabets = alphabets.ToList();
                this.finals=finals.ToList();
                this.q0=q0;
                this.delta=delta.ToList();
            }
        } 
        public static List<string> landa (List<string> input,DeterminFA fa)
        {
            List<string> result = new List<string>();
            foreach (string i in input)
            {
                List<string> current = new List<string>();
                current.Add(i);
                int j=0;
                while(j<current.Count)
                {
                    List<string> statelam=fa.delta.Where(x=>current[j]==x.startState && x.edge=='$').Select(x=> x.endState).ToList();
                    current.AddRange(statelam);
                    j++;
                }
                for (int k = 0; k < current.Count; k++)
                {
                    if (!result.Contains(current[k]))
                        result.Add(current[k]);
                }
            }  
            return result;
        }
        public static bool contain (List<List<string>> DFAStates, List<string> mclosur)
        {
            foreach (var state in DFAStates)
            {
                if (complist(state,mclosur))
                    return true;
                
            }
            return false;
        }
        public static List<string> move (List<string> input, char lable, DeterminFA fa)
        {
            List<string> result = new List<string>();
            foreach (string i in input)
            {
                List<string> output=fa.delta.Where(x=>i==x.startState && x.edge==lable).Select(x=> x.endState).ToList();
                result.AddRange(output);
            }
            return result;
        }
        public static bool complist(List<string> state , List<string> mclosur)
        {
            bool equal = true;
            state.Sort();
            mclosur.Sort();
            if (state.Count == mclosur.Count)
            {
                for (int j = 0; j < mclosur.Count; j++)
                {
                    if (state[j] != mclosur[j])
                    {
                        equal = false;
                        break;
                    }
                }
                if (equal)
                    return true;
                
            }
            return false;
        }
        public static long Solve(string[] states,char[] alphabets,string[] finals,string[][] rules,long n)
        {
            List<Transition> delta=new List<Transition>();
            List<List<string>> somestates = new List<List<string>>();
            for (int i = 0; i < n; i++)
                delta.Add(new Transition(rules[i][0],rules[i][1].ToCharArray()[0],rules[i][2]));
            DeterminFA fa=new DeterminFA(states,alphabets,finals,delta,states[0]);
            List<string> input = new List<string>();
            input.Add(states[0]);
            somestates.Add(landa(input,fa));
            int k = 0;
            while (k< somestates.Count)
            {
                foreach (char i in alphabets)
                {
                    List<string> s = move(somestates[k],i,fa);
                    List<string> slanda = landa(s,fa);
                    // if (!DFAStates.Contains(mclosur))
                    //     DFAStates.Add(mclosur);
                    if (!contain(somestates, slanda))
                        somestates.Add(slanda);
                }
                k++;
            }
            return somestates.Count;
        }
        static void Main2(string[] args)
        {
            string[] states=Console.ReadLine().Split(',');
            for (int i = 0; i < states.Length; i++)
                states[i]=states[i].Trim(new char[]{'{','}'});    

            string[] input=Console.ReadLine().Split(',');
            char[] alphabets=new char[input.Length];
            for (int i = 0; i < alphabets.Length; i++)
                alphabets[i]=input[i].Trim(new char[]{'{','}'}).ToCharArray()[0];   

            string[] finals=Console.ReadLine().Split(',');
            for (int i = 0; i < finals.Length; i++)
                finals[i]=finals[i].Trim(new char[]{'{','}'}); 
            
            string[] input1=Console.ReadLine().Split();
            long n=long.Parse(input1[0]);
            string[][] rules=new string[n][];
            for (int i = 0; i < n; i++)
                rules[i]=new string[3];
            for (int i = 0; i < n; i++)
            {
                string[] input2=Console.ReadLine().Split(' ',',');
                rules[i][0]=input2[0];
                rules[i][1]=input2[1];
                rules[i][2]=input2[2];
            } 
            System.Console.WriteLine(Solve(states,alphabets,finals,rules,n));
        }
    }
}
