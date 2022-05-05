using System;
using System.Collections.Generic;
using System.Text;

namespace Challenge.Ecommerce.Comun
{
    public interface IAppLogger<T>
    {

        void LogInformation(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void LogError(string message, params object[] args);

    }
}
