using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MyList<T> : IEnumerable<T>
{
    private T[] _array = new T[4]; // Хранит элементы списка
    private int _count = 0; // Счетчик добавленных элементов

    public int Count => _count; // Возвращает текущее количество элементов
    public int Capacity => _array.Length; // Возвращает емкость списка

    // Добавляет элемент в список
    public void Add(T item) 
    {
        if (_count == _array.Length) 
        {
            Resize(); // Увеличиваем массив, если он полон
        }
        _array[_count++] = item; // Добавляем элемент и увеличиваем счетчик
    }

    // Удаляет первый найденный элемент из списка
    public void Remove(T item) 
    {
        for (int i = 0; i < _count; i++) 
        {
            if (EqualityComparer<T>.Default.Equals(_array[i], item)) 
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
    public void Insert(int index, T item) 
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

    // Выполняет указанное действие для каждого элемента
    public void ForEach(Action<T> action)
    {
        for (int i = 0; i < _count; i++)
        {
            action(_array[i]); // Выполняем действие для элемента
        }
    }

    // Находит индекс первого вхождения указанного элемента
    public int IndexOf(T item)
    {
        for (int i = 0; i < _count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(_array[i], item))
            {
                return i; // Возвращаем индекс, если найдено
            }
        }
        return -1; // Возвращаем -1, если не найдено
    }

    // Находит первый элемент, который соответствует заданному предикату
    public T Find(Func<T, bool> predicate)
    {
        for (int i = 0; i < _count; i++)
        {
            if (predicate(_array[i]))
            {
                return _array[i]; // Возвращаем элемент, если найдено
            }
        }
        return default; // Возвращаем значение по умолчанию, если не найдено
    }

    // Сортирует элементы в списке
    public void Sort(Comparison<T> comparison)
    {
        Array.Sort(_array, 0, _count, Comparer<T>.Create(comparison)); // Сортируем массив
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
        T[] newArray = new T[newSize];
        Array.Copy(_array, newArray, _count); // Копируем старые элементы
        _array = newArray; // Заменяем старый массив новым
    }

    // Реализация IEnumerable<T>
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return _array[i]; // Возвращаем элементы для перебора
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); // Реализация интерфейса IEnumerable
}
