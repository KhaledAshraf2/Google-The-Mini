using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE
{
   
   public class Listt<t>
    {
       public t[] items;
       public   int elements, capacity;
        public  Listt()
        {
            capacity = 50;
            items = new t[capacity];
            elements = 0;
        }
        public Listt(int Capacity)
        {
            capacity = Capacity;
           items = new t[Capacity];
            elements = 0;
        }
        public  t getvalue(int i)
        { 
            return items[i];
        }
       
      
        public void add(t value)
        {
            if (elements == capacity)
            {
                expand();
            }

           
            items[elements++] = value;
            
          
            
        }
        public  int count()
        {
            return elements;
        }
        public void expand()
        {
            capacity *= 2;
            t[] tmp = new t[capacity];
            for (int i = 0; i < capacity / 2; i++)
            {
                tmp[i] = items[i];
            }
            items = new t[capacity];
            items = tmp;
        }
        public void clear()
        {
            capacity = 50;
            items = new t[capacity];
            elements = 0;

        }
       public bool isempty()
        {
            if (elements == 0)
            {
                return true;
            }
            else
                return false;
           
        }

    }
}
