using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aisde2
{
    class Listaup<T>
    {
        public Listaup(int rozmiar)
        {
            mmagazyn = new wezel<T>[rozmiar];
        }
        wezel<T>[] mmagazyn;
        int mrozmiar = 0;

        public void dodaj(double indeks, T wartosc)
        {
            mmagazyn[mrozmiar] = new wezel<T>(indeks, wartosc);
            insertionsort();
            ++mrozmiar;
        }
        void insertionsort()
        {
            wezel<T> tmp;
            for (int i = 0; i < mrozmiar; ++i)
            {
                if (mmagazyn[i].indeks > mmagazyn[mrozmiar].indeks)
                {
                    tmp = mmagazyn[i];
                    mmagazyn[i] = mmagazyn[mrozmiar];
                    for (int j = mrozmiar; j > i + 1; --j)
                    {
                        mmagazyn[j] = mmagazyn[j - 1];
                    }
                    mmagazyn[i + 1] = tmp;
                    break;
                }
            }
        }

        public void wyswietl()
        {
            for (int i = 0; i < mrozmiar; ++i)
            {
                Console.WriteLine( mmagazyn[i].indeks);

            }
    
            Console.ReadKey();
        }
        public void usun()
        {
            for (int j = 0; j < mrozmiar -1; ++j)
            {
                mmagazyn[j] = mmagazyn[j + 1];
            }
            mmagazyn[mrozmiar -1] = null;
            --mrozmiar;
        }
        public T pierwszyElem()
        {
            return mmagazyn[0].element;
        }
        public double pierwszyElemInd()
        {
            return mmagazyn[0].indeks;
        }
        public int rozmiar()
        {
            return mrozmiar;
        }
    }
}
