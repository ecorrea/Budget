using System;
using System.Collections.Generic;

class MonthlyStatement
{
    private List<Balance> chargebalance = new List<Balance>();
    private DateTime startdate, enddate, duedate;
    private double tempbal, totalbalance = 0, endbalance;
    private bool ispaidoff;

    public DateTime StartDate
    {
        get
        {
            return startdate;
        }
    }

    public DateTime EndDate
    {
        get
        {
            return enddate;
        }
    }

    public DateTime DueDate
    {
        get
        {
            return duedate;
        }
    }

    public bool IsPaidOff
    {
        get
        {
            return ispaidoff;
        }
    }

    public double TotalBalance
    {
        get
        {
            return totalbalance;
        }
    }

    public double EndBalance
    {
        get
        {
            return endbalance;
        }
    }

    public MonthlyStatement()
    {
    }

    public MonthlyStatement(DateTime statementend, DateTime due_date)
    {
        enddate = statementend;
        startdate = statementend.AddMonths(-1);
        startdate = startdate.AddDays(1);
        duedate = due_date;
    }

    public void AddTransaction(double amount, DateTime date)
    {
        if ((date.CompareTo(enddate) <= 0) && (date.CompareTo(startdate) >= 0))
        {
            if (chargebalance.Count == 0)
            {
                totalbalance = amount;
            }
            else
            {
                totalbalance = chargebalance[chargebalance.Count - 1].AccBalance + amount;
            }
            Balance bal = new Balance(totalbalance, date);
            chargebalance.Add(bal);
        }
    }

    public bool Payment(double amount, DateTime date)
    {
        if (date == duedate)
        {
            endbalance = totalbalance;
            totalbalance = totalbalance - amount;
            if (totalbalance == 0)
            {
                ispaidoff = true;
            }
            else
            {
                ispaidoff = false;
            }
        }
        return ispaidoff;
    }
}
