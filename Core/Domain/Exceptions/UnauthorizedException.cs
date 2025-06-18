﻿namespace Domain.Exceptions;

public sealed class UnauthorizedException(string message = "Invalid Email or Password")
    : Exception(message);
