using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BynaryTreeSearch
{
    public interface AbstractBinarySearchTree<E> where E : IComparable<E>
    {
        void Insert(E element);
        bool Contains(E element);
        AbstractBinarySearchTree<E> Search(E element);
        Node<E> GetRoot();
        Node<E> GetLeft();
        Node<E> GetRight();
        E GetValue();
        int MaxPathSum();





    }
}
