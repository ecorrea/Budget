using System;
using System.Collections.Generic;

class CreditCard : Debt
{
    // Needs to be fixed to displayed and allow for a list of balances to be 
    // kept and payed for
    private double limit, balance;
    private List<Balance> bankbalance = new List<Balance>();
    private DateTime statementend;

    public List<Balance> BankBalance
    {
        get
        {
            return bankbalance;
        }
    }

    public CreditCard()
    {
    }

    public CreditCard(string CardName, double CardUsage, double CardLimit, DateTime StatementEndDate) :
        base(CardName, "Credit Card", CardUsage)
    {
        statementend = StatementEndDate;
        limit = CardLimit;
    }
    
    public void Payment(double CheckTotal, DateTime dt)
    {
        if (bankbalance.Count == 0)
        {
            balance = -1*CheckTotal;
        }
        else
        {
            balance = bankbalance[bankbalance.Count - 1].AccBalance - CheckTotal;
        }
        Balance bal = new Balance(balance, dt);
        bankbalance.Add(bal);

    }

    public void Purchase(double Amount, DateTime dt)
    {
        if (bankbalance.Count == 0)
        {
            balance = Amount;
        }
        else
        {
            balance = bankbalance[bankbalance.Count - 1].AccBalance + Amount;
        }
        Balance bal = new Balance(balance, dt);
        bankbalance.Add(bal);
    }

    public void ClearBalance()
    {
        bankbalance.Clear();
        Balance bal = new Balance(initamount, programstartdate);
        bankbalance.Add(bal);
        balance = 0;
    }

}
