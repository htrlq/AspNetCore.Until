using System;

namespace AspNetCore.Until
{
    public interface ITokenCheck
    {
        void Valid(Object entity);
    }
}