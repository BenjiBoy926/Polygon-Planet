using System;

public class LazyLoader<T> where T : class
{
    private T _obj;
    private Func<T> load;

    public LazyLoader(Func<T> loadingFunction)
    {
        load = loadingFunction;
    }

    public T obj
    {
        get
        {
            if (_obj == null)
            {
                _obj = load();
            }
            return _obj;
        }
    }
}
