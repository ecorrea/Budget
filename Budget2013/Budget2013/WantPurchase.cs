using System;



class WantPurchase : Expense
{

    public string Store
    {
        get
        {
            return store;
        }
    }

    public DateTime Date
    {
        get
        {
            return dt;
        }
    }

    public WantPurchase()
    {
    }

    public WantPurchase(string WantItemName, string WantStore, double WantCost, ChargeCard card, DateTime date) :
        base("Need Purchase", WantItemName, WantCost, false, false, true)
    {
        dt = date;
        store = WantStore;
        isCharge = true;
        isCredit = false;
        isDebit = false;
        Charge = card;
        //card.Buy(WantCost, date);
    }

    public WantPurchase(string WantItemName, string WantStore, double WantCost, CreditCard card, DateTime date) :
        base("Need Purchase", WantItemName, WantCost, false, false, true)
    {
        dt = date;
        store = WantStore;
        isCharge = false;
        isCredit = true;
        isDebit = false;
        Credit = card;
        //card.Purchase(WantCost, date);
    }

    public WantPurchase(string WantItemName, string WantStore, double WantCost, BankAccounts card, DateTime date) :
        base("Need Purchase", WantItemName, WantCost, false, false, false)
    {
        name = WantItemName;
        dt = date;
        store = WantStore;
        isCharge = false;
        isCredit = false;
        isDebit = true;
        Debit = card;
        //card.Withdrawl(WantCost, date);
    }
}
