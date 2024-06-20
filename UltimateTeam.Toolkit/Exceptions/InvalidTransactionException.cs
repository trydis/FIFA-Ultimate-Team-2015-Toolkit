﻿using UltimateTeam.Toolkit.Models.Generic;

namespace UltimateTeam.Toolkit.Exceptions
{
    public class InvalidTransactionException : FutErrorException
    {
        public InvalidTransactionException(FutError futError, Exception exception)
            : base(futError, exception)
        {
        }
    }
}
