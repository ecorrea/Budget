using System;
using System.Collections.Generic;


class ChargeCard : Debt
{
    private double limit;
    private List<MonthlyStatement> listofstatement = new List<MonthlyStatement>();
    private DateTime statementend, statementdue;
    private BankAccounts bank;
    private bool hasdefault = false;
    private DateTime programstartdate;

    // Add potential fees and APR

    public DateTime ProgramStartDate
    {
        set
        {
            programstartdate = value;
        }
        get
        {
            return programstartdate;
        }
    }

    public bool HasDefault
    {
        get
        {
            return hasdefault;
        }
    }

    public BankAccounts Bank
    {
        set
        {
            bank = value;
            hasdefault = true;
        }
        get
        {
            return bank;
        }
    }

    public int DueDay
    {
        get
        {
            return statementdue.Day;
        }
    }

    public List<MonthlyStatement> ListofStatement
    {
        get
        {
            return listofstatement;
        }
    }


    public ChargeCard()
    {
    }

    public ChargeCard(string CardName, double CardUsage, double CardLimit, DateTime StatementEndDate, DateTime Due) :
        base(CardName, "Credit Card", CardUsage)
    {
        statementend = StatementEndDate;
        statementdue = Due;
        limit = CardLimit;
        MonthlyStatement mon = new MonthlyStatement(StatementEndDate, Due);
        listofstatement.Add(mon);
    }

    public void Buy(double amount, DateTime date)
    {
        bool IfInList = false;
        for (int i = 0; i < listofstatement.Count; i++)
        {
            if ((date.CompareTo(listofstatement[i].EndDate) <= 0) && (date.CompareTo(listofstatement[i].StartDate) >= 0))
            {
                IfInList = true;
                listofstatement[i].AddTransaction(amount, date);
            }
        }
        if (!IfInList)
        {
            if (date.Day <= statementend.Day)
            {          
               DateTime tempdate = date.AddMonths(1);
               DateTime tempduedate = new DateTime(tempdate.Year, tempdate.Month, statementdue.Day);
               DateTime tempenddate = new DateTime(date.Year, date.Month, statementend.Day);
               MonthlyStatement mon = new MonthlyStatement(tempenddate, tempduedate);
               listofstatement.Add(mon);
               listofstatement[listofstatement.Count - 1].AddTransaction(amount, date);
            }
            else if (date.Day > statementend.Day)
            {
                DateTime tempdate1 = date.AddMonths(2);
                DateTime tempdate2 = date.AddMonths(1);
                DateTime tempduedate = new DateTime(tempdate1.Year, tempdate1.Month, statementdue.Day);
                DateTime tempenddate = new DateTime(tempdate2.Year, tempdate2.Month, statementend.Day);
                MonthlyStatement mon = new MonthlyStatement(tempenddate, tempduedate);
                listofstatement.Add(mon);
                listofstatement[listofstatement.Count - 1].AddTransaction(amount, date);
            }
        }
    }

    public void Payoff(double amount, BankAccounts BankAcc, DateTime SelectedDueDate)
    {
        for (int i = 0; i < listofstatement.Count; i++)
        {
            if (listofstatement[i].DueDate == SelectedDueDate)
            {
                BankAcc.Withdrawl(amount, SelectedDueDate);
                listofstatement[i].Payment(amount, SelectedDueDate);
            }
        }
    }

    public void Payoff(DateTime SelectedDueDate)
    {
        for (int i = 0; i < listofstatement.Count; i++)
        {
            if (listofstatement[i].DueDate == SelectedDueDate)
            {
                bank.Withdrawl(listofstatement[i].TotalBalance, SelectedDueDate);
                listofstatement[i].Payment(listofstatement[i].TotalBalance, SelectedDueDate);
            }
        }
    }

    public void Payment(double amount, BankAccounts BankAcc, DateTime PaymentDate)
    {
        for (int i = 0; i < listofstatement.Count; i++)
        {
            if (listofstatement[i].DueDate.AddMonths(-1).Month == PaymentDate.Month)
            {
                listofstatement[i].Payment(amount, PaymentDate);
            }
        }
    }

    public double GetBalance(DateTime SelectedDueDate)
    {
        double temp = 0;
        for (int i = 0; i < listofstatement.Count; i++)
        {
            if (listofstatement[i].DueDate == SelectedDueDate)
            {
                temp = listofstatement[i].TotalBalance;
            }
        }
        return temp;
    }

    public double GetEndBalance(DateTime SelectedDueDate)
    {
        double temp = 0;
        for (int i = 0; i < listofstatement.Count; i++)
        {
            if (listofstatement[i].DueDate == SelectedDueDate)
            {
                temp = listofstatement[i].EndBalance;
            }
        }
        return temp;
    }

    public void ClearBalance()
    {
        listofstatement.Clear();
        MonthlyStatement mon = new MonthlyStatement(statementend, statementdue);
        listofstatement.Add(mon);
    }
}
