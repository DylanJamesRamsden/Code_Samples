using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinkQueue<T>
{
    public LinkQueue()
    {
        m_elements = new LinkList<T>();
    }

    ~LinkQueue()
    {

    }

    public void Push(T val)
    {
        m_elements.push_Back(val);
    }

    public T Pop()
    {
        return m_elements.pop_Front();
    }

    public int returnQueSize()
    {
        return m_elements.getSize();
    }

    public LinkList<T> m_elements { get; set; }
    //int m_size;
}
