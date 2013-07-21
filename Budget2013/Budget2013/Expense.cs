using System;

class Expense
{

    protected string type, name, comment;
    protected double cost;
    protected bool iscreditexpense;
    protected string accountused;
    protected CreditCard Credit;
    protected ChargeCard Charge;
    protected BankAccounts Debit;
    protected bool isCredit = false, isCharge = false, isDebit = false;
    protected DateTime dt;
    protected string store;

    public bool IsCredit
    {
        get
        {
            return isCredit;
        }
    }

    public bool IsCharge
    {
        get
        {
            return isCharge;
        }
    }

    public bool IsDebit
    {
        get
        {
            return isDebit;
        }
    }

    public bool IsCreditExpense
    {
        get
        {
            return iscreditexpense;
        }
    }

    public string Comment
    {
        set
        {
            comment = value;
        }
        get
        {
            return comment;
        }
    }

    public double Cost
    {
        get
        {
            return cost;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public Expense()
    {
    }

    public Expense(string ExpenseType, string ExpenseName, double ExpenseCost, bool isReoccuring, bool isMonthly)
    {
        type = ExpenseType;
        name = ExpenseName;
        cost = ExpenseCost;
    }

    public Expense(string ExpenseType, string ExpenseName, double ExpenseCost, bool isReoccuring, bool isMonthly, bool isCredit)
    {
        type = ExpenseType;
        name = ExpenseName;
        cost = ExpenseCost;
        iscreditexpense = isCredit;
    }

    public Expense(string ExpenseName, double ExpenseCost)
    {
        name = ExpenseName;
        cost = ExpenseCost;
    }

    public void ExpensePurchase(DateTime CurrentDate)
    {
        if (isCharge && !isCredit && !isDebit)
        {
            Charge.Buy(cost, CurrentDate);
        }
        else if (!isCharge && isCredit && !isDebit)
        {
            Credit.Purchase(cost, CurrentDate);
        }
        else if (!isCharge && !isCredit && isDebit)
        {
            Debit.Withdrawl(cost, CurrentDate);
        }            
    }

}
