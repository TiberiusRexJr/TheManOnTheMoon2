using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManOnTheMoon.Models
{
    public class Dummy :IEnumerable
    {
        List<Dummy> categories;



        public class DummyEnumerator : IEnumerator
        {
            List<Dummy> categoryEnumerationList;

            int position = -1;

            public DummyEnumerator(List<Dummy> categories)
            {
                categoryEnumerationList = categories;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return categoryEnumerationList[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public bool MoveNext()
            {
                position++;
                return (position < categoryEnumerationList.Count);
            }

            public void Reset()
            {
                position = -1;
            }
        }

        // Implementation for the GetEnumerator method.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public DummyEnumerator GetEnumerator()
        {
            return new DummyEnumerator(categories);
        }

    }
}