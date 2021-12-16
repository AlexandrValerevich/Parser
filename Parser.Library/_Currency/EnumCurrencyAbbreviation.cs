using System;

namespace Parser.Currency
{
    [System.Flags]
    public enum CurrencyAbbreviation : byte
    {
        RUB = 0b_0001,
        USD = 0b_0010,
        EUR = 0b_0100,
        BYN = 0b_1000
    }
}