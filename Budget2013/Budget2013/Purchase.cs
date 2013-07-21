using System;


class Purchase : Expense
{
    

    public DateTime PurchaseDate
    {
        get
        {
            return dt;
        }
    }

    public Purchase()
    {
    }

    public Purchase(string PurchaseItemName, string PurchaseStore, double PurchaseCost, ChargeCard card, DateTime date) :
        base("Need Purchase", PurchaseItemName, PurchaseCost, false, false, true)
    {
        dt = date;
        store = PurchaseStore;
        accountused = card.Name;
        isCharge = true;
        isCredit = false;
        isDebit = false;
        Charge = card;
        //card.Buy(PurchaseCost, date);
    }

    public Purchase(string PurchaseItemName, string PurchaseStore, double PurchaseCost, CreditCard card, DateTime date) :
        base("Need Purchase", PurchaseItemName, PurchaseCost, false, false, true)
    {
        dt = date;
        store = PurchaseStore;
        accountused = card.Name;
        isCharge = false;
        isCredit = true;
        isDebit = false;
        Credit = card;
        //card.Purchase(PurchaseCost, date);
    }

    public Purchase(string PurchaseItemName, string PurchaseStore, double PurchaseCost, BankAccounts card, DateTime date) :
        base("Need Purchase", PurchaseItemName, PurchaseCost, false, false, false)
    {
        dt = date;
        store = PurchaseStore;
        accountused = card.Name;
        isCharge = false;
        isCredit = false;
        isDebit = true;
        Debit = card;
        //card.Withdrawl(PurchaseCost, date);
    }

}
