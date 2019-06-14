using System;
using System.Collections.Generic;

public class BooleanFunctorList
{
    private List<Func<bool>> funcs = new List<Func<bool>>();

    public bool result
    {
        get
        {
            bool res = true;
            foreach(Func<bool> function in funcs)
            {
                res &= function();
            }
            return res;
        }
    }

    public void Add(Func<bool> add)
    {
        funcs.Add(add);
    }
    public void Remove(Func<bool> add)
    {
        funcs.Remove(add);
    }
}
