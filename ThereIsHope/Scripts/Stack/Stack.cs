using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Stack<T>
{
    public Stack(int size, int growSize = 1)
    {
        m_size = size;
        m_array = new T[m_size];
        m_growsize = ((growSize > 0) ? growSize : 1);
    }

    ~Stack()
    {
        //m_array = null;
    }

    public void Push(T val)
    {
        if (isFull())
        {
            expand();
        }

        m_array[m_Top++] = val;
    }

    public void Pop()
    {
        if (!isEmpty())
        {
            m_Top--;
        }
    }

    bool expand()
    {
        if (m_growsize <= 0)
            return false;

        T[] temp = new T[m_size + m_growsize];

        for (int i = 0; i < m_size; i++)
        {
            temp[i] = m_array[i];
        }

        m_array = temp;

        return true;
    }

    public T this[int index] { get { return m_array[index]; } set {} }


    int getSize() { return m_Top; }
    int getMaxSize() { return m_size; }
    bool isEmpty() { return (m_Top == -1); }
    bool isFull() { return (m_Top == m_size - 1); }

    public T[] m_array;
    int m_Top;
    int m_size;
    int m_growsize;
}
