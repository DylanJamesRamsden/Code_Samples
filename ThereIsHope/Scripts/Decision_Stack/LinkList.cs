using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LinkList<T>
{
    public LinkList()
    {
        m_size = 0;
        m_root = null;
        m_last = null;
    }

    ~LinkList()
    {
        while (m_root != null)
        {
            Pop();
        }
    }

    LinkNode<T> Begin()
    {
        return m_root;
    }

    LinkNode<T> End()
    {
        return null;
    }

    public void push_Back(T newData)
    {
        LinkNode<T> node = new LinkNode<T>();

        node.m_data = newData;
        node.m_next = null;

        if (m_last != null)
        {
            m_last.m_next = node;
            m_last = node;
        }
        else
        {
            m_root = node;
            m_last = node;
        }

        m_size++;
    }

    void Pop()
    {
        if (m_root.m_next == null)
        {
            m_root = null;
        }
        else
        {
            LinkNode<T> prevNode = m_root;

            while (prevNode.m_next != null && prevNode.m_next != m_last)
            {
                prevNode = prevNode.m_next;
            }

            prevNode.m_next = null;
            m_last = prevNode;

            m_size = (m_size == 0 ? m_size : m_size - 1);
        }
    }

    public T pop_Front()
    {
        T firstData = m_root.m_data;

        m_root = m_root.m_next;

        m_size = (m_size == 0 ? m_size : m_size - 1);

        return firstData;
    }

    public int getSize()
    {
        return m_size;
    }

    public int m_size { get; set; }
    public LinkNode<T> m_root { get; set; }
    public LinkNode<T> m_last { get; set; }
}
