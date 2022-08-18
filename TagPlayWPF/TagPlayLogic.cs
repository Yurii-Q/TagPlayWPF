namespace TagPlay
{
    //Класс для создание массива 4*4 с уникальными значениями от 0 до 15
    class RandomField
    {
        const int rang = 4;
        int[,] array = new int[rang, rang];
        System.Random random = new System.Random();

        //Свойства для хранение индексов нулевого числа
        internal int indexI0 { get; private set; } = 0;
        internal int indexJ0 { get; private set; } = 0;


        internal RandomField()
        {
            InitArray();
            FillFieldRandom();
        }

        internal void Reset()
        {
            InitArray();
            FillFieldRandom();
        }

        internal bool Check()
        {
            return checkedField1() || checkedField2();
        }

        //Получение числа из массива по индксам
        internal int getNumber(int i, int j)
        {
            return array[i, j];
        }

        //Функция для перестановки двух элементов по идексу i (в игровом поле)
        internal void swapNumberByI(int newIndexI)
        {
            if (newIndexI < 0 || newIndexI > 3) return;

            int temp = array[newIndexI, indexJ0];
            array[newIndexI, indexJ0] = 0;
            array[indexI0, indexJ0] = temp;
            indexI0 = newIndexI;
        }

        //Функция для перестановки двух элементов по идексу j (в игровом поле)
        internal void swapNumberByJ(int newIndexJ)
        {
            if (newIndexJ < 0 || newIndexJ > 3) return;

            int temp = array[indexI0, newIndexJ];
            array[indexI0, newIndexJ] = 0;
            array[indexI0, indexJ0] = temp;
            indexJ0 = newIndexJ;
        }

        //Функция отображающая игровое поле в консоли
        //internal void displayField()
        //{
        //    for (int i = 0; i < rang; i++)
        //    {
        //        for (int j = 0; j < rang; ++j)
        //        {
        //            if (array[i, j] == 0)
        //            {
        //                System.Console.Write("O\t");
        //                continue;
        //            }
        //            System.Console.Write(array[i, j] + "\t");
        //        }
        //        System.Console.Write("\n\n");
        //    }
        //}

        //internal void test()
        //{
        //    for (int i = 0; i < rang; ++i)
        //        for (int j = 0; j < rang; ++j)
        //        {                    
        //            array[i, j] = rang * i + j + 1;
        //        }
        //}
        private bool checkedField1()
        {
            for (int i = 0; i < rang; ++i)
                for (int j = 0; j < rang; ++j)
                {
                    if (array[i, j] != rang * i + j) return false;
                }

            return true;
        }
        private bool checkedField2()
        {
            for (int i = 0; i < rang; ++i)
                for (int j = 0; j < rang; ++j)
                {
                    if (i == 3 && j == 3) break;
                    if (array[i, j] != rang * i + j + 1) return false;
                }
            return true;
        }        

        //Инициализация массива -1
        private void InitArray()
        {
            
            for (int i = 0; i < rang; i++)
                for (int j = 0; j < rang; j++)
                    array[i, j] = -1;
        }

        //Заполнение массива уникальными числами
        private void FillFieldRandom()
        {
            int temp = -1;

            //Заполнение массива уникальными числами
            for (int i = 0; i < rang; ++i)
            {
                for (int j = 0; j < rang; ++j)
                {
                    //Проверка сгенерированного числа на существование в массиве                                       
                    while (true)
                    {
                    Begin: temp = random.Next(rang * rang);

                        for (int k = 0; k < rang; ++k)
                        {
                            for (int l = 0; l < rang; ++l)
                            {
                                if (array[k, l] == temp)
                                {
                                    goto Begin;
                                }
                            }
                        }
                        break;
                    }
                    array[i, j] = temp;
                }
            }//Конец инициализации массива

            //Поиск нулевого элемента
            for (int i = 0; i < rang; ++i)
            {
                for (int j = 0; j < rang; ++j)
                {
                    if (array[i, j] == 0)
                    {
                        indexI0 = i;
                        indexJ0 = j;
                        goto Exit;
                    }
                }
            }
        Exit:;
        }
    }
}
