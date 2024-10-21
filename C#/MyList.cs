public class MyList
{
    private int[] _array = new int[4]; // Хранит элементы списка
    private int _count = 0; // Счетчик добавленных элементов

    public int Count 
    {
        get { return _count; } // Возвращает текущее количество элементов
    }

    // Добавляет элемент в список
    public void Add(int item) 
    {
        if (_count == _array.Length) 
        {
            Resize(); // Увеличиваем массив, если он полон
        }
        _array[_count++] = item; // Добавляем элемент и увеличиваем счетчик
    }

    // Удаляет первый найденный элемент из списка
    public void Remove(int item) 
    {
        for (int i = 0; i < _count; i++) 
        {
            if (_array[i] == item) 
            {
                for (int j = i; j < _count - 1; j++) 
                {
                    _array[j] = _array[j + 1]; // Сдвигаем элементы влево
                }
                _count--; // Уменьшаем счетчик
                return;
            }
        }
    }

    // Удаляет элемент по индексу
    public void RemoveAt(int index) 
    {
        if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
        
        for (int i = index; i < _count - 1; i++) 
        {
            _array[i] = _array[i + 1]; // Сдвигаем элементы
        }
        _count--; // Уменьшаем счетчик
    }

    // Вставляет элемент по индексу
    public void Insert(int index, int item) 
    {
        if (index < 0 || index > _count) throw new IndexOutOfRangeException();
        
        if (_count == _array.Length) 
        {
            Resize(); // Увеличиваем массив, если он полон
        }
        for (int i = _count; i > index; i--) 
        {
            _array[i] = _array[i - 1]; // Сдвигаем элементы вправо
        }
        _array[index] = item; // Вставляем элемент
        _count++; // Увеличиваем счетчик
    }

    // Очищает список
    public void Clear() 
    {
        _count = 0; // Сбрасываем счетчик
    }

    // Возвращает строковое представление списка
    public override string ToString() 
    { 
        return string.Join(", ", _array.Take(_count)); // Выводит только добавленные элементы
    }

    // Увеличивает размер массива
    private void Resize() 
    {
        int newSize = _array.Length * 2; // Увеличиваем размер вдвое
        int[] newArray = new int[newSize];
        Array.Copy(_array, newArray, _count); // Копируем старые элементы
        _array = newArray; // Заменяем старый массив новым
    }
}
