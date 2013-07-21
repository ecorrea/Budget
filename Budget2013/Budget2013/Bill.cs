using System;

class Bill : Expense
{
    private int dayofmonth;

    public int DayofMonth
    {
        get
        {
            return dayofmonth;
        }
    }

    public Bill()
    {
    }

    public Bill(string BillName, double BillCost, int BillDay) :
        base("bill", BillName, BillCost, true, true)
    {
        dayofmonth = BillDay;
    }

    public Bill(string BillName, double BillCost, int BillDay, ChargeCard Card) :
        base("bill", BillName, BillCost, true, true)
    {
        dayofmonth = BillDay;
        Charge = Card;
        isCharge = true;
    }

    public Bill(string BillName, double BillCost, int BillDay, CreditCard Card) :
        base("bill", BillName, BillCost, true, true)
    {
        dayofmonth = BillDay;
        Credit = Card;
        isCredit = true;
    }

    public Bill(string BillName, double BillCost, int BillDay, BankAccounts Bank) :
        base("bill", BillName, BillCost, true, true)
    {
        dayofmonth = BillDay;
        Debit = Bank;
        isDebit = true;
    }

    public override string ToString()
    {
        return String.Format("Your {0} {1} is due {2}th of the month, and costs ${3}.", name, type, dayofmonth, cost);
    }

}
