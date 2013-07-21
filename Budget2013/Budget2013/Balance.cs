using System;


class Balance
{
    private double accbalance;
    private DateTime balancedate;

    public DateTime BalanceDate
    {
        get
        {
            return balancedate;
        }
    }

    public double AccBalance
    {
        set
        {
            accbalance = value;
        }
        get
        {
            return accbalance;
        }
    }

    public Balance()
    {
    }

    public Balance(double bal, DateTime dat)
    {
        accbalance = bal;
        balancedate = dat;
    }

    public override string ToString()
    {
        return String.Format("{0} on {1}", accbalance, balancedate);
    }
}
