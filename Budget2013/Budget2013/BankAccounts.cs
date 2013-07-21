using System;
using System.Collections.Generic;

class BankAccounts
{
    protected string name;
    protected double balance, fees, initbalance;
    private List<Balance> bankBalance = new List<Balance>();
    DateTime programstartdate;

    public List<Balance> BankBalance
    {
        get
        {
            return bankBalance;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public double InitBalance
    {
        get
        {
            return initbalance;
        }
    }

    public BankAccounts()
    {
    }

    public BankAccounts(string AccName, double TotBalance)
    {
        name = AccName;
        balance = TotBalance;
        programstartdate = new DateTime(2013, 4, 25);
        Balance bal = new Balance(balance, programstartdate);
        initbalance = TotBalance;
        bankBalance.Add(bal);
    }

    public void Paycheck(double CheckTotal, DateTime dt)
    {
        if (bankBalance.Count == 0)
        {
            balance = CheckTotal;
        }
        else
        {
            balance = bankBalance[bankBalance.Count - 1].AccBalance + CheckTotal;
        }
        Balance bal = new Balance(balance, dt);
        //FIX THIS SHIT
        bool nodate = true;
        for (int i = 0; i < bankBalance.Count; i++)
        {
            if (bankBalance[i].BalanceDate == dt)
            {
                bankBalance[i] = bal;
                nodate = false;
            }
        }
        if (nodate)
        {
            bankBalance.Add(bal);
        }
    }

    public void Withdrawl(double Amount, DateTime dt)
    {
        if (bankBalance.Count == 0)
        {
            balance = -1 * Amount;
        }
        else
        {
            balance = bankBalance[bankBalance.Count - 1].AccBalance - Amount;
        }
        Balance bal = new Balance(balance, dt);
        //FIX THIS SHIT
        bool nodate = true;
        for (int i = 0; i < bankBalance.Count; i++)
        {
            if (bankBalance[i].BalanceDate == dt)
            {
                bankBalance[i] = bal;
                nodate = false;
            }
        }
        if (nodate)
        {
            bankBalance.Add(bal);
        }
    }

    public void ClearBalance()
    {
        bankBalance.Clear();
        Balance bal = new Balance(initbalance, programstartdate);
        bankBalance.Add(bal);
        balance = 0;
    }
}
