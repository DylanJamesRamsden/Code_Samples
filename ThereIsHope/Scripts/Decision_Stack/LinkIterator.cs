using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class LinkIterator<T>
{
    LinkIterator()
    {
        m_node = null;
    }

    ~LinkIterator()
    {

    }

    //static LinkIterator<T> operator=(LinkNode<T> node)
    //{
    //   // m_node = node;
    //}

    public LinkNode<T> m_node { get; set; }
}
