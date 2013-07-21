using System;


class DayProfile
{
    private double balance;
    private DateTime balancedate;

    public DateTime BalanceDate
    {
        get
        {
            return balancedate;
        }
    }

    public double Balance
    {
        get
        {
            return balance;
        }
    }

    public DayProfile()
    {
    }

    public DayProfile(double bal, DateTime baldate)
    {
        balance = bal;
        balancedate = baldate;
    }

}

